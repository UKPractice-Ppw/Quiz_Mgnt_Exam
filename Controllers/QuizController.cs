using QuizMgnt.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuizMgnt.Controllers
{
    public class QuizController : Controller
    {
        dbHandler db = new dbHandler();

        public List<SelectListItem> FillCategoryDDL()
        {
            List<SelectListItem> cat_list = new List<SelectListItem>();
            string qry = "SELECT * FROM tblQuizCat";
            DataTable dt = db.ddlQuery(qry);
            foreach (DataRow dr in dt.Rows)
            {
                cat_list.Add(new SelectListItem
                {
                    Value= Convert.ToString(dr.ItemArray[0]),
                    Text = Convert.ToString(dr.ItemArray[1])
                });
            }
            return cat_list;
        }

        // GET: Quiz
        public ActionResult Index(int? cat_id, string quiz_title)
        {
            ViewBag.Cat_list = FillCategoryDDL();

            // Retain form field values after page submit
            ViewBag.SelectedCatId = cat_id;
            ViewBag.SearchTitle = quiz_title;

            return View(db.GetQuizList(cat_id, quiz_title));
        }
        // GET: Quiz/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Quiz/Create
        public ActionResult Create()
        {
            ViewBag.Cat_list = FillCategoryDDL();
            return View();
        }

        // POST: Quiz/Create
        [HttpPost]
        public ActionResult Create(Quiz Qz)
        {
            try
            {
                if(db.Add_Quiz(Qz))
                {
                    return RedirectToAction("Index","Quiz");
                }
                else
                    return RedirectToAction("Index","Home");
            }
            catch
            {
                return View();
            }
        }

        // GET: Quiz/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Quiz/Edit/5
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

        // GET: Quiz/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Quiz/Delete/5
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
