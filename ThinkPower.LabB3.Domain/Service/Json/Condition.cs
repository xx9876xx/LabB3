using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkPower.LabB3.Domain.Service.Json
{
    public class Condition
    {
        //TODO Class是非集合型態 不應該加s
        //TODO Service用就放在Service裡面 可以自己創Condition目錄
        //TODO 外面還有一層Conditions的物件 放Condition集合
        /// <summary>
        /// 條件題號
        /// </summary>
        public string QuestionId { get; set; }

        //TODO 盡量不要用陣列因為要給初始直會不夠彈性 可以用IEnumrable
        /// <summary>
        /// 條件選項
        /// </summary>
        public string[] AnswerCode { get; set; }
    }
}
