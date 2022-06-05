using LTTHver2._2.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace LTTHver2._2.Area.Main
{

    public class CartController : ApiController
    {
        public LTTH context = new LTTH();
        public CartController()
        {
            context.Configuration.ProxyCreationEnabled = false;
        }
        [Route("api/user/cartUserID")]
        [HttpGet]
        public IHttpActionResult GetAllCart(string id)
        {
            var cartfood = from a in context.Carts
                           from b in context.CartFoodDetails

                           from d in context.FoodOptions
                           from e in context.Foods

                           where a.CreatedByUserID == id &&
                           a.ID == b.CartID && b.FoodOptionID == d.ID && d.FoodID == e.ID
                           select new
                           {
                               e.Name,
                               e.Image,
                               e.OriginPrice,
                               e.PromotionPrice,
                               d.Size,
                               d.Topping,
                               d.BoundingPrice,
                               b.Quantity
                           };
            var cartcombos = from a in context.Carts
                             from c in context.CartComboDetails
                             from d in context.FoodOptions
                             from e in context.ComboDetails
                             from f in context.Combos
                             where a.CreatedByUserID == id && a.ID == c.CartID && c.ComboID == f.ID &&
                             f.ID == e.ComboID && e.FoodOptionID == d.ID
                             select new
                             {
                                 f.Name,
                                 f.Image,
                                 f.OriginPrice,
                                 f.PromotionPrice,
                                 d.Size,
                                 d.Topping,
                                 d.BoundingPrice,
                                 e.Quantity
                             };

            return Ok(new { data1 = cartfood, data2 = cartcombos, message = HttpStatusCode.OK });
        }
        [Route("api/user/cartUser")]
        [HttpGet]
        public IHttpActionResult AddCart(Cart cart, int idcombo, int quantitycombo, int foodoptionID, int quantityfood)
        {
            try
            {
                var _cart = cart;
                context.Carts.Add(_cart);

                CartComboDetail _cartcombodetail = new CartComboDetail();
                _cartcombodetail.CartID = _cart.ID;
                _cartcombodetail.ComboID = idcombo;
                _cartcombodetail.Quantity = quantitycombo;
                context.CartComboDetails.Add(_cartcombodetail);

                CartFoodDetail _cartfooddetail = new CartFoodDetail();
                _cartfooddetail.CartID = _cart.ID;
                _cartfooddetail.FoodOptionID = foodoptionID;
                _cartfooddetail.Quantity = quantityfood;
                context.CartFoodDetails.Add(_cartfooddetail);
                context.SaveChanges();
                return Ok(new { data = "Thành công", message = HttpStatusCode.OK });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [Route("api/user/Deletecart")]
        [HttpGet]
        public IHttpActionResult DeleteCart(int id)
        {
            try
            {
                var rm = context.Carts.Find(id);
                context.Carts.Remove(rm);
                context.SaveChanges();
                return Ok(new { data = "Thành công", message = HttpStatusCode.OK });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [Route("api/user/deleteCartItem")]
        [HttpPost]
        public IHttpActionResult deleteCartItem(string id, int idItem)
        {
            try
            {
                var rm = context.Carts.Where(x => x.CreatedByUserID == id).FirstOrDefault();
                var rm_Item = context.CartFoodDetails.Where(x => x.CartID == rm.ID).Where(x => x.FoodOptionID == idItem).FirstOrDefault();
                context.CartFoodDetails.Remove(rm_Item);
                context.SaveChanges();
                return Ok(new { data = "Thành công", message = HttpStatusCode.OK });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [Route("api/user/updateQuantity")]
        [HttpPost]
        public IHttpActionResult updateQuantity(string id, int idItem, int nQuantity)
        {
            try
            {
                var rm = context.Carts.Where(x => x.CreatedByUserID == id).FirstOrDefault();
                var rm_Item = context.CartFoodDetails.Where(x => x.CartID == rm.ID).Where(x => x.FoodOptionID == idItem).FirstOrDefault();
                rm_Item.Quantity = nQuantity;
                context.SaveChanges();
                return Ok(new { data = "Thành công", message = HttpStatusCode.OK });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [Route("api/user/paymentFromCart")]
        [HttpPost]
        public IHttpActionResult paymentFromCart()
        {
            try
            {
                string id = HttpContext.Current.Request.Form["id"];
                string name = HttpContext.Current.Request.Form["name"];
                string address = HttpContext.Current.Request.Form["address"];
                var rm = context.Carts.Where(x => x.CreatedByUserID == id).FirstOrDefault();
                var nOrder = new Order() { IDUser = id, CustomerName = name, CustomerAddress = address };

                context.Orders.Add(nOrder);
                context.SaveChanges();
                var idc = rm.ID;
                var id_ = nOrder.ID;
                context.Database.ExecuteSqlCommand("insert into OrderFoodDetails (orderid, FoodOptionID, quantity) select " + id_.ToString() + ", foodoptionid, quantity from cartfooddetails where cartID = " + idc.ToString());
                context.Database.ExecuteSqlCommand("delete from cartfooddetails where cartid = " + idc.ToString());
                context.Database.ExecuteSqlCommand("update orders set ToTalPrice = (select sum(promotionprice * quantity) from OrderFoodDetails a join Foods b on a.FoodOptionID = b.ID where orderid = " + id_.ToString() + ") where id = " + id_.ToString());

                context.SaveChanges();
                return Ok(new { data = "Thành công", message = HttpStatusCode.OK });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [Route("api/user/addCart")]
        [HttpPost]
        public IHttpActionResult addCart()
        {
            try
            {
                string id = HttpContext.Current.Request.Form["id"];
                int iditem = int.Parse(HttpContext.Current.Request.Form["item"]);

                var rm = context.Carts.Where(x => x.CreatedByUserID == id).FirstOrDefault();
                var k = context.CartFoodDetails.Where(i => i.FoodOptionID == iditem).Where(i => i.CartID == rm.ID).FirstOrDefault();
                if (k != null)
                {
                    k.Quantity += 1;
                }
                else
                {
                    var nOrder = new CartFoodDetail() { CartID = rm.ID, FoodOptionID = iditem, Quantity = 1 };

                    context.CartFoodDetails.Add(nOrder);
                }


                context.SaveChanges();
                return Ok(new { data = "Thành công", message = HttpStatusCode.OK });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
