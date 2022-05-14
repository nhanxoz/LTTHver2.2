using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LTTHver2._2.Models.EF;

namespace LTTHver2._2.Area.Main
{
    public class PromotionController : ApiController
    {
        public LTTH context = new LTTH();
        public PromotionController()
        {
            context.Configuration.ProxyCreationEnabled = false;
        }
        [Route("api/promotion")]
        [HttpGet]
        public IHttpActionResult GetAllPromotion()
        {
            var promo = from a in context.Promotions
                        select new
                        {
                            a.ID,
                            a.Name,
                            a.Description,
                            a.ActiveDay,
                            a.EndDay
                        };
            return Ok(new { data = promo, message = HttpStatusCode.OK });
        }
        [Route("api/promotion")]
        [HttpGet]
        public IHttpActionResult GetPromotionByFood(int idfood)
        {
            var promo = from a in context.Promotions
                        from b in context.PromotionFoodDetails
                        where a.ID == b.PromotionID && b.FoodID==idfood
                        select new
                        {
                            a.ID,
                            a.Name,
                            a.Description,
                            a.ActiveDay,
                            a.EndDay
                        };
            return Ok(new { data = promo, message = HttpStatusCode.OK });
        }

     }
}
