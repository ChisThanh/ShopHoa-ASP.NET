using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using ShopHoa.Identitty;

namespace ShopHoa.Models
{
    public class ApplicationDbContextMD : DbContext
    {
        public ApplicationDbContextMD() : base("MyConnectionString") { }
        public DbSet<Product> Products { get; set; }
    }
}