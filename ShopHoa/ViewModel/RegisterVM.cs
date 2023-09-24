using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopHoa.ViewModel
{
    public class RegisterVM
    {
        [Required]
        public string Name { get;set; }
        [Required]
        public string FullName { get;set; }
        [Required]
        public string Password{ get;set; }
        [Required]
        public string Email { get;set; }
        public string Avatar { get;set; }
        
    }
}