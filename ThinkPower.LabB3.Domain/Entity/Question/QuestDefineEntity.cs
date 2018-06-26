﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkPower.LabB3.DataAccess.DO;

namespace ThinkPower.LabB3.Domain.Entity.Question
{
    /// <summary>
    /// 問卷題目定義Entity類別
    /// </summary>
    public class QuestDefineEntity : BaseEntity
    {
        /// <summary>
        /// 將DO載入Entity建構式
        /// </summary>
        /// <param name="dataObject"> 題目選項DO物件 </param>
        public QuestDefineEntity(QuestionDefineDO dataObject)
        {
            if (dataObject == null)
            {
                throw new ArgumentNullException();
            }

            generateEntity(dataObject);
        }

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
        /// 將DO物件載入Entity物件
        /// </summary>
        /// <param name="dataObject">題目DO物件</param>
        /// <returns>載入成功/失敗</returns>
        private void generateEntity(QuestionDefineDO dataObject)
        {
            Uid = dataObject.Uid;
            CreateUserId = dataObject.CreateUserId;
            CreateTime = dataObject.CreateTime;
            ModifyUserId = dataObject.ModifyUserId;
            ModifyTime = dataObject.ModifyTime;

            QuestUid = dataObject.QuestUid;
            QuestionId = dataObject.QuestionId;
            QuestionContent = dataObject.QuestionContent;
            NeedAnswer = dataObject.NeedAnswer;
            AllowNaCondition = dataObject.AllowNaCondition;
            AnswerType = dataObject.AnswerType;
            MinMultipleAnswers = dataObject.MinMultipleAnswers;
            MaxMultipleAnswers = dataObject.MaxMultipleAnswers;
            SingleAnswerCondition = dataObject.SingleAnswerCondition;
            CountScoreType = dataObject.CountScoreType;
            Memo = dataObject.Memo;
            OrderSn = dataObject.OrderSn;
        }
    }
}
