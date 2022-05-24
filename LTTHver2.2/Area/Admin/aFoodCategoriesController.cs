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
    public class aFoodCategoriesController : ApiController
    {
        public LTTH context = new LTTH();
        public aFoodCategoriesController()
        {
            context.Configuration.ProxyCreationEnabled = false;
        }
        [Route("api/admin/categoryfood")]
        [HttpGet]
        public IHttpActionResult httpActionResult()
        {
            var k = context.FoodCategories.ToList();
            return Ok(new { data = k, message = HttpStatusCode.OK });
        }
        [Route("api/admin/categoryfood")]
        [HttpPost]
        public IHttpActionResult PostFoodCategory(FoodCategory foodcategory) 
        {

            try
            {
                var k = context.FoodCategories.ToList();
                int n = k.OrderByDescending(u => u.ID).FirstOrDefault().ID;
                foodcategory.ID = n + 1;
                context.FoodCategories.Add(foodcategory);

                context.SaveChanges();
                return Ok(new { data = "Thành công", message = HttpStatusCode.OK });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [Route("api/admin/categoryfood")]
        [HttpDelete]
        public IHttpActionResult DeleteFoodCategory(int id)
        {
            try
            {
                var rm = context.FoodCategories.Find(id);
                context.FoodCategories.Remove(rm);
                context.SaveChanges();
                return Ok(new { data = "Thành công", message = HttpStatusCode.OK });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("api/admin/categoryfood")]
        [HttpPut]
        public IHttpActionResult EditFoodCategory(FoodCategory foodcategory)
        {
            try
            {
                FoodCategory ed = context.FoodCategories.Find(foodcategory.ID);
                ed.Name = foodcategory.Name;
                ed.Alias = foodcategory.Alias;
                ed.ParentID = foodcategory.ParentID;
                ed.DisplayOrder = foodcategory.DisplayOrder;
                ed.CreatedDate = foodcategory.CreatedDate;
                ed.CreatedBy = foodcategory.CreatedBy;
                ed.UpdatedDate = foodcategory.UpdatedDate;
                ed.UpdatedBy = foodcategory.UpdatedBy;
                ed.Status = foodcategory.Status;
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
