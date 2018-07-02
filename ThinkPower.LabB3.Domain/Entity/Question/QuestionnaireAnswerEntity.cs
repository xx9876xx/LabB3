using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkPower.LabB3.DataAccess.DAO;
using ThinkPower.LabB3.DataAccess.DO;
using ThinkPower.LabB3.Domain.Entity.Risk;

namespace ThinkPower.LabB3.Domain.Entity.Question
{
    /// <summary>
    /// 問卷填答主檔Entity類別
    /// </summary>
    public class QuestionnaireAnswerEntity : BaseEntity
    {

        /// <summary>
        /// 問卷填答主檔Entity類別
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

        public bool SaveQuestionnaireAnswerData()
        {
            QuestionnaireAnswerDO questionnaireAnswerDO = new QuestionnaireAnswerDO();
            questionnaireAnswerDO.Uid = Uid;
            questionnaireAnswerDO.QuestUid = QuestUid;
            questionnaireAnswerDO.QuestAnswerId = QuestAnswerId;
            questionnaireAnswerDO.TesteeId = TesteeId;
            questionnaireAnswerDO.QuestScore = QuestScore;
            questionnaireAnswerDO.ActualScore = ActualScore;
            questionnaireAnswerDO.TesteeSource = TesteeSource;
            questionnaireAnswerDO.CreateUserId = CreateUserId;
            questionnaireAnswerDO.CreateTime = CreateTime;

            QuestionnaireAnswerDetailDAO questionnaireAnswerDetailDAO = new QuestionnaireAnswerDetailDAO();
            return questionnaireAnswerDetailDAO.SetQuestionnaireData(questionnaireAnswerDO);
        }

        /// <summary>
        /// 問卷識別碼
        /// </summary>
        public Guid QuestUid { get; set; }
        /// <summary>
        /// 問卷答題編號
        /// </summary>
        public string QuestAnswerId { get; set; }
        /// <summary>
        /// 填寫人員編號
        /// </summary>
        public string TesteeId { get; set; }
        /// <summary>
        /// 問卷總分
        /// </summary>
        public int? QuestScore { get; set; }
        /// <summary>
        /// 問卷得分
        /// </summary>
        public int? ActualScore { get; set; }
        /// <summary>
        /// 問卷填寫來源代號
        /// </summary>
        public string TesteeSource { get; set; }
        
        /// <summary>
        /// 風險問卷填答結果
        /// </summary>
        public Dictionary<string, string> AnswerItems { get; set; }

        private void GenerateEntity(RiskEvaAnswerEntity riskAnswerEntity)
        {
            Uid = Guid.NewGuid();
            QuestUid = riskAnswerEntity.QuestUid;
            QuestAnswerId = string.Format("{0:yyMMddHHmmssfff}", DateTime.Now);
            TesteeId = string.Format("{0:yyyydd}", DateTime.Now);

            TesteeSource = riskAnswerEntity.TesteeSource;
            AnswerItems = riskAnswerEntity.AnswerItems;
            CreateUserId = TesteeId;
            CreateTime = DateTime.Now;
        }
    }
}
