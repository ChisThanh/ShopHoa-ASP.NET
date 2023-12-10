using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopHoa.Models;
namespace ShopHoa.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        AppDBConnext db = new AppDBConnext();
        public ActionResult Index()
        {
            var l = db.Flowers.Take(12).ToList();

            return View(l.ToArray());
        }
        public ActionResult TypeFlowerMenu()
        {
            var ds = db.Types.ToList();
            return View(ds);
        }

    }
}