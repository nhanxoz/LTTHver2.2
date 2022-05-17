using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using LTTHver2._2.Models.EF;

namespace LTTHver2._2.Area.Main
{
    public class FoodController : ApiController
    {
        public LTTH context = new LTTH();
        public FoodController()
        {
            context.Configuration.ProxyCreationEnabled = false;
        }

        [Route("api/user/food")]
        [HttpGet]
        public IHttpActionResult httpActionResult()
        {
            var k = context.Foods.ToList();
            return Ok(new { data = k, message = HttpStatusCode.OK });
        }

        [Route("api/user/food")]
        [HttpGet]
        public IHttpActionResult GetByID(int id)
        {
            var food = context.Foods.Find(id);
            return Ok(new { data = food, message = HttpStatusCode.OK });
        }
        [Route("api/user/Category")]
        [HttpGet]
        public IHttpActionResult GetByCat(int id)
        {
            var food = from a in context.Foods
                       join b in context.FoodCategories on a.CategoryID equals b.ID
                       select new
                       {
                           a.ID,
                           a.Name,
                           a.Image,
                           a.OriginPrice,
                           a.PromotionFoodDetails,
                           a.PromotionPrice,
                           a.Status
                       };
            return Ok(new { data = food, message = HttpStatusCode.OK });
        }


    }
}
