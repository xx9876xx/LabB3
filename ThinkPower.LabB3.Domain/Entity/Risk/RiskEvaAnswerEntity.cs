using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkPower.LabB3.Domain.DTO;
using ThinkPower.LabB3.Domain.Entity.Question;

namespace ThinkPower.LabB3.Domain.Entity.Risk
{
    /// <summary>
    /// 投資風險評估填答明細Entity類別
    /// </summary>
    public class RiskEvaAnswerEntity : BaseEntity
    {
        /// <summary>
        /// 問卷識別項
        /// </summary>
        public Guid QuestUid { get; set; }

        //TODO TesteeId先寫死在這
        /// <summary>
        /// 填寫人員編號
        /// </summary>
        public string TesteeId { get { return String.Format("{0:yyyydd}", DateTime.Now); } set { TesteeId = value; } }

        /// <summary>
        /// 問卷填寫來源代號
        /// </summary>
        public string TesteeSource { get; set; }

        /// <summary>
        /// 風險問卷填答結果
        /// </summary>
        public IEnumerable<AnswerDetailEntity> Questions { get; set; }
        
    }
}
