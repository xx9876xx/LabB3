using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkPower.LabB3.DataAccess.DO
{
    /// <summary>
    /// 問卷主檔DO
    /// </summary>
    public class QuestionnaireDO
    {
        /// <summary>
        /// 紀錄識別碼
        /// </summary>
        public Guid Uid { get; set; }
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
        /// 備註說明
        /// </summary>
        public string Memo { get; set; }
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
