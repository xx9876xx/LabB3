using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkPower.LabB3.Domain;
using ThinkPower.LabB3.DataAccess.DAO;
using ThinkPower.LabB3.DataAccess.DO;
using ThinkPower.LabB3.Domain.DTO;
using ThinkPower.LabB3.Domain.Entity.Question;
using NLog;
using System.Runtime.ExceptionServices;
using Newtonsoft.Json;
using ThinkPower.LabB3.Domain.Resources;

namespace ThinkPower.LabB3.Domain.Service
{
    //TODO 補interface的公開方法
    public class QuestionnaireService
    {
        /// <summary>
        /// 計算問卷填答得分
        /// </summary>
        /// <param name="answer">問卷填答資料</param>
        /// <returns> 問卷填答結果 </returns>
        public QuestionnaireResultEntity Calculate(QuestionnaireAnswerEntity answer)
        {
            if (answer == null)
            {
                throw new ArgumentNullException(nameof(answer));
            }

            try
            {
                QuestionnaireResultEntity resultEntity = new QuestionnaireResultEntity();
                
                //此份問卷的計分資訊
                QuestionnaireEntity questionnaireEntity = GetQuestionnaire(Convert.ToString(answer.QuestUid));

                //TODO 把計分方法獨立起來 這樣可以好寫測試程式 不然測試計分還寫到DB很怪
                QuestionnaireAnswerEntity answerResult = new QuestionnaireAnswerEntity();
                //整理
                answerResult = SetAnswerDetail(answer, questionnaireEntity);
                //檢核
                //if (!AnswerValidate(answerResult, questionnaireEntity))
                //{
                //    throw new ApplicationException("填答內容有誤!");
                //}
                //分數計算+存檔
                //取得問卷總分
                answer.QuestScore = questionnaireEntity.QuestScore;

                if (questionnaireEntity.NeedScore == "Y")
                {
                    switch (questionnaireEntity.ScoreKind)
                    {
                        //TODO 分數算錯 可以將對應題號之分數清單拉在switch之前共用
                        //加總
                        case "1":
                            var sum = from question in answer.Questions
                                      select question.Score;
                            answer.ActualScore = sum.Sum(e => e.Value);
                            break;
                        //取最高
                        case "2":
                            var max = from question in answer.Questions
                                      select question.Score;
                            answer.ActualScore = max.Max(e => e.Value);
                            break;
                        //取最低
                        case "3"://TODO min method
                            var min = from question in answer.Questions
                                      select question.Score;
                            answer.ActualScore = min.Min(e => e.Value);
                            break;
                        //平均
                        case "4":
                            var avg = from question in answer.Questions
                                      select question.Score;
                            answer.ActualScore = Convert.ToInt32(avg.Average(e => e.Value));
                            break;
                    }
                    //檢查有沒有超過問卷總分
                    if (answer.ActualScore.Value > answer.QuestScore.Value)
                    {
                        answer.ActualScore = answer.QuestScore;
                    }
                    //TODO 存檔拉出來統一做
                    answer.SaveQuestionnaireAnswer();
                }
                else
                {
                    answer.ActualScore = null;
                    //TODO 存檔拉出來統一做
                    answer.SaveQuestionnaireAnswer();
                    resultEntity.message = "您的問卷己填答完畢，謝謝您的參與!";
                    return resultEntity;
                }
                return null;
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(ex).Throw();
                return null;
            }
        }
    
        /// <summary>
        /// 整理問卷填答項目各屬性資料
        /// </summary>
        /// <param name="answer"> 填答問卷主檔Entity </param>
        /// <param name="questionnaireEntity"> 問卷計分資訊 </param>
        /// <returns></returns>
        private QuestionnaireAnswerEntity SetAnswerDetail(QuestionnaireAnswerEntity answer, QuestionnaireEntity questionnaireEntity)
        {
            //針對每個選項的填答
            foreach (var question in answer.Questions)
            {
                var answerDefine = (from quest in questionnaireEntity.QuestDefineEntitys
                                    where quest.QuestionId == question.QuestionId
                                    select quest).FirstOrDefault();
                //填入題目Uid
                if (answerDefine == null)
                {
                    //題目定義不存在
                    throw new ArgumentNullException(nameof(answerDefine));
                }
                question.QuestionUid = answerDefine.Uid;
                
                var scoreQuery = (from ans in answerDefine.AnswerDefineEntities
                                 where question.AnswerCode == ans.AnswerCode
                                 select ans.Score).FirstOrDefault();
                if (!scoreQuery.HasValue)
                {
                        //檢核 題號 不存在 選項 不存在 要能夠檢核到
                        //TODO 直接把選項物件丟出來 在指出分數就好 還可以對選項存不存在做檢核
                    //TODO 查一下
                    throw new ArgumentNullException(nameof(scoreQuery));
                }
                //填入選項分數
                question.Score = scoreQuery.HasValue ? question.Score = scoreQuery.Value : null;
            }
            return answer;
        }

        /// <summary>
        /// 填答內容檢核
        /// </summary>
        /// <param name="answer"> 填答內容 </param>
        /// <param name="questionnaireEntity"> 檢核資訊 </param>
        /// <returns> 檢核是否通過 </returns> 
        private bool AnswerValidate(QuestionnaireAnswerEntity answer, QuestionnaireEntity questionnaireEntity)
        {
            //答題類型
            string type = "";
            //是否必答
            string required = "";
            //可不做答條件
            string allowNaCondition = "";
            //複選最少答項數
            int? minMultipleAnswers = 0;
            //複選最多答項數
            int? maxMultipleAnswers = 0;
            //複選限制單一做答條件
            string singleAnswerCondition = "";
            //答題說明是否必填
            string otherRequired = "";
            //檢核訊息
            string message = "";

            foreach (var question in answer.Questions)
            {
                var questValidate = from quest in questionnaireEntity.QuestDefineEntitys
                                    where quest.Uid == question.QuestionUid
                                    select new
                                    {
                                        quest.AnswerType,
                                        quest.NeedAnswer,
                                        quest.AllowNaCondition,
                                        quest.MinMultipleAnswers,
                                        quest.MaxMultipleAnswers,
                                        quest.SingleAnswerCondition,
                                        quest.AnswerDefineEntities
                                    };
                if (questValidate.FirstOrDefault() == null)
                {
                    throw new ApplicationException("資料庫無「" + nameof(question.AnswerUid) + "」題目之檢核資料");
                }
                else
                {
                    var v = questValidate.First();
                    type = v.AnswerType;
                    required = v.NeedAnswer;
                    allowNaCondition = v.AllowNaCondition;
                    minMultipleAnswers = v.MinMultipleAnswers;
                    maxMultipleAnswers = v.MaxMultipleAnswers;
                    singleAnswerCondition = v.SingleAnswerCondition;
                    //TODO otherRequired
                }
                //TODO 針對每一題 取得檢核資訊 在針對每個檢核項目去瀏覽我載入的相關題號 選項 之狀態來判斷是否通過檢核
                //TODO 可以有一個檢核集合來放置題號與檢核訊息 直接回傳到前端載入
                //必填檢核
                if (required == "Y")
                {
                    
                    if (String.IsNullOrEmpty(allowNaCondition))
                    {
                        message = "此題必須填答!";
                        return false;
                    }
                    //存在可不做答條件
                    else
                    {
                        Conditions conditions = JsonConvert.DeserializeObject<Conditions>(allowNaCondition);
                        //conditions.QuestionId;

                    }


                }
            }
            return true;
        }

        /// <summary>
        /// 指定問卷編號取得有效的問卷資料
        /// </summary>
        /// <param name="id">問卷編號</param>
        /// <returns> 問卷資料物件 </returns>
        public QuestionnaireEntity GetActiveQuestionnaire(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException();
            }

            try
            {
                QuestionnaireEntity questionnaireEntity = new QuestionnaireEntity(id);
                return questionnaireEntity;
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(ex).Throw();
                return null;
            }
            
        }

        /// <summary>
        /// 指定問卷識別碼取得問卷資料
        /// </summary>
        /// <param name="uid">問卷識別碼</param>
        /// <returns> 問建資料物件 </returns>
        public QuestionnaireEntity GetQuestionnaire(string uid)
        {
            if (uid == null)
            {
                throw new ArgumentNullException();
            }

            try
            {
                QuestionnaireEntity questionnaireEntity = new QuestionnaireEntity(Guid.Parse(uid));
                return questionnaireEntity;
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(ex).Throw();
                return null;
            }
        }
    }
}
