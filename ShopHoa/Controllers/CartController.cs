using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;
using Microsoft.Owin.Logging;
using Microsoft.AspNet.Identity;

namespace ShopHoa.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            ViewBag.Error = TempData["Error"];
            return View();
        }

        public ActionResult Checkout()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            TempData["Error"] = "Bạn phải đăng nhập mới thanh toán được";
            return RedirectToAction("Index");
        }
    }
}