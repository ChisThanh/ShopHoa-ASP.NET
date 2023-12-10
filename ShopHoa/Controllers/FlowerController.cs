using ShopHoa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using System.Web.UI;

namespace ShopHoa.Controllers
{
    public class FlowerController : Controller
    {
        AppDBConnext db = new AppDBConnext();

        // GET: Flower
        public ActionResult index()
        {
            return RedirectToAction("ShowListFlower");
        }
        public ActionResult ShowListFlower(int? page)
        {
            if (page == null)
                page = 1;
            var links = (from l in db.Flowers
                         select l).OrderBy(x => x.IdFlower);
            int pageSize = 6;

            int pageNumber = (page ?? 1);

            return View(links.ToPagedList(pageNumber, pageSize));
        }     
        public ActionResult ShowHoaByType(string MaLoai)
        {
            var allTypes = db.Types.ToList();
            ViewBag.AllTypes = allTypes;
            return View(db.Flowers.Where(t => t.IdType == MaLoai).ToList());
        }
        public ActionResult ShowDetailFlower(string id)
        {
            var SeachId = db.Flowers.FirstOrDefault(t => t.IdFlower == id);
            var Eval = db.Evaluates.ToList().FindAll(each => each.IdFlower.Trim() == id);
            foreach (var item in Eval)
            {
                var u = db.AspNetUsers.ToList().Find(each => each.Id.Trim() == item.IdClient.Trim());
                item.IdClient = u.FullName;
            }
            ViewBag.Eval = Eval;
            return View(SeachId);
        }
        public ActionResult SortByNameFlower()
        {
            var SapXep = db.Flowers.OrderBy(t => t.NameFlower).ToList();
            return PartialView("~/Views/Patial/_DanhSachSanPham.cshtml", SapXep);
        }
        public ActionResult SortByNamePriceAZ()
        {
            var SapXep = db.Flowers.OrderBy(t => t.Price).ToList();
            return PartialView("~/Views/Patial/_DanhSachSanPham.cshtml", SapXep);
        }
        public ActionResult SortByNamePriceZA()
        {
            var SapXep = db.Flowers.OrderByDescending(t => t.Price).ToList();
            return PartialView("~/Views/Patial/_DanhSachSanPham.cshtml", SapXep);
        }
        public ActionResult SlideShowChiTiet()
        {
            return View(db.Flowers.ToList().Take(5));
        }
        public ActionResult ShowChiTietDonHang()
        {
            return View();
        }
        public ActionResult TimKiemTheoTen(string TenSP)
        {
            var tk = db.Flowers.Where(t=>t.NameFlower.Contains(TenSP)).ToList();
            if (tk.Count == 0)
            {
                ViewBag.TimKiem = "Không có hoa này trong danh sách";
            }
            return View(tk);
        }
        public ActionResult LocTheoGia(int GiaMin, int GiaMax)
        {
            var flowersInPriceRange = db.Flowers.Where(t => t.Price >= GiaMin && t.Price <= GiaMax).ToList();
            return View(flowersInPriceRange);
        }
    }
}