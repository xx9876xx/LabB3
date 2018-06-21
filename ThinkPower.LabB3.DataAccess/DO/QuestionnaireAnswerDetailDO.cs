﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkPower.LabB3.DataAccess.DO
{
    /// <summary>
    /// 問卷答題明細DO
    /// </summary>
    public class QuestionnaireAnswerDetailDO
    {
        /// <summary>
        /// 紀錄識別碼
        /// </summary>
        public Guid Uid { get; set; }
        /// <summary>
        /// 問卷答題識別碼
        /// </summary>
        public Guid AnswerUid { get; set; }
        /// <summary>
        /// 問卷題目識別碼
        /// </summary>
        public Guid QuestionUid { get; set; }
        /// <summary>
        /// 答案代碼
        /// </summary>
        public string AnswerCode { get; set; }
        /// <summary>
        /// 答題其他說明
        /// </summary>
        public string OtherAnswer { get; set; }
        /// <summary>
        /// 答題計分分數
        /// </summary>
        public int Score { get; set; }
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
