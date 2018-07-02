using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThinkPower.LabB3.Domain.Service;
using ThinkPower.LabB3.Web.ActionModels;
using ThinkPower.LabB3.Web.ViewModels;
using ThinkPower.LabB3.Domain.Entity.Risk;
using System.Runtime.ExceptionServices;
using ThinkPower.LabB3.Domain.Entity.Question;

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
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(ex).Throw();
                return null;
            }
        }
        /// <summary>
        /// 執行評估投資風險等級
        /// </summary>
        /// <param name="answers">投資風險評估問卷填答資料</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EvaluationRank(FormCollection answers)
        {
            try
            {
                //TODO UserId 屬於識別資訊，要在後端串出來，並非從前端帶，ActionMode的建構式可以被前端重整就可以重改，也不行
                //TODO 要用Session去判斷是否為同一個UserId，也可以用儲存Cache或是存資料庫來做判別
                RiskEvaAnswerEntity answerEntity = new RiskEvaAnswerEntity();
                if (answers.AllKeys.Contains("QuestUid"))
                {
                    answerEntity.QuestUid = Guid.Parse(answers["QuestUid"]);
                    answers.Remove("QuestUid");
                }
                
                if (answers.AllKeys.Contains("TesteeSource"))
                {
                    answerEntity.TesteeSource = answers["TesteeSource"];
                    answers.Remove("TesteeSource");
                }
                //答題結果
                Dictionary<string, string> answerItems = new Dictionary<string, string>();
                foreach (string ans in answers)
                {
                    answerItems.Add(ans, answers[ans].ToString());
                }
                answerEntity.AnswerItems = answerItems;

                RiskEvaluationService riskEvaluationService = new RiskEvaluationService();
                riskEvaluationService.EvaluateRiskRank(answerEntity);

                
                //TODO 暫時轉的View
                return RedirectToAction("AcceptRiskRank");
                //return View();
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(ex).Throw();
                return null;
            }
        }
        /// <summary>
        /// 進行投資風險評估問卷填答
        /// </summary>
        /// <param name="actionMode">來源資料</param>
        /// <returns>投資風險評估問卷畫面</returns>
        [HttpGet]
        public ActionResult EvaQuest(EvaluationRankActionModel actionMode)
        {
            try
            {
                RiskEvaluationService riskService = new RiskEvaluationService();
                RiskEvaQuestionnaireEntity riskEvaQuestionnaireEntity = riskService.GetRiskQuestionnaire(actionMode.QuestId);
                QuestionnaireDisplayViewModel viewModel = new QuestionnaireDisplayViewModel(riskEvaQuestionnaireEntity)
                {
                    TesteeSource = actionMode.TesteeSource
                };
                
                
                
                return View(viewModel);
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(ex).Throw();
                return null;
                //TODO 前端顯示>> 系統發生錯誤，請於上班時段來電客服中心0800-015-000，造成不便敬請見諒。
            }

        }        
    }
}
