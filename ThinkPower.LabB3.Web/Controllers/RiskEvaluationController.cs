using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ThinkPower.LabB3.Web.Controllers
{
    public class RiskEvaluationController : Controller
    {
        // GET: RiskEvaluation
        public ActionResult Index()
        {
            return View();
        }

        // GET: RiskEvaluation/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RiskEvaluation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RiskEvaluation/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: RiskEvaluation/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RiskEvaluation/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: RiskEvaluation/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RiskEvaluation/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
