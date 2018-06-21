using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThinkPower.LabB3.Domain.Service;
using ThinkPower.LabB3.Web.ActionModels;
using ThinkPower.LabB3.Web.ViewModels;
using ThinkPower.LabB3.Domain.DTO;

namespace ThinkPower.LabB3.Web.Controllers
{
    public class RiskEvaluationController : Controller
    {
        /// <summary>
        /// 確認接受投資風險評估結果
        /// </summary>
        /// <param name="actionModel">投資風險評估資料</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AcceptRiskRank(SaveRankActionModel actionModel)
        {
            return View();
        }
        /// <summary>
        /// 執行評估投資風險等級
        /// </summary>
        /// <param name="answers">投資風險評估問卷填答資料</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EvaluationRank(FormCollection answers)
        {
            return View();
        }
        /// <summary>
        /// 進行投資風險評估問卷填答
        /// </summary>
        /// <param name="actionMode">來源資料</param>
        /// <returns>投資風險評估問卷畫面</returns>
        [HttpGet]
        public ActionResult EvaQuest(EvaluationRankActionModel actionMode)
        {
            RiskEvaluationService riskService = new RiskEvaluationService();
            RiskEvaQuestionnaire riskEvaQuestionnaire = riskService.GetRiskQuestionnaire("FNDRE001");
            QuestionnaireDisplayViewModel viewModel = new QuestionnaireDisplayViewModel(riskEvaQuestionnaire);
            return View(viewModel);
        }
    }
}
