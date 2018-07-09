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
using ThinkPower.LabB3.Domain.DTO;
using System.Text.RegularExpressions;

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
                return RedirectToAction("About", "Home", actionModel);
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(ex).Throw();
                return RedirectToAction("About", "Home", actionModel);
            }
        }
        /// <summary>
        /// 執行評估投資風險等級
        /// </summary>
        /// <param name="answers">投資風險評估問卷填答資料</param>
        /// <returns></returns>
        //TODO UserId 屬於識別資訊，要在後端串出來，並非從前端帶，ActionMode的建構式可以被前端重整就可以重改，也不行
        //TODO 要用Session去判斷是否為同一個UserId，也可以用儲存Cache或是存資料庫來做判別
        [HttpPost]
        public ActionResult EvaluationRank(FormCollection answers)
        {
            if (answers == null)
            {
                throw new ArgumentNullException(nameof(answers));
            }
            
            try
            {
                RiskEvaAnswerEntity riskAnswerEntity = new RiskEvaAnswerEntity();
                List<AnswerDetailEntity> questions = new List<AnswerDetailEntity>();
                foreach (string fieldName in answers)
                {
                    //題目fieldName => "Question-Q001" or "Question-Q1"
                    if (fieldName.StartsWith("Question-") && !fieldName.EndsWith("-other"))
                    {
                        string questId = fieldName.Split('-')[1];
                        string[] ansCode = answers[fieldName].Split(',');
                        //找到有相同題號與選項的填答物件
                        foreach (var ans in ansCode)
                        {
                            var question = (from quest in questions
                                            where (quest.QuestionId == questId) && (quest.AnswerCode == ans)
                                            select quest).FirstOrDefault();
                            //若物件不存在
                            if (question == null)
                            {
                                AnswerDetailEntity questionNew = new AnswerDetailEntity();
                                questionNew.QuestionId = questId;
                                questionNew.AnswerCode = ans;
                                questionNew.OtherAnswer = "";
                                questions.Add(questionNew);
                            }
                        }
                        
                    }
                    //填充題fieldName => "Question-Q1-E-other"
                    else if (fieldName.StartsWith("Question-") && fieldName.EndsWith("-other"))
                    {
                        string questId = fieldName.Split('-')[1];
                        string ansCode = fieldName.Split('-')[2];
                        string otherAnswer = answers[fieldName];
                        //找到有相同題號與選項的填答物件
                        var question = (from quest in questions
                                        where (quest.QuestionId == questId) && (quest.AnswerCode == ansCode)
                                        select quest).FirstOrDefault();
                        //若物件不存在
                        if (question == null)
                        {
                            AnswerDetailEntity questionNew = new AnswerDetailEntity();
                            questionNew.QuestionId = questId;
                            questionNew.AnswerCode = ansCode;
                            questionNew.OtherAnswer = otherAnswer;
                            questions.Add(questionNew);
                        }
                        //若物件存在
                        else
                        {
                            question.OtherAnswer = otherAnswer;
                        }
                    } 
                }
                riskAnswerEntity.Questions = questions;
                
                //問卷識別碼 (參考問卷主檔Uid)
                if (answers.AllKeys.Contains("QuestUid"))
                {
                    riskAnswerEntity.QuestUid = Guid.Parse(answers["QuestUid"]);
                }

                //問卷填寫來源代號 (固定為LabB3)
                if (answers.AllKeys.Contains("TesteeSource"))
                {
                    riskAnswerEntity.TesteeSource = answers["TesteeSource"];
                }
                
                RiskEvaluationService riskService = new RiskEvaluationService();
                riskService.EvaluateRiskRank(riskAnswerEntity);

                return View();
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
            if (actionMode == null)
            {
                throw new ArgumentNullException(nameof(actionMode));
            }
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
