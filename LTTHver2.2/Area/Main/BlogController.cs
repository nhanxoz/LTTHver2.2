using LTTHver2._2.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LTTHver2._2.Area.Main
{
    public class BlogController : ApiController
    {
        public LTTH context = new LTTH();
        public BlogController()
        {
            context.Configuration.ProxyCreationEnabled = false;
        }
        [Route("api/blog")]
        [HttpGet]
        public IHttpActionResult Blog()
        {
            var blog = from a in context.Blogs
                       select new
                       {
                           a.ID,
                           a.Name,
                           a.Status,
                           a.Image,
                           a.Content,
                           a.BlogComments,
                           a.CreatedDate
                       };
            return Ok(new { data = blog, message = HttpStatusCode.OK });
        }
        [Route("api/blog")]
        [HttpGet]
        public IHttpActionResult BlogByID(int id)
        {
            var blog = from a in context.Blogs
                       where a.ID == id
                       select new
                       {
                           a.ID,
                           a.Name,
                           a.Status,
                           a.Image,
                           a.Content,
                           a.BlogComments,
                           a.CreatedDate
                       };
            return Ok(new { data = blog, message = HttpStatusCode.OK });
        }
        [Route("api/blog")]
        [HttpPost]
        public IHttpActionResult CreateNewBlog(Blog blog)
        {
            var _blog = blog;
            context.Blogs.Add(_blog);
            context.SaveChanges();
            return Ok(new { data = "Thành công", message = HttpStatusCode.OK });
        }
    }
}
