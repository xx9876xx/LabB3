using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkPower.LabB3.Domain.DTO
{
    /// <summary>
    /// 風險評估問卷資料DTO
    /// </summary>
    public class RiskEvaQuestionnaire
    {

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
        public string QuestScore { get; set; }
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
        public RiskEvaQuestionnaire()
        {
        }
        public RiskEvaQuestionnaire(Questionnaire questionnaire)
        {
            QuestId = questionnaire.QuestId;
            Name = questionnaire.Name;
            QuestScore = questionnaire.QuestScore;
            ScoreKind = questionnaire.ScoreKind;
            HeadBackgroundImg = questionnaire.HeadBackgroundImg;
            HeadDescription = questionnaire.HeadDescription;
            FooterDescription = questionnaire.FooterDescription;
        }
    }
}
