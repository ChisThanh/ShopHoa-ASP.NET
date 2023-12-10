using ShopHoa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopHoa.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TypeController : Controller
    {
        // GET: Admin/Type
        AppDBConnext db = new AppDBConnext();
        public ActionResult Index()
        {
            return RedirectToAction("ShowTypeFlower");
        }
        public ActionResult ShowTypeFlower()
        {
            return View(db.Types.ToList());
        }
        public ActionResult CreateType()
        {
            ViewBag.idtype = new SelectList(db.Flowers, "IdFlower", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult CreateType(ShopHoa.Models.Type type)
        {
            try
            {
                // Kiểm tra xem có Type nào có cùng IdType đã tồn tại hay không
                if (db.Types.Any(t => t.IdType == type.IdType))
                {
                    ModelState.AddModelError("IdType", "IdType đã tồn tại. Vui lòng chọn IdType khác.");
                    ViewBag.idtype = new SelectList(db.Flowers, "IdFlower", "Name", type.IdType);
                    return View(type);
                }

                db.Types.Add(type);
                db.SaveChanges();
                return RedirectToAction("ShowTypeFlower", "Type");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult EditType(string id)
        {
            var p = db.Types.FirstOrDefault(t => t.IdType == id);
            if (p == null)
            {
                return HttpNotFound();
            }

            return View(p);
        }
        [HttpPost]
        public ActionResult EditType(ShopHoa.Models.Type type)
        {
            var tk = db.Types.FirstOrDefault(t => t.IdType == type.IdType);
            if (ModelState.IsValid)
            {
                tk.IdType = type.IdType;
                tk.NameType = type.NameType;                        
                db.SaveChanges();
                return RedirectToAction("ShowTypeFlower", "Type");
            }
            return View(tk);
        }

        public ActionResult DeleteType(string id)
        {
            var type = db.Types.SingleOrDefault(t => t.IdType == id);

            if (type == null)
            {
                return HttpNotFound();
            }

            // Check if there are any flowers associated with this type
            bool hasAssociatedFlowers = db.Flowers.Any(f => f.IdType == id);

            if (hasAssociatedFlowers)
            {
                TempData["DeleteError"] = "Không thể xóa loại hoa này vì có hoa liên quan.";
            }
            else
            {
                db.Types.Remove(type);
                db.SaveChanges();
            }

            return RedirectToAction("ShowTypeFlower", "Type");
        }
    }
}