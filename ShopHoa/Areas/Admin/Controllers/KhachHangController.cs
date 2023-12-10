using ShopHoa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopHoa.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class KhachHangController : Controller
    {
        // GET: Admin/KhachHang
        AppDBConnext db = new AppDBConnext();
        public ActionResult Index()
        {
            return View(db.AspNetUsers.ToList());
        }
        public ActionResult Delete(string id)
        {
            AspNetUser p = db.AspNetUsers.Where(t => t.Id == id).FirstOrDefault();
            if (p == null)
            {
                return HttpNotFound();
            }
            db.Entry(p).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index", "KhachHang");
        }

    }
}