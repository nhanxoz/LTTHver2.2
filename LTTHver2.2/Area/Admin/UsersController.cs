using LTTHver2._2.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LTTHver2._2.Area.Admin
{
    public class UsersController : ApiController
    {
        public LTTH context = new LTTH();
        public UsersController()
        {
            context.Configuration.ProxyCreationEnabled = false;
        }
        [Route("api/admin/users")]
        [HttpGet]
        public IHttpActionResult httpActionResult()
        {
            var k = context.Users.ToList();
            return Ok(new { data = k, message = HttpStatusCode.OK });
        }
        [Route("api/admin/users")]
        [HttpPost]
        public IHttpActionResult PostUsers(User user)
        {

            try
            {
                var k = context.Users.ToList();
                int n = k.OrderByDescending(u => u.ID).FirstOrDefault().ID;
                user.ID = n + 1;
                context.Users.Add(user);

                context.SaveChanges();
                return Ok(new { data = "Thành công", message = HttpStatusCode.OK });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [Route("api/admin/users")]
        [HttpDelete]
        public IHttpActionResult DeleteUsers(int id)
        {
            try
            {
                var rm = context.Users.Find(id);
                context.Users.Remove(rm);
                context.SaveChanges();
                return Ok(new { data = "Thành công", message = HttpStatusCode.OK });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("api/admin/users")]
        [HttpPut]
        public IHttpActionResult EditUser(User user)
        {
            try
            {
                User ed = context.Users.Find(user.ID);
                ed.UserName = user.UserName;
                ed.Password = user.Password;
                ed.FullName = user.FullName;
                ed.Address = user.Address;
                ed.Email = user.Email;
                ed.City = user.City;
                ed.Country = user.Country;
                ed.PhoneNumber = user.PhoneNumber;
                ed.Description = user.Description;
                ed.Image = user.Image;
                ed.BirthDay = user.BirthDay;
                ed.Career = user.Career;
                ed.CreatedDay = user.CreatedDay;
                ed.Level = user.Level;
                ed.IDChucVu = user.IDChucVu;
                ed.Wallet = user.Wallet;
                ed.RequiresVerification = user.RequiresVerification;


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