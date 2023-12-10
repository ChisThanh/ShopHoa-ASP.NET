using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopHoa.Models;

namespace ShopHoa.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]

    public class EvaluateController : Controller
    {
        // GET: Admin/Evaluate
        AppDBConnext db = new AppDBConnext();
        public ActionResult Index()
        {
            return RedirectToAction("ShowEvaluate");
        }
        public ActionResult ShowEvaluate()
        {
            return View(db.Evaluates.ToList());
        }

        public ActionResult DetailEvaluate(string id)
        {
            Evaluate evl = db.Evaluates.Single(d => d.IdEvaluate == id);
            if (evl == null)
            {
                return HttpNotFound();
            }
            return View(evl);
        }

        public ActionResult CreateEvalute()
        {
            ViewBag.IdEvaluate = new SelectList(db.Evaluates, "IdEvaluate", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult CreateEvaluate(Evaluate evl)
        {
            if (ModelState.IsValid)
            {
                db.Evaluates.Add(evl);
                db.SaveChanges();
                return RedirectToAction("ShowEvaluate", "Home");
            }
            return View(evl);
        }

        public ActionResult EditEvaluate(string id)
        {
            Evaluate evl = db.Evaluates.Single(d => d.IdEvaluate == id);
            ViewBag.IdEvaluate = new SelectList(db.Evaluates, "IdFlower", "Name");
            return View(evl);
        }
        [HttpPost]
        public ActionResult EditEvaluate(Evaluate evl)
        {
            if (ModelState.IsValid)
            {
                db.Evaluates.Add(evl);
                db.SaveChanges();
                return RedirectToAction("ShowEvaluate", "Home");
            }
            return View(evl);
        }

        public ActionResult DeleteEvaluate(string id)
        {
            Evaluate evl = db.Evaluates.Single(d => d.IdEvaluate == id);
            if (evl == null)
            {
                return HttpNotFound();
            }
            return View(evl);
        }
        [HttpPost, ActionName("DeleteEvaluate")]
        public ActionResult DeleteEvaluateConfirm(string id)
        {
            Evaluate evl = db.Evaluates.Single(d => d.IdEvaluate == id);
            if (evl == null)
            {
                return HttpNotFound();
            }
            db.Entry(evl).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("ShowEvaluate", "Home");
        }
    }
}