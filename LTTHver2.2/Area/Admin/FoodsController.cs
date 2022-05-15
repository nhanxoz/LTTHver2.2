using LTTHver2._2.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LTTHver2._2.Area.Admin
{
    public class FoodsController : ApiController
    {
        public LTTH context = new LTTH();
        public FoodsController()
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
        [Route("api/admin/food")]
        [HttpPost]
        public IHttpActionResult PostFood(Food food) // đoạn này Dũng nhớ thêm đổi id của food (tự sinh id lấy max id)
        {
            try
            {
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
        [Route("api/admin/food")]
        [HttpPut]
        public IHttpActionResult EditFood(Food food)
        {
            try
            {
                var ed = context.Foods.Find(food.ID);
                ed.Name = food.Name;//sửa từng cái thế này thôi chứ del sửa được luôn :)
                
                ed = food; // dòng này xóa đi nhớ
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
