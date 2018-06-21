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
    public class RiskEvaluationService
    {
        /// <summary>
        /// 評估投資風險等級
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public RiskEvaResult EvaluateRiskRank(RiskEvaAnswer uid)
        {
            return null;
        }
        /// <summary>
        /// 依紀錄識別碼取得風險評估資料
        /// </summary>
        /// <param name="uid">紀錄識別碼</param>
        /// <returns></returns>
        public RiskEvaluation Get(string uid)
        {
            return null;
        }
        /// <summary>
        /// 取得風險評估問卷資料
        /// </summary>
        /// <param name="questId">問卷編號</param>
        /// <returns></returns>
        public RiskEvaQuestionnaire GetRiskQuestionnaire(string questId)
        {
            QuestionnaireService questService = new QuestionnaireService();
            Questionnaire questionnaire = questService.GetActiveQuestionnaire(questId);
            RiskEvaQuestionnaire result = new RiskEvaQuestionnaire(questionnaire);
            return result;
        }
        /// <summary>
        /// 取得暫存的風險評估資料
        /// </summary>
        /// <param name="key">暫存資料識別碼</param>
        /// <returns></returns>
        public RiskEvaResult GetRiskResult(string key)
        {
            return null;
        }
        /// <summary>
        /// 依投資風險屬性取得可投資的風險等級項目
        /// </summary>
        /// <param name="riskRankKind">投資風險屬性</param>
        /// <returns></returns>
        public List<string> RiskRank(string riskRankKind)
        {
            return null;
        }
        /// <summary>
        /// 儲存評估投資風險評估結果資料
        /// </summary>
        /// <param name="riskResultId">風險評估結果識別代號</param>
        public void SaveRiskRank(string riskResultId)
        {

        }
    }
}
