using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using ShopHoa.Identitty;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using System.Web.Helpers;
using FormCollection = System.Web.Mvc.FormCollection;
using ShopHoa.Helpers;
using System.Runtime.Remoting.Contexts;
using ShopHoa.ViewModel;

namespace ShopHoa.Areas.Admin.Controllers
{
   
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Register(string email = "", string fullname = "",string avatar ="")
        {
            ViewBag.Email = email;
            ViewBag.FullName = fullname;
            ViewBag.Avatar = avatar;
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterVM vm)
        {
            var passHash = Crypto.HashPassword(vm.Password);

            var user = new ApplicationUser()
            {
                FullName = vm.FullName,
                Email = vm.Email,
                UserName = vm.Email,
                Avatar = vm.Avatar,
                PasswordHash = passHash
            };

            var context = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>
            (new UserStore<ApplicationUser>(context));

            IdentityResult identityResult = userManager.Create(user);

            if (identityResult.Succeeded)
            {
                userManager.AddToRole(user.Id, "Customer");
                var authenManager = HttpContext.GetOwinContext().Authentication;
                var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                authenManager.SignIn(new AuthenticationProperties(), userIdentity);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpGet]
        public ActionResult Login(string email="")
        {
            ViewBag.Email = email;
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();

        }
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            var context = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>
               (new UserStore<ApplicationUser>(context));

            
            var user = userManager.Find(email, password);

            if (user == null)
            {
                ViewBag.ErrorPas = "Tài khoảng hoặc mật khẩu không chính xác!";
                return View();
            }
            var authenManager = HttpContext.GetOwinContext().Authentication;
            var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            authenManager.SignIn(new AuthenticationProperties(), userIdentity);


            if (userManager.IsInRole(user.Id, "Admin"))
            {
                return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Logout()
        {
            var authenManager = HttpContext.GetOwinContext().Authentication;
            authenManager.SignOut();
            return RedirectToAction("Index", "Home", new {area = ""});
        }
    }
}  

