﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkPower.LabB3.Domain.Entity.Question
{
    /// <summary>
    /// 問卷題目填答Entity類別
    /// </summary>
    public class AnswerDetailEntity : BaseEntity
    {
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
    }
}
