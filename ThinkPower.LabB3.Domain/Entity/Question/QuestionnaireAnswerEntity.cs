using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkPower.LabB3.DataAccess.DO;
using ThinkPower.LabB3.Domain.Entity.Risk;

namespace ThinkPower.LabB3.Domain.Entity.Question
{
    /// <summary>
    /// 問卷答題主檔Entity類別
    /// </summary>
    public class QuestionnaireAnswerEntity : BaseEntity
    {

        /// <summary>
        /// 問卷答題明細Entity類別
        /// </summary>
        /// <param name="riskAnswerEntity"></param>
        public QuestionnaireAnswerEntity(RiskEvaAnswerEntity riskAnswerEntity)
        {
            if (riskAnswerEntity == null)
            {
                throw new ArgumentNullException();
            }

            GenerateEntity(riskAnswerEntity);
        }

        /// <summary>
        /// 問卷答題識別碼
        /// </summary>
        public Guid AnswerUid { get; set; }
        /// <summary>
        /// 問卷題目識別碼
        /// </summary>
        public Guid QuestionUid { get; set; }
        /// <summary>
        /// 答案代碼
        /// </summary>
        public string AnswerCode { get; set; }
        /// <summary>
        /// 答題其他說明
        /// </summary>
        public string OtherAnswer { get; set; }
        /// <summary>
        /// 答題計分分數
        /// </summary>
        public int? Score { get; set; }

        /// <summary>
        /// 問卷識別項
        /// </summary>
        public Guid QuestUid { get; set; }

        /// <summary>
        /// 填寫人員編號 (每次登入隨機產生)
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 問卷填寫來源代號
        /// </summary>
        public string QuestionnaireId { get; set; }

        /// <summary>
        /// 風險問卷填答結果
        /// </summary>
        public Dictionary<string, string> AnswerItems { get; set; }

        private void GenerateEntity(RiskEvaAnswerEntity riskAnswerEntity)
        {
            QuestUid = riskAnswerEntity.QuestUid;
            QuestionnaireId = riskAnswerEntity.QuestionnaireId;
            AnswerItems = riskAnswerEntity.AnswerItems;

        }
    }
}
