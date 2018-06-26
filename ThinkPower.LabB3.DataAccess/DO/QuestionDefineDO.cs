﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkPower.LabB3.DataAccess.DO
{
    /// <summary>
    /// 問卷題目定義DO
    /// </summary>
    public class QuestionDefineDO
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
        /// 題目編號
        /// </summary>
        public string QuestionId { get; set; }

        /// <summary>
        /// 題目內容描述
        /// </summary>
        public string QuestionContent { get; set; }

        /// <summary>
        /// 是否必答
        /// </summary>
        public string NeedAnswer { get; set; }

        /// <summary>
        /// 可不做答條件
        /// </summary>
        public string AllowNaCondition { get; set; }

        /// <summary>
        /// 答題型態
        /// </summary>
        public string AnswerType { get; set; }

        /// <summary>
        /// 複選最少答項數
        /// </summary>
        public int? MinMultipleAnswers { get; set; }

        /// <summary>
        /// 複選最多答項數
        /// </summary>
        public int? MaxMultipleAnswers { get; set; }

        /// <summary>
        /// 複選限制單一做答條件
        /// </summary>
        public string SingleAnswerCondition { get; set; }

        /// <summary>
        /// 計分種類
        /// </summary>
        public string CountScoreType { get; set; }

        /// <summary>
        /// 備註說明
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 題目排序序號
        /// </summary>
        public int? OrderSn { get; set; }

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
