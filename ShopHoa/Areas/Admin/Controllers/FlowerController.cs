using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopHoa.Models;
using ShopHoa.ViewModel;

namespace ShopHoa.Areas.Admin.Controllers
{
    public class FlowerController : Controller
    {
        // GET: Admin/Flower
        AppDBConnext db = new AppDBConnext();
        public ActionResult Index()
        {
            return RedirectToAction("ShowFlower");   
        }
        public ActionResult ShowFlower()
        {
            return View(db.Flowers.ToList());
        }

        public ActionResult DetailFlower(string id)
        {
            Flower fl = db.Flowers.Single(d => d.IdFlower == id.Trim());
            if (fl == null)
            {
                return HttpNotFound();
            }
            return View(fl);
        }
        public ActionResult CreateFlower()
        {
            ViewBag.IdFlower = new SelectList(db.Flowers, "IdFlower", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult CreateFlower(Flower fl)
        {
            try
            {
                db.Flowers.Add(fl);
                db.SaveChanges();
                return RedirectToAction("ShowFlower", "Flower");
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        public ActionResult EditFlower(string id )
        {
            Flower fl = db.Flowers.Single(d => d.IdFlower == id.Trim());
            ViewBag.IdFlower = new SelectList(db.Flowers, "IdFlower", "Name");
            return View(fl);
        }
        [HttpPost]
        public ActionResult EditFlower(Flower fl)
        {
            if(ModelState.IsValid)
            {
                db.Flowers.Add(fl);
                db.SaveChanges();
                return RedirectToAction("ShowFlower", "Home");
            }
            return View(fl);
        }

        public ActionResult DeleteFlower(string id)
        {
            Flower fl = db.Flowers.Single(d => d.IdFlower == id);
            if (fl == null)
            {
                return HttpNotFound();
            }
            db.Entry(fl).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("ShowFlower", "Flower");
        }
         
    }
}