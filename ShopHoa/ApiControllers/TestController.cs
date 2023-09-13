using ShopHoa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShopHoa.ApiControllers
{
    public class TestController : ApiController
    {
        public List<Product> get() {
            ApplicationDbContext context = new ApplicationDbContext();
            return context.Products.ToList();
        }
    }
}
