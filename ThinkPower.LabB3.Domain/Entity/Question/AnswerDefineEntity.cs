using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkPower.LabB3.DataAccess.DO;

namespace ThinkPower.LabB3.Domain.Entity.Question
{
    /// <summary>
    /// 問卷答案定義Entity類別
    /// </summary>
    public class AnswerDefineEntity : BaseEntity
    {
        /// <summary>
        /// 將DO載入Entity建構式
        /// </summary>
        /// <param name="dataObject"> 題目選項DO物件 </param>
        public AnswerDefineEntity(QuestionAnswerDefineDO dataObject)
        {
            if (dataObject == null)
            {
                throw new ArgumentNullException();
            }
            GenerateEntity(dataObject);
        }

        /// <summary>
        /// 問卷題目識別碼
        /// </summary>
        public Guid QuestionUid { get; set; }
        /// <summary>
        /// 答案代碼
        /// </summary>
        public string AnswerCode { get; set; }
        /// <summary>
        /// 答案內容描敍
        /// </summary>
        public string AnswerContent { get; set; }
        /// <summary>
        /// 備註說明
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// 是否答題有輸入說明
        /// </summary>
        public string HaveOtherAnswer { get; set; }
        /// <summary>
        /// 答題說明是否為必填
        /// </summary>
        public string NeedOtherAnswer { get; set; }
        /// <summary>
        /// 計分分數
        /// </summary>
        public int? Score { get; set; }
        /// <summary>
        /// 答案項目排序序號
        /// </summary>
        public int? OrderSn { get; set; }

        /// <summary>
        /// 將DO物件載入Entity物件
        /// </summary>
        /// <param name="dataObject">問卷主檔DO物件</param>
        private void GenerateEntity(QuestionAnswerDefineDO dataObject)
        {
            Uid = dataObject.Uid;
            CreateUserId = dataObject.CreateUserId;
            CreateTime = dataObject.CreateTime;
            ModifyUserId = dataObject.ModifyUserId;
            ModifyTime = dataObject.ModifyTime;

            QuestionUid = dataObject.QuestionUid;
            AnswerCode = dataObject.AnswerCode;
            AnswerContent = dataObject.AnswerContent;
            Memo = dataObject.Memo;
            HaveOtherAnswer = dataObject.HaveOtherAnswer;
            NeedOtherAnswer = dataObject.NeedOtherAnswer;
            Score = dataObject.Score;
            OrderSn = dataObject.OrderSn;
        }
    }
}
