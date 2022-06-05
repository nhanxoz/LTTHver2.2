using LTTHver2._2.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace LTTHver2._2.Area.Main
{
    
    public class UserController : ApiController
    {
        public LTTH context = new LTTH();
        public UserController()
        {
            context.Configuration.ProxyCreationEnabled = false;
        }
        [Route("api/user")]
        [HttpGet]
        public IHttpActionResult GetUser(string id)
        {
            var user = from a in context.Users
                       where a.Id == id
                       select new
                       {
                           a.Id,
                           a.FullName,
                           a.Image,
                           a.PhoneNumber,
                           a.UserName,
                           a.Password
                        
                           
                       };
            return Ok(new { data = user, message = HttpStatusCode.OK });
        }
        [Route("api/user")]
        [HttpGet]
        public IHttpActionResult getID(string email)
        {
            var user = from a in context.Users
                       where a.Email == email
                       select new
                       {
                           a.Id,
                           a.FullName,
                           a.Image,
                           a.PhoneNumber,
                           a.UserName,
                           a.Password


                       };
            return Ok(new { data = user, message = HttpStatusCode.OK });
        }
        [Route("api/user")]
        [HttpGet]
        public IHttpActionResult GetUser(User user)
        {
            try
            {

                User _user = context.Users.Find(user.Id);
                _user = user;
                context.SaveChanges();

                return Ok(new { data = user, message = HttpStatusCode.OK });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
