using ShopHoa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShopHoa.ApiControllers
{
    public class ProductController : ApiController
    {
       
        AppDBConnext db = new AppDBConnext();
        [HttpGet]
        public IHttpActionResult Getfl(string id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var fl = db.Flowers.FirstOrDefault(each => each.IdFlower.Trim() == id);
            if (fl == null)
                return BadRequest();
            return Ok(fl);
        }
        [HttpGet]
        [Route("api/Product/GetMonthlyRevenue")]
        public IHttpActionResult GetMonthlyRevenue()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var l = db.GetMonthlyRevenue();
            if (l == null)
                return BadRequest();
            return Ok(l);
        }
        [HttpGet]
        [Route("api/Product/GetFlowerTypePercentage")]
        public IHttpActionResult GetFlowerTypePercentage()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var l = db.GetFlowerTypePercentage();
            if (l == null)
                return BadRequest();
            return Ok(l);
        }
    }
}
