using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkPower.LabB3.DataAccess.DO
{
    /// <summary>
    /// 問卷答題主檔DO
    /// </summary>
    public class QuestionnaireAnswerDO
    {
        /// <summary>
        /// 紀錄識別碼
        /// </summary>
        public Guid Uid { get; set; }
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
        public int QuestScore { get; set; }
        /// <summary>
        /// 問卷得分
        /// </summary>
        public int ActualScore { get; set; }
        /// <summary>
        /// 問卷填寫來源代號
        /// </summary>
        public string TesteeSource { get; set; }
        /// <summary>
        /// 建立人員代號
        /// </summary>
        public string CreateUserId { get; set; }
        /// <summary>
        /// 建立時間
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 修改人員代號
        /// </summary>
        public string ModifyUserId { get; set; }
        /// <summary>
        /// 修改時間
        /// </summary>
        public DateTime? ModifyTime { get; set; }
    }
}
