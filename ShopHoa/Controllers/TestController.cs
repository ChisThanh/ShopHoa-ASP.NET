using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Microsoft.AspNet.Identity.Owin;
using ShopHoa.Identitty;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Text;
using System.Web.Http;
using System.Net.Mail;
using System.Net;
using ShopHoa.Helpers;

namespace ShopHoa.Controllers
{
    public class TestController : Controller
    {
        public ActionResult Index()
        {
            
            //Helper.SendEmail("chithanh18042003@gmail.com", "Hello", "Xin chòa");
            return View();
        }
        
    }
}
