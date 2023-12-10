using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopHoa.ViewModel
{
    public class CartVM
    {
        public string id { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public double price { get; set; }
        public int quantity{ get; set; }
 
    }
    public class Form
    {
        public string id { get; set; }
        public string name{ get; set; }
        public string ward { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public string phoneNumber { get; set; }
        public string note { get; set; }
    }
   
}