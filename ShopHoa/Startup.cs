using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using ShopHoa.Identity;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using System.Security.Claims;
using System.Web.Helpers;
using Microsoft.Owin.Security.OAuth;


[assembly: OwinStartup(typeof(ShopHoa.Startup))]

namespace ShopHoa
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
        public void ConfigureAuth(IAppBuilder app)
        {

            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            var context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            

            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole("Admin");
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Customer"))
            {
                var role = new IdentityRole("Customer");
                roleManager.Create(role);
            }

            var userManager = new UserManager<ApplicationUser>
                (new UserStore<ApplicationUser>(context));

            if (userManager.FindByName("admin") == null)
            {
                var user = new ApplicationUser();
                user.UserName = "admin@gmail.com";
                user.Email = "admin@gmail.com";
                string password = "admin123";

                var checkUser = userManager.Create(user, password);
                if (checkUser.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admin");
                }
            }

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }
    }
}
