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
using ThinkPower.LabB3.Domain.Service.Json;

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
                QuestionnaireAnswerEntity answerResult = new QuestionnaireAnswerEntity();
                //此份問卷的計分資訊
                QuestionnaireEntity questionnaireEntity = GetQuestionnaire(Convert.ToString(answer.QuestUid));
                //整理
                answerResult = SetAnswerDetail(answer, questionnaireEntity);
                //檢核
                if (!AnswerValidate(answerResult, questionnaireEntity))
                {
                    throw new ApplicationException("填答內容有誤!");
                }
                //計分
                answerResult = CaculatePoint(answerResult ,questionnaireEntity);
                //存檔
                answerResult.SaveQuestionnaireAnswer();
                QuestionnaireResultEntity resultEntity = new QuestionnaireResultEntity()
                {
                    QuestUid = answerResult.QuestUid,
                    QuestAnswerId = answerResult.QuestAnswerId,
                    TesteeId = answerResult.TesteeId,
                    QuestScore = answerResult.QuestScore,
                    ActualScore = answerResult.ActualScore,
                    ViewMessage = answerResult.ViewMessage
                };
                
                return resultEntity;
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(ex).Throw();
                return null;
            }
        }

        /// <summary>
        /// 計分邏輯
        /// </summary>
        /// <param name="answerResult"> 填答結果 </param>
        /// <param name="questionnaireEntity">問卷資訊</param>
        /// <returns> 計分後的填答結果</returns>
        private QuestionnaireAnswerEntity CaculatePoint(QuestionnaireAnswerEntity answerResult, QuestionnaireEntity questionnaireEntity)
        {
            //填入該問卷總分
            answerResult.QuestScore = questionnaireEntity.QuestScore;
            //該問卷需計分
            if (questionnaireEntity.NeedScore == "Y")
            {
                //初始化總分
                answerResult.ActualScore = 0;
                foreach (var questionDef in questionnaireEntity.QuestDefineEntitys)
                {
                    var questionScore = from question in answerResult.Questions
                                        where questionDef.QuestionId == question.QuestionId
                                        select question.Score;
                    //計分方式
                    switch (questionnaireEntity.ScoreKind)
                    {
                        //加總
                        case "1":
                            answerResult.ActualScore += questionScore.Sum(e => e.Value);
                            break;
                        //取最高
                        case "2":
                            answerResult.ActualScore += questionScore.Max(e => e.Value);
                            break;
                        //取最低
                        case "3":
                            answerResult.ActualScore += questionScore.Min(e => e.Value);
                            break;
                        //平均
                        case "4":
                            answerResult.ActualScore += (int)questionScore.Average(e => e.Value);
                            break;
                    }
                }
                //檢查有沒有超過問卷總分
                if (answerResult.ActualScore.Value > answerResult.QuestScore.Value)
                {
                    answerResult.ActualScore = answerResult.QuestScore;
                }
            }
            else
            {
                answerResult.ActualScore = null;
                answerResult.ViewMessage = "您的問卷己填答完畢，謝謝您的參與!";
            }

            return answerResult;
        }
    
        /// <summary>
        /// 整理問卷填答項目各屬性資料
        /// </summary>
        /// <param name="answer"> 填答問卷主檔Entity </param>
        /// <param name="questionnaireEntity"> 問卷計分資訊 </param>
        /// <returns>回傳填答結果</returns>
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
                    throw new InvalidOperationException(nameof(answerDefine));
                }
                else
                {
                    question.QuestionUid = answerDefine.Uid;
                }
                
                var scoreQuery = (from ans in answerDefine.AnswerDefineEntities
                                 where question.AnswerCode == ans.AnswerCode
                                 select ans.Score).FirstOrDefault();
                //填入各填答的選項分數
                if (!scoreQuery.HasValue)
                {
                    throw new InvalidOperationException(nameof(scoreQuery));
                }
                else
                {
                    question.Score = scoreQuery.HasValue ? question.Score = scoreQuery.Value : null;
                }
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
            //檢核訊息(題號/檢核訊息)
            Dictionary<string,string> message = new Dictionary<string,string>();

            
            foreach (var question in answer.Questions)
            {
                var questValidate = (from quest in questionnaireEntity.QuestDefineEntitys
                                     from ans in quest.AnswerDefineEntities
                                     where quest.Uid == question.QuestionUid
                                     where ans.AnswerCode == question.AnswerCode
                                     select new
                                    {
                                        quest.AnswerType,
                                        quest.NeedAnswer,
                                        quest.AllowNaCondition,
                                        quest.MinMultipleAnswers,
                                        quest.MaxMultipleAnswers,
                                        quest.SingleAnswerCondition,
                                        quest.AnswerDefineEntities,
                                        ans.HaveOtherAnswer,
                                        ans.NeedOtherAnswer
                                    }).FirstOrDefault();

                if (questValidate == null)
                {
                    throw new ApplicationException("資料庫無「" + nameof(question.AnswerUid) + "」題目之檢核資料");
                }
                else
                {
                    type = questValidate.AnswerType;
                    required = questValidate.NeedAnswer;
                    allowNaCondition = questValidate.AllowNaCondition;
                    minMultipleAnswers = questValidate.MinMultipleAnswers;
                    maxMultipleAnswers = questValidate.MaxMultipleAnswers;
                    singleAnswerCondition = questValidate.SingleAnswerCondition;
                    otherRequired = questValidate.NeedOtherAnswer;
                }

                switch (type)
                {
                    case "F":
                        FieldValidate(question,required, allowNaCondition,message);
                        break;
                    case "S":
                        break;
                    case "M":
                        break;
                }


                //必填檢核
                if (required == "Y")
                {
                    if (String.IsNullOrEmpty(allowNaCondition))
                    {
                        //message = "此題必須填答!";
                        //return false;
                    }
                    //存在可不做答條件
                    else
                    {
                        Rule conditions = JsonConvert.DeserializeObject<Rule> (allowNaCondition);
                        foreach (var caondition in conditions.Conditions)
                        {
                            
                        }
                    }
                }
            }
            return true;
        }

        private void FieldValidate(AnswerDetailEntity question,string required, string allowNaCondition,Dictionary<string,string>message)
        {
            //必填檢核
            if (required == "Y")
            {
                if (String.IsNullOrEmpty(allowNaCondition))
                {
                    if (question.OtherAnswer == "")
                    {
                        message.Add(question.QuestionId, "此題必須填答!");
                    }
                }
                //存在可不做答條件
                else
                {
                    Rule conditions = JsonConvert.DeserializeObject<Rule>(allowNaCondition);
                    foreach (var caondition in conditions.Conditions)
                    {

                    }
                }
            }
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
