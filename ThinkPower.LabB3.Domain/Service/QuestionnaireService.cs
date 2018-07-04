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

namespace ThinkPower.LabB3.Domain.Service
{
    //TODO 補interface的公開方法
    //TODO 計分呼叫檢核的private方法再執行計分，如果Entity有足夠資訊(先拿到檢核資訊、再做判斷),可以創一個檢核function，讓Serice呼叫檢核方法來判斷流程是否可以繼續往下走，也可以直接先在Entity收資料直接做檢核
    //TODO 重構一下Calculate裡面的同樣方法和資料物件導向化 
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
                //此份問卷的計分資訊
                QuestionnaireEntity questionnaireEntity = GetQuestionnaire(Convert.ToString(answer.QuestUid));

                QuestionnaireAnswerEntity answerResult = SetAnswerDetail(answer, questionnaireEntity);

                if (questionnaireEntity.NeedScore == "Y")
                {
                    int sum = 0;
                    switch (questionnaireEntity.ScoreKind)
                    {
                        //加總
                        case "1":
                            break;
                        //取最高
                        case "2":
                            break;
                        //取最低
                        case "3":
                            break;
                        //平均
                        case "4":
                            break;

                    }
                        foreach (var question in answerResult.Questions)
                        {
                            sum += question.Score.Value;
                        }
                    

                    //問卷得分
                    answer.ActualScore = sum;
                    //問卷總分
                    answer.QuestScore = questionnaireEntity.QuestScore;


                }




                return null;
                //QuestionnaireEntity questionnaireEntity = GetQuestionnaire(Convert.ToString(answer.QuestUid));
                ////填答項目集合
                ////欲儲存的問卷填答項目Entity
                //AnswerDetailEntity answerDetail = new AnswerDetailEntity();

                ////TODO 把題目選項疊代統整成一個method

                
                //    //您的問卷己填答完畢，謝謝您的參與

                //    //問卷得分
                //    answer.ActualScore = sum;

                //    if (sum > (questionnaireEntity.QuestScore == null ? throw new InvalidCastException() : Convert.ToInt32(questionnaireEntity.QuestScore)))
                //    {
                //        sum = Convert.ToInt32(questionnaireEntity.QuestScore);
                //    }

                //    //問卷總分
                //    answer.QuestScore = sum;

                //    answer.SaveQuestionnaireAnswer();
                //    answerDetail.SaveQuestionnaireAnswer();

            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(ex).Throw();
                return null;
            }


            
        }
            
    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="answer"> 填答問卷主檔Entity </param>
        /// <param name="questionnaireEntity"> 問卷計分資訊 </param>
        /// <returns></returns>
        private QuestionnaireAnswerEntity SetAnswerDetail(QuestionnaireAnswerEntity answer, QuestionnaireEntity questionnaireEntity)
        {
            //針對每個填答
            foreach (var question in answer.Questions)
            {
                var AnswerDefines = from quest in questionnaireEntity.QuestDefineEntitys
                                    where (quest.QuestionId == question.QuestionId)
                                    select new {quest.AnswerDefineEntities};

                //填入題目Uid
                question.QuestionUid = AnswerDefines.First().QuestionUid;

                string[]answers = question.AnswerCode.Split(',');
                var scoreQuery = from ans in AnswerDefines
                                 where (answers.Contains(ans.AnswerCode))
                                 select new { ans.Score };
                if (questionnaireEntity.NeedScore == "Y")
                {
                    switch (questionnaireEntity.ScoreKind)
                    {
                        //加總
                        case "1":
                            question.Score = scoreQuery.Sum(e => e.Score);
                            break;
                        //取最高
                        case "2":
                            question.Score = scoreQuery.OrderByDescending(e => e.Score).First().Score;
                            break;
                        //取最低
                        case "3":
                            question.Score = scoreQuery.OrderBy(e => e.Score).First().Score;
                            break;
                        //平均
                        case "4":
                            question.Score = (int)scoreQuery.Average(e => e.Score);
                            break;
                    }
                }
                
                
            }
            return answer;
        }

        /// <summary>
        /// 取得有效的問卷資料
        /// </summary>
        /// <param name="id">問卷編號</param>
        /// <returns></returns>
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
        /// 取得問卷資料
        /// </summary>
        /// <param name="uid">問卷識別碼</param>
        /// <returns></returns>
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
