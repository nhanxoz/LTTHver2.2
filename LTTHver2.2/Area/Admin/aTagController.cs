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
    public class aTagController : ApiController
    {
        public LTTH context = new LTTH();
        public aTagController()
        {
            context.Configuration.ProxyCreationEnabled = false;
        }
        [Route("api/admin/tag")]
        [HttpGet]
        public IHttpActionResult httpActionResult()
        {
            var k = context.Tags.ToList();
            return Ok(new { data = k, message = HttpStatusCode.OK });
        }
        [Route("api/admin/tag")]
        [HttpPost]
        public IHttpActionResult PostTag(Tag t)
        {

            try
            {
                var k = context.Tags.ToList();
                int n = k.OrderByDescending(u => u.ID).FirstOrDefault().ID;
                t.ID = n + 1;
                context.Tags.Add(t);

                context.SaveChanges();
                return Ok(new { data = "Thành công", message = HttpStatusCode.OK });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [Route("api/admin/tag")]
        [HttpDelete]
        public IHttpActionResult DeleteTag(int id)
        {
            try
            {
                var rm = context.Tags.Find(id);
                context.Tags.Remove(rm);
                context.SaveChanges();
                return Ok(new { data = "Thành công", message = HttpStatusCode.OK });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("api/admin/tag")]
        [HttpPut]
        public IHttpActionResult EditTag(Tag t)
        {
            try
            {
                Tag ed = context.Tags.Find(t.ID);
                ed.Name = t.Name;
                ed.Alias = t.Alias;
                ed.Type = t.Type;
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