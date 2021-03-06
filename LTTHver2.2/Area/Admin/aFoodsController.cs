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
    public class aFoodsController : ApiController
    {
        public LTTH context = new LTTH();
        public aFoodsController()
        {
            context.Configuration.ProxyCreationEnabled = false;
        }
        [Authorize]
        [Route("api/admin/food")]
        [HttpGet]
        public IHttpActionResult httpActionResult()
        {
            var k = context.Foods.ToList();
            return Ok(new { data = k , message=HttpStatusCode.OK});
        }
        [Authorize]
        [Route("api/admin/categories")]
        [HttpGet]
        public IHttpActionResult categories()
        {
            var k = context.FoodCategories.ToList();
            return Ok(new { data = k, message = HttpStatusCode.OK });
        }

        [Route("api/admin/food")]
        [HttpPost]
        public IHttpActionResult PostFood(Food food) 
        {
            
            try
            {
                var k = context.Foods.ToList();
                int n = k.OrderByDescending(u => u.ID).FirstOrDefault().ID;
                food.ID = n + 1;
                context.Foods.Add(food);
            
            context.SaveChanges();
            return Ok(new { data = "Thành công", message = HttpStatusCode.OK });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        [Route("api/admin/food")]
        [HttpDelete]
        [Authorize(Roles="ADMIN")]
        public IHttpActionResult DeleteFood(int id)
        {
            try
            {
                var rm = context.Foods.Find(id);
                context.Foods.Remove(rm);
                context.SaveChanges();
                return Ok(new { data = "Thành công", message = HttpStatusCode.OK });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("api/admin/editfood")]
        [HttpPut]
        public IHttpActionResult EditFood(Food food)
        {
            try
            {
                Food ed = context.Foods.Find(food.ID);
                ed.CategoryID = food.CategoryID;
                ed.Name = food.Name;
                ed.Alias = food.Alias;
                ed.Image = food.Image;
                ed.OriginPrice = food.OriginPrice;
                ed.PromotionPrice = food.PromotionPrice;
                ed.CategoryID = food.CategoryID;
                ed.Description = food.Description;
                ed.Content = food.Content;
                ed.CreatedDate = food.CreatedDate;
                ed.CreatedBy = food.CreatedBy;
                ed.UpdatedDate = food.UpdatedDate;
                ed.UpdatedBy = food.UpdatedBy;
                ed.HotFlag = food.HotFlag;
                ed.ViewCount = food.ViewCount;
                ed.Status = food.Status;
                ed.SlideID = food.SlideID;
                ed.Slide = food.Slide;
                ed.PromotionFoodDetails = food.PromotionFoodDetails;
                ed.Tags = food.Tags;

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
