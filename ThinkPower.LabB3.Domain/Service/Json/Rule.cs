using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkPower.LabB3.Domain.Service.Json
{
    /// <summary>
    /// 特殊規則(特殊條件彼此之間為OR)
    /// </summary>
    public class Rule
    {
        /// <summary>
        /// 特殊條件集合
        /// </summary>
        public IEnumerable<Condition> Conditions { get; set; }
    }
}
