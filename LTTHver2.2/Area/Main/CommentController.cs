using LTTHver2._2.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
    }
}
