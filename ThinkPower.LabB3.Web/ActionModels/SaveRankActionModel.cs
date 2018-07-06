using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThinkPower.LabB3.Web.ActionModels
{
    /// <summary>
    /// 投資風險評估資料
    /// </summary>
    public class SaveRankActionModel
    {
        /// <summary>
        /// 風險評估分數
        /// </summary>
        public int RiskScore { get; set; }

        /// <summary>
        /// 回傳訊息
        /// </summary>
        public string message { get; set; }

    }
}