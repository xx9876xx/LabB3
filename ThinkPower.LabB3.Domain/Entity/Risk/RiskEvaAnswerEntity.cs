using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        /// <summary>
        /// 問卷填寫來源代號
        /// </summary>
        public string TesteeSource { get; set; }

        /// <summary>
        /// 風險問卷填答結果
        /// </summary>
        public Dictionary<string, string> AnswerItems { get; set; }

    }
}
