using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ShopHoa.ViewModel;
using ShopHoa.Models;
using Microsoft.AspNet.Identity;
using ShopHoa.Helpers;
using System.Collections;
using System.Data.Entity;
using System;

namespace ShopHoa.ApiControllers
{
    public class CartController : ApiController
    {
        AppDBConnext db = new AppDBConnext();
        [HttpPost]
        [Route("api/cart/cart")]
        public IHttpActionResult Cart(List<CartVM> l)
        {
             db.Configuration.ProxyCreationEnabled = false;

            bool check = l.Select(item => item.id)
                            .All(id => db.Flowers.ToList().Select(i => i.IdFlower.Trim())
                            .Contains(id.Trim()));
            string newId;
            if (!check)
            {
                return BadRequest("aaa");
            }

            var idUser = User.Identity.GetUserId();
            Cart c = new Cart();

            var lastCart = db.Carts
                            .OrderByDescending(e => e.IdBill)
                            .FirstOrDefault();

            if (lastCart != null)
            {
                string lastCode = lastCart.IdBill;
                newId = Helper.GetNextCode("GH", lastCode.Trim());
                c.IdClient = idUser;
                c.IdBill = newId;
            }
            else
            {
                c.IdClient = idUser;
                newId = "GH0001";
                c.IdBill = newId;
            }
            c.Status = "CXN";
            c.Date = DateTime.Now;
            db.Carts.Add(c);
            var f = db.SaveChanges();
            if (f <= 0)
            {
                return BadRequest("aaa");
            }
            foreach (var item in l)
            {
                var fl = db.Flowers.FirstOrDefault(each => each.IdFlower == item.id);
                Bill b = new Bill();
                b.IdBill = c.IdBill;
                b.IdFlower = fl.IdFlower;
                b.IntoMoney = item.quantity * fl.Price;
                b.Quantity = item.quantity;
                db.Bills.Add(b);
            }
            db.SaveChanges();
            return Ok(new { id = Helper.Encrypt(newId) });
        }
        [HttpPost]
        [Route("api/cart/updatecart")]
        public IHttpActionResult UpdateCart(Form f)
        {
            db.Configuration.ProxyCreationEnabled = false;

            var idB = Helper.Decrypt(f.id);

            var cart = db.Carts.FirstOrDefault(each => each.IdBill == idB);
            if (cart == null)
            {
                return BadRequest();
            }
            var lastA = db.Addresses
                            .OrderByDescending(e => e.Id)
                            .FirstOrDefault();
            string newId;
            if (lastA != null)
            {
                string lastCode = lastA.Id;
                newId = Helper.GetNextCode("DC", lastCode.Trim());
            }
            else
            {
                newId = "DC0001";
            }
            Address a = new Address()
            {
                Id = newId,
                SpecificAddress = f.address,
                District = f.district,
                City = f.city,
                Ward = f.ward,
            };
           
            db.Addresses.Add(a);
            var check = db.SaveChanges();
            if (check <= 0) 
                return BadRequest();

            cart.PhoneNumber = f.phoneNumber;
            cart.Address = a.Id;
            cart.Name = f.name;
            if (db.SaveChanges() <= 0)
                return BadRequest();

            return Ok("Đúng rồi nha");
        }
        [HttpGet]
        [Route("api/cart/changeStatus")]
        public IHttpActionResult changeStatus(string id, string status)
        {
            var c = db.Carts.ToList().Find(each => each.IdBill == id);
            if (c == null) return BadRequest();
            c.Status = status;
            db.SaveChanges();
            return Ok();
        }
    }
}
