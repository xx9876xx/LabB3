using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkPower.LabB3.DataAccess.DO;

namespace ThinkPower.LabB3.Domain.Entity.Question
{
    /// <summary>
    /// 問卷答題主檔Entity類別
    /// </summary>
    public class QuestionnaireAnswerEntity : BaseEntity
    {

        /// <summary>
        /// 問卷答題明細Entity類別
        /// </summary>
        /// <param name="answers"></param>
        public QuestionnaireAnswerEntity(string answers)
        {
            if (answers == null)
            {
                throw new ArgumentNullException();
            }

            GenerateEntity(answers);
        }

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
        public int? Score { get; set; }

        private void GenerateEntity(string a)
        {

        }
    }
}
