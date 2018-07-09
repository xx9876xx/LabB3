using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkPower.LabB3.Domain.Entity.Question
{
    /// <summary>
    /// 問卷填答評分結果Entity類別
    /// </summary>
    public class QuestionnaireResultEntity : BaseEntity
    {
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
        /// 回傳畫面訊息
        /// </summary>
        public string ViewMessage { get; set; }
    }
}
