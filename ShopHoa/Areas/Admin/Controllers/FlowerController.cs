using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopHoa.Models;
using ShopHoa.ViewModel;

namespace ShopHoa.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
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
                // Kiểm tra xem có hoa nào có cùng IdFlower đã tồn tại hay không
                if (db.Flowers.Any(f => f.IdFlower == fl.IdFlower))
                {
                    ModelState.AddModelError("IdFlower", "IdFlower đã tồn tại. Vui lòng chọn IdFlower khác.");
                    ViewBag.IdFlower = new SelectList(db.Flowers, "IdFlower", "Name", fl.IdFlower);
                    return View(fl);
                }

                db.Flowers.Add(fl);
                db.SaveChanges();
                return RedirectToAction("ShowFlower", "Flower");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public ActionResult EditFlower(string id)
        {
            Flower p = db.Flowers.FirstOrDefault(t => t.IdFlower == id);
            if (p == null)
            {
                return HttpNotFound();
            }

            return View(p);
        }
        [HttpPost]
        public ActionResult EditFlower(Flower fl)
        {
            var tk = db.Flowers.FirstOrDefault(t => t.IdFlower == fl.IdFlower);
            if (ModelState.IsValid)
            {
                tk.IdFlower = fl.IdFlower;
                tk.IdType = fl.IdType;
                tk.Price = fl.Price;
                tk.NameFlower = fl.NameFlower;
                tk.Note = fl.Note;
                tk.Status = fl.Status;
                tk.IdDiscount = fl.IdDiscount;
                if (!string.IsNullOrEmpty(fl.Image))
                {
                    tk.Image = fl.Image;
                }
                db.SaveChanges();
                return RedirectToAction("ShowFlower", "Flower");
            }
            return View(tk);
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