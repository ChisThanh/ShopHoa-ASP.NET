using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopHoa.Models;

namespace ShopHoa.Controllers
{
    public class TypeController : Controller
    {
        // GET: Type
        AppDBConnext db = new AppDBConnext();
        public ActionResult DanhMucThucDon()
        {
            return View(db.Types.ToList());
        }
        public ActionResult LoaiHoaTrongDanhMuc() 
        {
            return View(db.Flowers.ToList());
        }
    }
}