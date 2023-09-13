using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using ShopHoa.Identitty;




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
            // ... Cấu hình xác thực và phân quyền

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
                user.UserName = "admin";
                user.Email = "admin@gmail.com";
                string password = "admin123";

                var checkUser = userManager.Create(user, password);
                if (checkUser.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admin");
                }
            }


            if (userManager.FindByName("customer") == null)
            {
                var user = new ApplicationUser();
                user.UserName = "c";
                string password = "admin123";

                var checkUser = userManager.Create(user, password);
                if (checkUser.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Customer");
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
