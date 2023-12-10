using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ShopHoa.ApiControllers
{
    public class UploadController : ApiController
    {
        [HttpPost]
        [Route("api/upload")]
        public IHttpActionResult Upload()
        {
            try
            {
                var file = HttpContext.Current.Request.Files[0];
                string filePath = "/assets/images/product/" + file.FileName;
                file.SaveAs(HttpContext.Current.Server.MapPath(filePath));
                return Ok(new { filePath = filePath });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
