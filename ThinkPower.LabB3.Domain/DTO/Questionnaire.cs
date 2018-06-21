using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkPower.LabB3.DataAccess.DO;
using ThinkPower.LabB3.Domain.Entity.Question;

namespace ThinkPower.LabB3.Domain.DTO
{
    /// <summary>
    /// 有效問卷資料DTO
    /// </summary>
    public class Questionnaire
    {
        /// <summary>
        /// 問卷編號
        /// </summary>
        public string QuestId { get; set; }
        /// <summary>
        /// 問卷版本
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 問卷種類
        /// </summary>
        public string Kind { get; set; }
        /// <summary>
        /// 問卷名稱
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 生效日期
        /// </summary>
        public DateTime? Ondate { get; set; }
        /// <summary>
        /// 生效截止日期
        /// </summary>
        public DateTime? Offdate { get; set; }
        /// <summary>
        /// 是否計分
        /// </summary>
        public string NeedScore { get; set; }
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

        public Questionnaire()
        {
        }
        public Questionnaire(QuestionnaireEntity questionnaireDO)
        {
            QuestId = questionnaireDO.QuestId;
            Version = questionnaireDO.Version;
            Kind = questionnaireDO.Kind;
            Name = questionnaireDO.Name;
            Ondate = questionnaireDO.Ondate;
            Offdate = questionnaireDO.Offdate;
            NeedScore = questionnaireDO.NeedScore;
            QuestScore = questionnaireDO.QuestScore;
            ScoreKind = questionnaireDO.ScoreKind;
            HeadBackgroundImg = questionnaireDO.HeadBackgroundImg;
            HeadDescription = questionnaireDO.HeadDescription;
            FooterDescription = questionnaireDO.FooterDescription;
        }
    }
}
