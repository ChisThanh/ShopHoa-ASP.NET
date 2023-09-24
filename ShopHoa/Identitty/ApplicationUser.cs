using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
namespace ShopHoa.Identitty
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public DateTime? BirthDay { get; set; }
        public string IDAddress { get; set; }
        public string Avatar { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string applicationCookie)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
        public string getFullName()
        {
            return FullName;
        }
    }
}