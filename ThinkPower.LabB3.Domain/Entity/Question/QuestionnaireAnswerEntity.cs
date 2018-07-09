using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkPower.LabB3.DataAccess.DAO;
using ThinkPower.LabB3.DataAccess.DO;
using ThinkPower.LabB3.Domain.DTO;
using ThinkPower.LabB3.Domain.Entity.Risk;

namespace ThinkPower.LabB3.Domain.Entity.Question
{
    /// <summary>
    /// 問卷填答主檔Entity類別
    /// </summary>
    public class QuestionnaireAnswerEntity : BaseEntity
    {
        /// <summary>
        /// 原生建構式
        /// </summary>
        public QuestionnaireAnswerEntity() { }
        /// <summary>
        /// 問卷填答主檔Entity建構式
        /// </summary>
        /// <param name="riskAnswerEntity">投資風險評估填答明細Entity類別</param>
        public QuestionnaireAnswerEntity(RiskEvaAnswerEntity riskAnswerEntity)
        {
            if (riskAnswerEntity == null)
            {
                throw new ArgumentNullException(nameof(riskAnswerEntity));
            }
            
            QuestUid = riskAnswerEntity.QuestUid;
            QuestAnswerId = string.Format("{0:yyMMddHHmmssfff}", DateTime.Now);
            TesteeId = riskAnswerEntity.TesteeId;
            TesteeSource = riskAnswerEntity.TesteeSource;
            Questions = riskAnswerEntity.Questions;
            CreateUserId = TesteeId;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 儲存問卷填答主檔資料
        /// </summary>
        public void SaveQuestionnaireAnswer()
        {
            QuestionnaireAnswerDO questionnaireAnswerDO = new QuestionnaireAnswerDO
            {
                Uid = Uid,
                QuestUid = QuestUid,
                QuestAnswerId = QuestAnswerId,
                TesteeId = TesteeId,
                QuestScore = QuestScore,
                ActualScore = ActualScore,
                TesteeSource = TesteeSource,
                CreateUserId = CreateUserId,
                CreateTime = CreateTime
            };

            //儲存問卷主檔
            QuestionnaireAnswerDAO questionnaireAnswerDAO = new QuestionnaireAnswerDAO();
            string answerUid = questionnaireAnswerDAO.Insert(questionnaireAnswerDO);
            //TODO 先把整理資料的方法做掉，最後再把儲存行為一起處理，相間隔的時間會比較短比較不會出錯
            List<QuestionnaireAnswerDetailDO> answerDetails = new List<QuestionnaireAnswerDetailDO>();
            foreach (var answerDetail in Questions)
            {
                answerDetails.Add(answerDetail.SaveQuestionnaireAnswer(Guid.Parse(answerUid), CreateUserId));
            }
            //儲存答題明細集合
            QuestionnaireAnswerDetailDAO questionnaireAnswerDetailDAO = new QuestionnaireAnswerDetailDAO();
            questionnaireAnswerDetailDAO.Insert(answerDetails);
        }

        /// <summary>
        /// 問卷識別碼
        /// </summary>
        public Guid QuestUid { get; set; }

        /// <summary>
        /// 問卷答題編號(依目前西元年月日(6)+時分秒(6)+隨機數(3)編碼)
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
        public IEnumerable<AnswerDetailEntity> Questions { get; set; }

        /// <summary>
        /// 回傳畫面訊息
        /// </summary>
        public string ViewMessage { get; set; }
    }
}
