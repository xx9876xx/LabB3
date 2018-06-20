using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkPower.LabB3.Domain;
using ThinkPower.LabB3.DataAccess;
using ThinkPower.LabB3.Domain.DTO;
using NLog;

namespace ThinkPower.LabB3.Domain.Service
{
    class QuestionnaireService
    {
        /// <summary>
        /// 計算問卷填答得分
        /// </summary>
        /// <param name="answer">問卷填答資料</param>
        /// <returns></returns>
        public QuestionnaireResult Calculate(QuestionnaireAnswer answer)
        {
            return null;
        }
        /// <summary>
        /// 取得有效的問卷資料
        /// </summary>
        /// <param name="id">問卷編號</param>
        /// <returns></returns>
        public Questionnaire GetActiveQuestionnaire(string id)
        {
            return null;
        }
        /// <summary>
        /// 取得問卷資料
        /// </summary>
        /// <param name="uid">問卷識別碼</param>
        /// <returns></returns>
        public Questionnaire GetQuestionnaire(string uid)
        {
            return null;
        }
    }
}
