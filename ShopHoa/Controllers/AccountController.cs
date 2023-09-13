using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using ShopHoa.Identitty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using ShopHoa.ViewModel;
using System.Web.Helpers;

namespace ShopHoa.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        // GET: Admin/Account
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterVM rvm)
        {
            var passHash = Crypto.HashPassword(rvm.Password);
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    Email = rvm.Email,
                    UserName = rvm.Name,
                    PasswordHash = passHash
                };

                var context = new ApplicationDbContext();
                var userManager = new UserManager<ApplicationUser>
                (new UserStore<ApplicationUser>(context));

                IdentityResult identityResult = userManager.Create(user);

                if (identityResult.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Customer");
                    return RedirectToAction("Login");
                }
            }
            ModelState.AddModelError("Error", "Không tồn tại!!");
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string name, string password)
        {
            var context = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>
               (new UserStore<ApplicationUser>(context));

            var user = userManager.Find(name, password);
            if (user == null)
                ViewBag.Error = "Không tồn tại tài khoảng và mật khẩu";
            else
            {
                var authenManager = HttpContext.GetOwinContext().Authentication;
                var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                authenManager.SignIn(new AuthenticationProperties(), userIdentity);

                if(userManager.IsInRole(user.Id, "Admin"))
                {
                    return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
                }
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult Logout()
        {
            var authenManager = HttpContext.GetOwinContext().Authentication;
            authenManager.SignOut();
            return RedirectToAction("Index", "Home", new {area = ""});

        }
    }
}