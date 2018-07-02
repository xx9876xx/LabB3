using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThinkPower.LabB3.Domain.DTO;
using ThinkPower.LabB3.Domain.Entity.Question;
using ThinkPower.LabB3.Domain.Entity.Risk;

namespace ThinkPower.LabB3.Web.ViewModels
{
    /// <summary>
    /// 問卷展示
    /// </summary>
    public class QuestionnaireDisplayViewModel
    {
        /// <summary>
        /// 問卷ViewModel建構式
        /// </summary>
        /// <param name="riskEvaQuestionnaireEntity"> </param>
        public QuestionnaireDisplayViewModel(RiskEvaQuestionnaireEntity riskEvaQuestionnaireEntity)
        {
            QuestUid = riskEvaQuestionnaireEntity.QuestUid;
            QuestionnaireName = riskEvaQuestionnaireEntity.Name;
            HeadBackgroundImg = riskEvaQuestionnaireEntity.HeadBackgroundImg;
            HeadDescription = riskEvaQuestionnaireEntity.HeadDescription;
            FooterDescription = riskEvaQuestionnaireEntity.FooterDescription;
            QuestDefineEntitys = riskEvaQuestionnaireEntity.QuestDefineEntitys;
        }

        /// <summary>
        /// 問卷主檔問卷名稱
        /// </summary>
        public string QuestionnaireName { get; set; }

        /// <summary>
        /// 頁首底圖網址圖片網址
        /// </summary>
        public string HeadBackgroundImg { get; set; }

        /// <summary>
        /// 頁首說明文字
        /// </summary>
        public string HeadDescription { get; set; }

        /// <summary>
        /// 頁尾說明文字
        /// </summary>
        public string FooterDescription { get; set; }

        /// <summary>
        /// 題目集合
        /// </summary>
        public IEnumerable<QuestDefineEntity> QuestDefineEntitys { get; set; }

        /// <summary>
        /// 問卷識別項
        /// </summary>
        public Guid QuestUid { get; set; }

        /// <summary>
        /// 問卷填寫來源代號
        /// </summary>
        public string TesteeSource { get; set; }

    }

}