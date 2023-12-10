using ShopHoa.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopHoa.Areas.Admin.Controllers
{
    public class CartController : Controller
    {
        // GET: Admin/Cart
        AppDBConnext db = new AppDBConnext();
        public ActionResult Index()
        {
            return RedirectToAction("DSDonHang");
        }
        public ActionResult DSDonHang()
        {
            List<Cart> b = db.Carts.ToList();
            return View(b);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Cart t)
        {
            if (ModelState.IsValid)
            {
                db.Carts.Add(t);
                db.SaveChanges();
                return RedirectToAction("DSDonHang", "Cart");
            }
            return View(t);
        }
        public ActionResult Details(string id)
        {
            var p = db.Bills.Where(t => t.IdBill.Contains(id)).ToList();
            return View(p);
        }


        public ActionResult Delete(string id)
        {
            Cart p = db.Carts.Where(t => t.IdBill == id).FirstOrDefault();
            if (p == null)
            {
                return HttpNotFound();
            }
            db.Entry(p).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("DSDonHang", "Cart");
        }

        public ActionResult Edit(string id)
        {
            Cart p = db.Carts.Where(t => t.IdBill.Contains(id)).FirstOrDefault();
            if (p == null)
            {
                return HttpNotFound();
            }
            return View(p);
        }

        [HttpPost]
        public ActionResult Edit(Cart t)
        {
            if (ModelState.IsValid)
            {
                db.Carts.Attach(t);
                db.Entry(t).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DSDonHang", "Cart");

            }
            return View(t);
        }

       

    }
}