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
    public class aCommentController : ApiController
    {
        public LTTH context = new LTTH();
        public aCommentController()
        {
            context.Configuration.ProxyCreationEnabled = false;
        }
        [Route("api/admin/comment")]
        [HttpGet]
        public IHttpActionResult httpActionResult()
        {
            var k = context.FoodComments.ToList();
            return Ok(new { data = k, message = HttpStatusCode.OK });
        }
        [Route("api/admin/comment")]
        [HttpPost]
        public IHttpActionResult PostComment(FoodComment cm)
        {

            try
            {
                var k = context.FoodComments.ToList();
                int n = k.OrderByDescending(u => u.ID).FirstOrDefault().ID;
                cm.ID = n + 1;
                context.FoodComments.Add(cm);

                context.SaveChanges();
                return Ok(new { data = "Thành công", message = HttpStatusCode.OK });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [Route("api/admin/comment")]
        [HttpDelete]
        public IHttpActionResult DeleteCommnent(int id)
        {
            try
            {
                var rm = context.FoodComments.Find(id);
                context.FoodComments.Remove(rm);
                context.SaveChanges();
                return Ok(new { data = "Thành công", message = HttpStatusCode.OK });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("api/admin/comment")]
        [HttpPut]
        public IHttpActionResult EditComnment(FoodComment cm)
        {
            try
            {
                FoodComment ed = context.FoodComments.Find(cm.ID);
                ed.FoodID = cm.FoodID;
                ed.DisplayOrder = cm.DisplayOrder;
                ed.ParentID = cm.ParentID;
                ed.Content = cm.Content;
                ed.CreatedDate = cm.CreatedDate;
                ed.CreatedBy = cm.CreatedBy;
                ed.UpdatedDate = cm.UpdatedDate;
                ed.UpdatedBy = cm.UpdatedBy;
                ed.Status = cm.Status;



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