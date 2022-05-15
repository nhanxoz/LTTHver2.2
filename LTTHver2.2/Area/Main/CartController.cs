using LTTHver2._2.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LTTHver2._2.Area.Main
{
    [Authorize]
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

    }
    }
