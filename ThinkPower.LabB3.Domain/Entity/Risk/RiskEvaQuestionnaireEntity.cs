using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkPower.LabB3.Domain.Entity.Question;

namespace ThinkPower.LabB3.Domain.Entity.Risk
{
    /// <summary>
    /// 投資風險評估問卷Entity類別
    /// </summary>
    public class RiskEvaQuestionnaireEntity : BaseEntity
    {
        public RiskEvaQuestionnaireEntity(QuestionnaireEntity questionnaireEntity)
        {
            if (questionnaireEntity == null)
            {
                throw new ArgumentNullException();
            }            
            GenerateEntity(questionnaireEntity);
        }

        /// <summary>
        /// 問卷識別碼
        /// </summary>
        public Guid QuestUid { get; set; }

        /// <summary>
        /// 問卷編號
        /// </summary>
        public string QuestId { get; set; }
        /// <summary>
        /// 問卷名稱
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 問卷總分數
        /// </summary>
        public int? QuestScore { get; set; }
        /// <summary>
        /// 計分方式
        /// </summary>
        public string ScoreKind { get; set; }
        /// <summary>
        /// 問卷頁首底圖網址
        /// </summary>
        public string HeadBackgroundImg { get; set; }
        /// <summary>
        /// 問卷頁首描敍內容
        /// </summary>
        public string HeadDescription { get; set; }
        /// <summary>
        /// 問卷頁尾描敍內容
        /// </summary>
        public string FooterDescription { get; set; }
        /// <summary>
        /// 題目集合
        /// </summary>
        public IEnumerable<QuestDefineEntity> QuestDefEnumer { get; set; }
        /// <summary>
        /// 題目集合
        /// </summary>
        public IEnumerable<QuestDefineEntity> QuestDefineEntitys { get; set; }

        private void GenerateEntity(QuestionnaireEntity questionnaireEntity)
        {
            QuestUid = questionnaireEntity.Uid;
            QuestId = questionnaireEntity.QuestId;
            Name = questionnaireEntity.Name;
            QuestScore = questionnaireEntity.QuestScore;
            ScoreKind = questionnaireEntity.ScoreKind;
            HeadBackgroundImg = questionnaireEntity.HeadBackgroundImg;
            HeadDescription = questionnaireEntity.HeadDescription;
            FooterDescription = questionnaireEntity.FooterDescription;
            QuestDefEnumer = questionnaireEntity.QuestDefineEntitys;
            QuestDefineEntitys = questionnaireEntity.QuestDefineEntitys;
        }
    }
}
