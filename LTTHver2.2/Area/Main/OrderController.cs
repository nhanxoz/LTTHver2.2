using LTTHver2._2.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LTTHver2._2.Area.Main
{
   
    public class OrderController : ApiController
    {
        public LTTH context = new LTTH();
        public OrderController()
        {
            context.Configuration.ProxyCreationEnabled = false;
        }
        [Route("api/user/orderUser")]
        [HttpGet]
        public IHttpActionResult GetAllByUser(string id)
        {
            var order = from a in context.Orders
                        where a.CreatedByUserID == id
                        select new
                        {
                            a.ID,
                            a.Status,
                            a.ToTalPrice,
                            Listmon = (from b in context.OrderFoodDetails
                                       from c in context.Foods 
                                       where b.FoodOptionID == c.ID && b.OrderID == a.ID
                                       select c.Name)
                        };

            return Ok(new { data = order, message = HttpStatusCode.OK });
        }

        [Route("api/user/order")]
        [HttpPost]
        public IHttpActionResult CreateNew(Order order, List<OrderFoodDetail> food)
        {
            try
            {
                var _order = order;
                foreach (var k in food)
                {
                    var price = from a in context.OrderFoodDetails
                                from b in context.FoodOptions
                                from c in context.Foods
                                where a.FoodOptionID == k.FoodOptionID && b.ID == a.FoodOptionID
                                && c.ID == b.FoodID
                                select new
                                {
                                    c.PromotionPrice

                                };
                    _order.ToTalPrice += Convert.ToInt32(price);
                    context.OrderFoodDetails.Add(k);
                }

                context.Orders.Add(_order);
                context.SaveChanges();
                return Ok(new { data = "Thành công", message = HttpStatusCode.OK });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [Route("api/user/order")]
        [HttpPost]
        public IHttpActionResult CancelOrder(int OrderID, int status)
        {
            try
            {
                var rm = context.Foods.Find(OrderID);
                rm.Status = status;
                context.SaveChanges();
                return Ok(new { data = "Thành công", message = HttpStatusCode.OK });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [Route("api/user/orderID")]
        [HttpGet]
        public IHttpActionResult GetOrderByID(int orderID)
        {
            var order = from a in context.Orders
                        where a.ID == orderID
                        select new
                        {
                            a.ID,
                            a.CustomerName,
                            a.CustomerAddress,
                            a.CustomerMessage,
                            a.PaymentMethod,
                            a.CreatedTime,
                            a.Status,
                            a.ToTalPrice
                        };
            List<OrderDetail> _lsorder = new List<OrderDetail>();
            var k = from a in context.OrderFoodDetails
                    from b in context.FoodOptions
                    from c in context.Foods
                    where a.OrderID == orderID && a.FoodOptionID == b.ID && b.FoodID == c.ID
                    select new
                    {
                        c.Name,
                        c.Image,
                        c.OriginPrice,
                        c.PromotionPrice,
                        b.Size,
                        b.Topping,
                        b.BoundingPrice,
                        a.Quantity
                    };
            _lsorder.Add((OrderDetail) k);
            return Ok(new { data1 = order,data2 = _lsorder, message = HttpStatusCode.OK });
        }
        [Route("api/user/order")]
        [HttpGet]
        public IHttpActionResult order(string id)
        {
            var food = from a in context.Foods
                       join b in context.OrderFoodDetails on a.ID equals b.FoodOptionID
                       join c in context.Orders on b.OrderID equals c.ID
                       where c.CreatedByUserID == id
                       select new
                       {
                           a.ID,
                           a.Name,
                           a.Alias,
                           a.OriginPrice,
                           a.PromotionPrice,
                           b.Quantity
                       };
            return Ok(new { data = food, message = HttpStatusCode.OK });
        }
        [Route("api/user/orderDetail")]
        [HttpGet]
        public IHttpActionResult orderDetail(int id)
        {
            var food = from a in context.Foods
                       join b in context.OrderFoodDetails on a.ID equals b.FoodOptionID
                       join c in context.Orders on b.OrderID equals c.ID
                       where c.ID == id
                       select new
                       {
                           a.ID,
                           a.Name,
                           a.Alias,
                           a.OriginPrice,
                           a.PromotionPrice,
                           b.Quantity
                       };
            return Ok(new { data = food, message = HttpStatusCode.OK });
        }
    }
}
