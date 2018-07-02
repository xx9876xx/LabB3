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
                throw new ArgumentNullException();
            }

            try
            {
                //得到此份問卷的計分資訊
                QuestionnaireEntity questionnaireEntity = GetQuestionnaire(Convert.ToString(answer.QuestUid));

                if (questionnaireEntity.NeedScore == "N")
                {
                    return null;
                    //您的問卷己填答完畢，謝謝您的參與
                }

                if (questionnaireEntity.NeedScore == "Y")
                {
                    int sum = 0;
                    //計分方式為加總
                    if (questionnaireEntity.ScoreKind == "1")
                    {
                        Dictionary<string, string> answerItems = answer.AnswerItems;
                        
                        foreach (var QuestDefineEntity in questionnaireEntity.QuestDefineEntitys)
                        {
                            if (answerItems.Keys.Contains(QuestDefineEntity.QuestionId))
                            {
                                foreach (var answerDefineEntitiy in QuestDefineEntity.AnswerDefineEntities)
                                {
                                    string[] answerItemArray = answerItems[QuestDefineEntity.QuestionId].Split(',');
                                    if (answerItemArray.Contains(answerDefineEntitiy.AnswerCode))
                                    {
                                        sum += answerDefineEntitiy.Score == null ? throw new InvalidCastException() : Convert.ToInt32(answerDefineEntitiy.Score);
                                    }
                                }
                            }
                        }
                    }

                    if (sum > (questionnaireEntity.QuestScore == null ? throw new InvalidCastException() : Convert.ToInt32(questionnaireEntity.QuestScore)))
                    {
                        sum = Convert.ToInt32(questionnaireEntity.QuestScore);
                    }
                    

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
