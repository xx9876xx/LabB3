using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThinkPower.LabB3.Web.ActionModels
{
    public class EvaluationRankActionModel
    {
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
        /// 問卷題目
        /// </summary>
        public List<string> Question { get; set; }
        /// <summary>
        /// 題目答案項目
        /// </summary>
        public dynamic Answer { get; set; }
        /// <summary>
        /// 頁尾說明文字
        /// </summary>
        public string FooterDescription { get; set; }
        
    }
}