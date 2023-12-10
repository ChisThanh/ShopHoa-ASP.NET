using ShopHoa.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace ShopHoa.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        // GET: Home
        AppDBConnext db = new AppDBConnext();
        public ActionResult Index()
        {
            ViewBag.total = db.Database.SqlQuery<double>("SELECT dbo.Total()").FirstOrDefault();
            ViewBag.cart = db.Database.SqlQuery<double>("SELECT dbo.QuantityCart()").FirstOrDefault();
            ViewBag.pro = db.Database.SqlQuery<double>("SELECT dbo.QuantityPro()").FirstOrDefault();
            ViewBag.cus = db.Database.SqlQuery<double>("SELECT dbo.QuantityCus()").FirstOrDefault();
            return View();
        }

       
    }
}