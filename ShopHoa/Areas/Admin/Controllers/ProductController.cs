using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopHoa.Models;

namespace ShopHoa.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        // GET: Admin/Product

        public ActionResult Index()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            List<Product> products = db.Products.ToList();
            return View(products);
        }
    }
}