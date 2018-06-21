using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkPower.LabB3.Domain.Entity
{
    public class BaseEntity
    {
        public Guid Uid { get; set; }
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
