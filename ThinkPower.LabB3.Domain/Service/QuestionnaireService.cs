﻿using System;
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
                answerResult = CaculatePoint(answerResult, questionnaireEntity);
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
                //暫存的總分
                List<int> actualScore = new List<int>();
                foreach (var questionDef in questionnaireEntity.QuestDefineEntitys)
                {
                    var questionScore = from question in answerResult.Questions
                                        where questionDef.QuestionId == question.QuestionId
                                        select question.Score;
                    int tmp = 0;
                    //單題計分方式 
                    switch (questionDef.CountScoreType)
                    {
                        //加總
                        case "1":
                            tmp += questionScore.Sum(e => e.Value);
                            actualScore.Add(tmp);
                            break;
                        //取最高
                        case "2":
                            tmp += questionScore.Max(e => e.Value);
                            actualScore.Add(tmp);
                            break;
                        //取最低
                        case "3":
                            tmp += questionScore.Min(e => e.Value);
                            actualScore.Add(tmp);
                            break;
                        //平均
                        case "4":
                            tmp += (int)questionScore.Average(e => e.Value);
                            actualScore.Add(tmp);
                            break;
                    }
                }
                //總問卷
                switch (questionnaireEntity.ScoreKind)
                {
                    case "1":
                        answerResult.ActualScore += actualScore.Sum(e => e); ;
                        break;
                    case "2":
                        answerResult.ActualScore += actualScore.Max(e => e);
                        break;
                    case "3":
                        answerResult.ActualScore += actualScore.Min(e => e);
                        break;
                    case "4":
                        answerResult.ActualScore += (int)actualScore.Average(e => e);
                        break;
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

            //TODO 依這份問卷的所有檢核規則 去 檢核所有欄位
            foreach (var QuestDefine in questionnaireEntity.QuestDefineEntitys)
            {
                switch (QuestDefine.AnswerType)
                {
                    case "F":
                        if (QuestDefine.NeedAnswer == "Y" && !ConditionCheck(QuestDefine.AllowNaCondition, answer.Questions))
                        {

                        }

                        break;
                    case "S":

                        break;
                    case "M":

                        break;
                }
                //其他說明必填檢核
                foreach (var AnswerDefine in QuestDefine.AnswerDefineEntities)
                {

                }
            }

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
            Dictionary<string, string> message = new Dictionary<string, string>();


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
                        Rule conditions = JsonConvert.DeserializeObject<Rule>(allowNaCondition);
                        foreach (var caondition in conditions.Conditions)
                        {

                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 特殊條件Json解析
        /// </summary>
        /// <param name="conditionString">Json字串</param>
        /// <returns>特殊條件是否成立</returns>
        public bool ConditionCheck(string conditionString, IEnumerable<AnswerDetailEntity> questions)
        {
            bool check = false;
            
            //Json字串拆解
            Rule conditions = JsonConvert.DeserializeObject<Rule>(conditionString);

            foreach (var condition in conditions.Conditions)
            {
                int count = 0;
                foreach (var question in questions)
                {
                    //若填答題號與特殊條件題號相等
                    if (condition.QuestionId == question.QuestionId)
                    {
                        //填答選項與特殊條件選項相等
                        if (condition.AnswerCode.Contains(question.AnswerCode))
                        {
                            count++;
                            //同一題目之選項皆勾選
                            if (count == condition.AnswerCode.Count())
                            {
                                check = true;
                            }  
                        }
                    }
                }
            }
            return check;
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
        /// 指定問卷識別碼取得問卷資料c
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
