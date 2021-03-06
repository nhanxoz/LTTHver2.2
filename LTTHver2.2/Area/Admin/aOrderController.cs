using LTTHver2._2.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LTTHver2._2.Area.Admin
{
    [Authorize(Roles = "ADMIN,STAFF")]
    public class aOrderController : ApiController
    {
        public LTTH context = new LTTH();
        public aOrderController()
        {
            context.Configuration.ProxyCreationEnabled = false;
        }
        [Route("api/Order")]
        [HttpGet]
        public IHttpActionResult httpActionResult()
        {
            var order = from a in context.OrderFoodDetails
                        from b in context.Orders
                        from c in context.FoodOptions
                        from d in context.Foods
                        where b.ID == a.OrderID && a.FoodOptionID == c.ID &&
                        c.ID == d.ID 
                        select new
                        {
                            b.ID,
                            b.CreatedByUserID,
                            b.CustomerName,
                            b.CustomerAddress,
                            a.FoodOptionID,
                            d.Name,
                            d.Image,
                            d.OriginPrice,
                            d.PromotionPrice,
                            a.Quantity,
                            b.PaymentMethod,
                            d.Alias,
                            b.Status
                        };

            return Ok(new { data = order, message = HttpStatusCode.OK });
        }
        [Route("api/admin/order")]
        [HttpPost]
        public IHttpActionResult PostOrder(Order order)
        {

            try
            {
                var k = context.FoodCategories.ToList();
                int n = k.OrderByDescending(u => u.ID).FirstOrDefault().ID;
                order.ID = n + 1;
                context.Orders.Add(order);

                context.SaveChanges();
                return Ok(new { data = "Thành công", message = HttpStatusCode.OK });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [Route("api/admin/order")]
        [HttpDelete]
        public IHttpActionResult DeleteOrder(int id)
        {
            try
            {
                var k = context.OrderFoodDetails.Where(x=>x.FoodOptionID==id).FirstOrDefault();
                context.OrderFoodDetails.Remove(k);
                context.SaveChanges();
                return Ok(new { data = "Thành công", message = HttpStatusCode.OK });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("api/admin/editorder")]
        [HttpPost]
        public IHttpActionResult EditOrder(Order order)
        {
            try
            {
                Console.Write(order);
                Order ed = context.Orders.Find(order.ID);
                ed.CustomerName = order.CustomerName;
                ed.CustomerAddress = order.CustomerAddress;
                ed.CustomerMessage = order.CustomerMessage;
                ed.PaymentMethod = order.PaymentMethod;
                ed.CreatedTime = order.CreatedTime;
                ed.CreatedByUserID = order.CreatedByUserID;
                ed.Status = order.Status;
                ed.IDUser = order.IDUser;
                ed.ToTalPrice = order.ToTalPrice;

                context.SaveChanges();
                return Ok(new { data = "Thành công", message = HttpStatusCode.OK });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("api/admin/editstatus")]
        [HttpPut]
        public IHttpActionResult EditOrderStatus(int ID, int stt)
        {
            try
            {
                Order ed = context.Orders.Find(ID);
                if (ed.Status != null) ed.Status = stt;
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
