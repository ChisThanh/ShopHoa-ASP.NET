using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopHoa.ViewModel
{
    public class FlowerVM
    {
        public string IdFlower { get; set; }
        public string IdType { get; set; }
        public Nullable<double> Price { get; set; }
        public string NameFlower { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }
        public string IdDiscount { get; set; }
        public string Image { get; set; }
    }
}