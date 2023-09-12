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
            // Cấu hình DbContext và UserManager
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


            //var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            //var user = userManager.FindByName("username"); // Thay "username" bằng tên người dùng cụ thể
            //userManager.AddToRole(user.Id, "Admin");


            // Sử dụng Cookie Authentication
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Admin/Account/Login"),
            });
        }
    }
}
