using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThinkPower.LabB3.Domain.Resources
{
    /// <summary>
    /// 條件物件
    /// </summary>
    public class Conditions
    {
        //TODO Class是非集合型態 不應該加s
        //TODO Service用就放在Service裡面 可以自己創Condition目錄
        //TODO 外面還有一層Conditions的物件 放Condition集合
        /// <summary>
        /// 條件題號
        /// </summary>
        public string QuestionId { get; set; }

        /// <summary>
        /// 條件選項
        /// </summary>
        public string[] AnswerCode { get; set; }
        

    }
}
