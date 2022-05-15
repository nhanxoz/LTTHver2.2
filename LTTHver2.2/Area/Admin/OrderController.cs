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
    public class OrderController : ApiController
    {
        public LTTH context = new LTTH();
        public OrderController()
        {
            context.Configuration.ProxyCreationEnabled = false;
        }
        [Route("api/admin/order")]
        [HttpGet]
        public IHttpActionResult httpActionResult()
        {
            var k = context.Orders.ToList();
            return Ok(new { data = k, message = HttpStatusCode.OK });
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
                var rm = context.Orders.Find(id);
                context.Orders.Remove(rm);
                context.SaveChanges();
                return Ok(new { data = "Thành công", message = HttpStatusCode.OK });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("api/admin/order")]
        [HttpPut]
        public IHttpActionResult EditOrder(Order order)
        {
            try
            {
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
        [Route("api/admin/order")]
        [HttpPut]
        public IHttpActionResult EditOrderStatus(int id)
        {
            try
            {
                Order ed = context.Orders.Find(id);
                if (ed.Status != null && ed.Status < 6) ed.Status = ed.Status + 1; else if (ed.Status == null) ed.Status = 1;
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
