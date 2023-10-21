using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopHoa.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

       
    }
}