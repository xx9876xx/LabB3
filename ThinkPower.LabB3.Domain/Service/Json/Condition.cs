using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkPower.LabB3.Domain.Service.Json
{
    /// <summary>
    /// 特殊條件
    /// </summary>
    public class Condition
    {        
        /// <summary>
        /// 條件題號
        /// </summary>
        public string QuestionId { get; set; }
        
        /// <summary>
        /// 條件選項集合
        /// </summary>
        public IEnumerable<string> AnswerCode { get; set; }
    }
}
