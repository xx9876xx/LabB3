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
            if (answers == null)
            {
                throw new ArgumentNullException(nameof(answers));
            }
            Regex regex = new Regex("[Q][0-9]");
            try
            {
                RiskEvaAnswerEntity riskAnswerEntity = new RiskEvaAnswerEntity();
                List<AnswerDetailEntity> questions = new List<AnswerDetailEntity>();
                foreach (string ansId in answers)
                {
                    //為題目
                    if (regex.IsMatch(ansId))
                    {
                        //非填充題
                        if (!ansId.EndsWith("-other"))
                        {
                            //若集合不存在該題號物件
                            if (!questions.Any(q => q.QuestionId == ansId))
                            {
                                foreach (string ansCode in answers[ansId].Split(','))
                                {
                                    if (ansCode.Length == 1)
                                    {
                                        AnswerDetailEntity question = new AnswerDetailEntity();
                                        question.QuestionId = ansId;
                                        question.AnswerCode = Convert.ToChar(ansCode);
                                        questions.Add(question);
                                    }
                                }
                            }
                            //若已存在則忽略
                            else
                            {
                            }
                        }
                        //若為填充題
                        else
                        {
                            string[] array = ansId.Split('-');
                            //若集合不存在該題號物件
                            if (!questions.Any(q => q.QuestionId == array[0]))
                            {
                                AnswerDetailEntity question = new AnswerDetailEntity();
                                question.QuestionId = array[0];
                                if (array[1].Length == 1)
                                {
                                    question.AnswerCode = Convert.ToChar(array[1]);
                                }
                                question.OtherAnswer = answers[ansId];
                                questions.Add(question);
                            }
                            //若已存在則填入其他說明即可
                            else
                            {
                                foreach (var question in questions)
                                {
                                    if (question.QuestionId == array[0] && Convert.ToChar(question.AnswerCode) == Convert.ToChar(array[1]))
                                    {
                                        question.OtherAnswer = answers[ansId];
                                    }
                                }
                            }
                        }
                    }
                }
                riskAnswerEntity.Questions = questions;

                //TODO UserId 屬於識別資訊，要在後端串出來，並非從前端帶，ActionMode的建構式可以被前端重整就可以重改，也不行
                //TODO 要用Session去判斷是否為同一個UserId，也可以用儲存Cache或是存資料庫來做判別

                //問卷識別碼 (參考問卷主檔Uid)
                if (answers.AllKeys.Contains("questUid"))
                {
                    riskAnswerEntity.QuestUid = Guid.Parse(answers["questUid"]);
                }

                //問卷填寫來源代號 (固定為LabB3)
                if (answers.AllKeys.Contains("TesteeSource"))
                {
                    riskAnswerEntity.TesteeSource = answers["TesteeSource"];
                }
                
                RiskEvaluationService riskService = new RiskEvaluationService();
                riskService.EvaluateRiskRank(riskAnswerEntity);
                
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
