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
            QuestionnaireName = riskEvaQuestionnaireEntity.Name;
            HeadBackgroundImg = riskEvaQuestionnaireEntity.HeadBackgroundImg;
            HeadDescription = riskEvaQuestionnaireEntity.HeadDescription;
            FooterDescription = riskEvaQuestionnaireEntity.FooterDescription;
            QuestDefEnumer = riskEvaQuestionnaireEntity.QuestDefEnumer;
            QuestionAnswer = riskEvaQuestionnaireEntity.QuestAnswerPairs;
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
        /// 題目集合
        /// </summary>
        public IEnumerable<QuestDefineEntity> QuestDefEnumer { get; set; }

        /// <summary>
        /// 題目識別碼及選項字典集合
        /// </summary>
        public Dictionary<string, IEnumerable<AnswerDefineEntity>> QuestionAnswer { get; set; }
        
        /// <summary>
        /// 頁尾說明文字
        /// </summary>
        public string FooterDescription { get; set; }
    }

}