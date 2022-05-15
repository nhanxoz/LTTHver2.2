using LTTHver2._2.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LTTHver2._2.Area.Admin
{
    [Authorize(Roles ="ADMIN,STAFF")]
    public class BlogController : ApiController
    {
        public LTTH context = new LTTH();
        public BlogController()
        {
            context.Configuration.ProxyCreationEnabled = false;
        }
        [Route("api/admin/blog")]
        [HttpGet]
        public IHttpActionResult httpActionResult()
        {
            var k = context.Blogs.ToList();
            return Ok(new { data = k, message = HttpStatusCode.OK });
        }
        [Route("api/admin/blog")]
        [HttpPost]
        public IHttpActionResult PostBlog(Blog blog)
        {

            try
            {
                var k = context.Blogs.ToList();
                int n = k.OrderByDescending(u => u.ID).FirstOrDefault().ID;
                blog.ID = n + 1;
                context.Blogs.Add(blog);

                context.SaveChanges();
                return Ok(new { data = "Thành công", message = HttpStatusCode.OK });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [Route("api/admin/blog")]
        [HttpDelete]
        public IHttpActionResult DeleteBlog(int id)
        {
            try
            {
                var rm = context.Blogs.Find(id);
                context.Blogs.Remove(rm);
                context.SaveChanges();
                return Ok(new { data = "Thành công", message = HttpStatusCode.OK });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("api/admin/blog")]
        [HttpPut]
        public IHttpActionResult EditBlog(Blog blog)
        {
            try
            {
                Blog ed = context.Blogs.Find(blog.ID);
                ed.Name = blog.Name;
                ed.Alias = blog.Alias;
                ed.Image = blog.Image;
                ed.CategoryID = blog.CategoryID;
                ed.Description = blog.Description;
                ed.Content = blog.Content;
                ed.CreatedDate = blog.CreatedDate;
                ed.CreatedBy = blog.CreatedBy;
                ed.UpdatedDate = blog.UpdatedDate;
                ed.UpdatedBy = blog.UpdatedBy;
                ed.HotFlag = blog.HotFlag;
                ed.ViewCount = blog.ViewCount;
                ed.Status = blog.Status;
                


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