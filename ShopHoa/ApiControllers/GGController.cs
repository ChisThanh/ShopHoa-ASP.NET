using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using ShopHoa.Identity;
using System.Web.Http;

namespace ShopHoa.ApiControllers
{
    public class GGController : ApiController
    {
        public IHttpActionResult post([FromBody] string email) {

            var userManager = new UserManager<ApplicationUser>
                (new UserStore<ApplicationUser>(new ApplicationDbContext()));

            var user = userManager.FindByName(email);

            if (user != null)
            {
                return Json(new { success = true, msg = "Tài khoảng đã tồn tại" });
            }
            return Json(new { success = false, msg = "Tài khoảng chưa tồn tại" });
        }
    }
}
