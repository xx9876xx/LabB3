using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkPower.LabB3.DataAccess.DAO;
using ThinkPower.LabB3.DataAccess.DO;

namespace ThinkPower.LabB3.Domain.Entity.Question
{
    /// <summary>
    /// 填答題目選項Entity類別
    /// </summary>
    public class AnswerDetailEntity : BaseEntity
    {
        /// <summary>
        /// 儲存問卷填答主檔資料
        /// </summary>
        //TODO 把該存檔方法移至問卷填答主檔才不會造成重複產生DAO對資料庫過多連線
        //TODO 要把detail的DAO 載入集合 一次處理存檔 才不會過多連線
        public void SaveQuestionnaireAnswer(Guid AnswerUid, string CreateUserId)
        {
            if (AnswerUid == null)
            {
                throw new ArgumentException(nameof(AnswerUid));
            }
            

            QuestionnaireAnswerDetailDO questionnaireAnswerDetailDO = new QuestionnaireAnswerDetailDO
            {
                AnswerUid = AnswerUid,
                QuestionUid = QuestionUid,
                AnswerCode = AnswerCode,
                OtherAnswer = OtherAnswer,
                Score = Score,
                CreateUserId = CreateUserId,
                CreateTime = DateTime.Now
        };

            QuestionnaireAnswerDetailDAO questionnaireAnswerDetailDAO = new QuestionnaireAnswerDetailDAO();
            questionnaireAnswerDetailDAO.Insert(questionnaireAnswerDetailDO);
        }

        /// <summary>
        /// 問卷答題識別碼(參考問卷答題主檔uid)
        /// </summary>
        public Guid AnswerUid { get; set; }

        /// <summary>
        /// 問卷題目識別碼(參考問卷題目定義uid)
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
        public int? Score { get; set; }

        /// <summary>
        /// 題號
        /// </summary>
        public string QuestionId { get; set; }
    }
}
