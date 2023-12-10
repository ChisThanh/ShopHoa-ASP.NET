using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopHoa.Models;
using ShopHoa.ViewModel;
using ShopHoa.Helpers;
using System.Data.Entity;
using Microsoft.SqlServer.Server;

namespace ShopHoa.Controllers
{
    public class UserController : Controller
    {
        AppDBConnext db = new AppDBConnext();
        // GET: User
        public ActionResult Index()
        {
            return RedirectToAction("ShowTTUser");
        }
        public ActionResult ShowTTUser()
        {
            var tk = User.Identity.GetUserId();  
            var user = db.AspNetUsers.FirstOrDefault(x => x.Id == tk);
            return View(user);
        }
        public ActionResult ShowDonHang()
        {
            var tk = User.Identity.GetUserId();
            var cart = db.Carts.Where(x => x.IdClient == tk).ToList();
            return View(cart);
        }
        public ActionResult RateProduct(string id)
        {
            var c = db.Bills.ToList().FindAll(each =>each.IdBill.Trim() == id);
            if (c.Count() ==  0) return HttpNotFound();
            var fl = db.Flowers.ToList();
            foreach (var item in c)
            {
                item.IdFlower = fl.Find(each => each.IdFlower == item.IdFlower).NameFlower;
            }
            return View(c);
        }
        [HttpPost]
        public ActionResult RateProduct(RateProduct rt)
        {
            if(rt == null) return HttpNotFound();


            string idU = User.Identity.GetUserId();
           

            var Bills = db.Bills.ToList().FindAll(each =>each.IdBill.Trim() == rt.IdBill.Trim());
            foreach (var item in Bills)
            {
                
                var lastEvaluate = db.Evaluates
                      .OrderByDescending(e => e.IdEvaluate)
                      .FirstOrDefault();
                string newId = "";
                if (lastEvaluate != null)
                {
                    string lastCode = lastEvaluate.IdEvaluate;
                    newId = Helper.GetNextCode("DG", lastCode.Trim());
                }
                else { newId = "DG0001"; }

                Evaluate tmp = new Evaluate() { 
                    IdEvaluate = newId,
                    Comment = rt.comment,
                    Star = double.Parse(rt.star),
                    Imgae = null,
                    IdFlower = item.IdFlower,
                    IdClient = idU,
                };
                db.Evaluates.Add(tmp);
                db.SaveChanges();
            }

            var c = db.Carts.ToList().Find(each => each.IdBill == rt.IdBill);
            c.Status = "DG";
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult cart(string id)
        {
            var Bills = db.Bills.ToList().FindAll(each => each.IdBill.Trim() == id.Trim());
            var fl = db.Flowers.ToList();
            foreach (var item in Bills)
            {
                item.IdBill = fl.Find(each => each.IdFlower == item.IdFlower).NameFlower;
            }
            return View(Bills);
        }
        public ActionResult cancelCart(string id)
        {
            var c = db.Carts.ToList().Find(each => each.IdBill.Trim() == id.Trim());
            if(c == null) return HttpNotFound();
            c.Status = "HD";
            db.SaveChanges();
            return RedirectToAction("ShowTTUser");
        }
    }
}