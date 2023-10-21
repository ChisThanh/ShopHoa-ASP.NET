using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopHoa.ViewModel
{
    public class ProductVM
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn một tệp hình ảnh.")]
        public HttpPostedFileBase FileImg { get; set; }
        [Required]
        public string Description { get; set; }
        public int? VoucherId { get; set; }
        public Nullable<bool> Active { get; set; }
    }
}