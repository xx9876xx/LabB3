﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkPower.LabB3.DataAccess.DO
{
    /// <summary>
    /// 投資風險等級DO
    /// </summary>
    public class RiskRankDO
    {
        /// <summary>
        /// 紀錄識別碼
        /// </summary>
        public Guid Uid { get; set; }
        /// <summary>
        /// 風險評估項目代號
        /// </summary>
        public string RiskEvaId { get; set; }
        /// <summary>
        /// 起始分數
        /// </summary>
        public int MinScore { get; set; }
        /// <summary>
        /// 截止分數
        /// </summary>
        public int MaxScore { get; set; }
        /// <summary>
        /// 投資屬性類型
        /// </summary>
        public string RiskRankKind { get; set; }
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
