using LTTHver2._2.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LTTHver2._2.Area.Admin
{
    public class aDashboardController : ApiController
    {
        public LTTH context = new LTTH();
        public aDashboardController()
        {
            context.Configuration.ProxyCreationEnabled = false;
        }
        [Route("api/admin/sales")]
        [HttpGet]
        public IHttpActionResult httpActionResult()
        {
            var sale = (from a in context.Foods
                        from b in context.OrderFoodDetails
                        from c in context.Orders
                        where a.ID == b.FoodOptionID && b.OrderID == c.ID
                        orderby c.CreatedTime
                        select new
                        {
                            a.Alias,
                            a.Name,
                            a.OriginPrice,
                            a.PromotionPrice
                        }).Take(5);

            return Ok(new { data = sale, message = HttpStatusCode.OK });
        }
        public class Sell
        {
            public int id { get; set; }
            public string name { get; set; }
            public int c { get; set; }
        }
        public class OrderTK
        {
            public int allorder { get; set; }

            public int neworder { get; set; }
        }
        public class Revenue
        {
            public int? Inmonth { get; set; }

            public int? Lastmonth { get; set; }


        }
        public class CCustomer
        {
            public int? Customer { get; set; }

            public int? NewRegister { get; set; }


        }
        public class Chart
        {
            public int? thang { get; set; }

            public int? ngay { get; set; }

            public int? tong { get; set; }

        }
        [Route("api/admin/bestselling")]
        [HttpGet]
        public IHttpActionResult BestSelling()
        {
            var selling = context.Database.SqlQuery<Sell>("select top 5 b.id, b.name, a.c from(select Foods.ID, count(*) c from Foods join OrderFoodDetails on Foods.ID = OrderFoodDetails.FoodOptionID join Orders on OrderFoodDetails.OrderID = Orders.ID group by Foods.ID) a join Foods b on a.id = b.id order by a.c desc ");

            return Ok(new { data = selling, message = HttpStatusCode.OK });
        }
        [Route("api/admin/allorder")]
        [HttpGet]
        public IHttpActionResult Orders()
        {
            var a = context.Database.SqlQuery<OrderTK>("select B.allorder allorder, A.neworder neworder from(select count(*) neworder from Orders where datediff(day, dateadd(s, Orders.CreatedTime, '1970-01-01'), getdate()) < 32) A, (select count(*) allorder from Orders) B ");

            return Ok(new { data = a, message = HttpStatusCode.OK });
        }
        [Route("api/admin/revenue")]
        [HttpGet]
        public IHttpActionResult Revenues()
        {
            var a = context.Database.SqlQuery<Revenue>("Select *  from(select SUM(ToTalPrice) Inmonth from Orders where month(GETDATE()) - month(CONVERT(INT, CONVERT(DATETIME, CreatedTime))) = 0) B, (select SUM(ToTalPrice) Lastmonth from Orders where month(GETDATE()) - month(CONVERT(INT, CONVERT(DATETIME, CreatedTime))) = 1) A");
            return Ok(new { data = a, message = HttpStatusCode.OK });
        }
        [Route("api/admin/customer")]
        [HttpGet]
        public IHttpActionResult Customers()
        {
            var a = context.Database.SqlQuery<CCustomer>("Select * from(select COUNT(*) Customer from Users) B, (select COUNT(*) NewRegister from Users where datediff(day, dateadd(s, Users.CreatedDay, '1970-01-01'), getdate()) < 7) A");
            return Ok(new { data = a, message = HttpStatusCode.OK });
        }
        [Route("api/admin/chart")]
        [HttpGet]
        public IHttpActionResult Charts()
        {
            var a = context.Database.SqlQuery<Chart>("select month(CreatedTime) thang,day(CreatedTime) ngay, sum(TotalPrice) tong from Orders where month(CreatedTime) = month(getdate()) group by day(CreatedTime), month(CreatedTime) ");
            return Ok(new { data = a, message = HttpStatusCode.OK });
        }
        public class CartAcc
        {
            public int ID { get; set; }

            public string CusName { get; set; }
            public int? Status { get; set; }

        }
        [Route("api/admin/acceptOrder")]
        [HttpGet]
        public IHttpActionResult GetOrder()
        {
            var cart = from a in context.Orders
                       from b in context.Users
                       where a.Status==0 && a.CreatedByUserID == b.Id
                       select new
                       {
                           a.ID,
                           b.FullName,
                           a.Status,
                           Foods = (from d in context.OrderFoodDetails
                                    from e in context.FoodOptions
                                    from c in context.Foods
                                    where d.OrderID == a.ID && d.FoodOptionID == e.ID && e.FoodID == c.ID
                                    select new
                                    {
                                        c.Name,
                                        c.Image,
                                        c.OriginPrice,
                                        c.PromotionPrice,
                                        e.Size,
                                        e.Topping,
                                        e.BoundingPrice,
                                        d.Quantity,
                                        c.Alias
                                    })
                       };
        

            return Ok(new { data = cart, message = HttpStatusCode.OK });
        }
        [Route("api/admin/filterOrder")]
        [HttpGet]
        public IHttpActionResult GetOrderSTT(int stt)
        {
            var cart = from a in context.Orders
                       from b in context.Users
                       where a.Status == stt && a.CreatedByUserID == b.Id
                       select new
                       {
                           a.ID,
                           b.FullName,
                           a.Status,
                           Foods = (from d in context.OrderFoodDetails
                                    from e in context.FoodOptions
                                    from c in context.Foods
                                    where d.OrderID == a.ID && d.FoodOptionID == e.ID && e.FoodID == c.ID
                                    select new
                                    {
                                        c.Name,
                                        c.Image,
                                        c.OriginPrice,
                                        c.PromotionPrice,
                                        e.Size,
                                        e.Topping,
                                        e.BoundingPrice,
                                        d.Quantity,
                                        c.Alias
                                    })
                       };


            return Ok(new { data = cart, message = HttpStatusCode.OK });
        }
        [Route("api/admin/detailOrder")]
        [HttpGet]
        public IHttpActionResult GetDetail(int id)
        {
            List<OrderDetail> _lsfood = new List<OrderDetail>();

            var k = from a in context.OrderFoodDetails
                    from b in context.FoodOptions
                    from c in context.Foods
                    where a.OrderID == id && a.FoodOptionID == b.ID && b.FoodID == c.ID
                    select new
                    {
                        c.Name,
                        c.Image,
                        c.OriginPrice,
                        c.PromotionPrice,
                        b.Size,
                        b.Topping,
                        b.BoundingPrice,
                        a.Quantity,
                        c.Alias
                    };

            return Ok(new { data = k, message = HttpStatusCode.OK });
        }
    }
}
