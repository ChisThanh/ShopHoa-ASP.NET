using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using ShopHoa.Identitty;
using ShopHoa.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Contexts;
using System.Web.Http;
using System.Web.ModelBinding;
using System.Web.UI.WebControls;
using System.Web.Helpers;
using Microsoft.Owin.Security;
using System.Web;
using System.Web.Mvc;
using FormCollection = System.Web.Mvc.FormCollection;
using ShopHoa.Helpers;

using ShopHoa.Models;
using System.Threading.Tasks;

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
