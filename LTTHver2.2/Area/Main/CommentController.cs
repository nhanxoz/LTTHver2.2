using LTTHver2._2.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;


namespace LTTHver2._2.Area.Main
{
    public class CommentController : ApiController
    {
        public LTTH context = new LTTH();
        public CommentController()
        {
            context.Configuration.ProxyCreationEnabled = false;
        }
        [Route("api/comment")]
        [HttpGet]
        public IHttpActionResult GetAllComment(int idblog)
        {
            var comment = from a in context.BlogComments
                          where a.BlogID == idblog
                          select new
                          {
                              a.DisplayOrder,
                              a.Content,
                              a.CreatedBy,
                              a.CreatedDate
                          };
            return Ok(new { data = comment, message = HttpStatusCode.OK });
        }
        [Route("api/food/comment")]
        [HttpGet]
        public IHttpActionResult getCommentFood(int idfood)
        {
            var comment = from a in context.FoodComments
                          where a.FoodID == idfood && a.Status == 1
                          select new
                          {
                              a.DisplayOrder,
                              a.Content,
                              a.CreatedBy,
                              a.CreatedDate
                          };
            return Ok(new { data = comment, message = HttpStatusCode.OK });
        }
        [Route("api/user/addComment")]
        [HttpPost]
        public IHttpActionResult addComment()
        {
            try
            {
                string id = HttpContext.Current.Request.Form["id"];
                string idfood = HttpContext.Current.Request.Form["idfood"];
                string content = (HttpContext.Current.Request.Form["content"]);

                var rm = context.Users.Where(x => x.Id == id).FirstOrDefault();
                string name = rm.FullName;
                var newcomment = new FoodComment() { CreatedBy = name, CreatedDate = DateTime.Now, Content = content , FoodID = int.Parse(idfood), Status=1};
                
                
                    context.FoodComments.Add(newcomment);
                


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
