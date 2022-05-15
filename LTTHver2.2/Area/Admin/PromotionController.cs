using LTTHver2._2.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LTTHver2._2.Area.Admin
{
    public class PromotionController : ApiController
    {
        public LTTH context = new LTTH();
        public PromotionController()
        {
            context.Configuration.ProxyCreationEnabled = false;
        }
        [Route("api/admin/promotion")]
        [HttpGet]
        public IHttpActionResult httpActionResult()
        {
            var k = context.Promotions.ToList();
            return Ok(new { data = k, message = HttpStatusCode.OK });
        }
        [Route("api/admin/promotion")]
        [HttpPost]
        public IHttpActionResult PostPromotion(Promotion pr)
        {

            try
            {
                var k = context.Promotions.ToList();
                int n = k.OrderByDescending(u => u.ID).FirstOrDefault().ID;
                pr.ID = n + 1;
                context.Promotions.Add(pr);

                context.SaveChanges();
                return Ok(new { data = "Thành công", message = HttpStatusCode.OK });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [Route("api/admin/promotion")]
        [HttpDelete]
        public IHttpActionResult DeletePromotion(int id)
        {
            try
            {
                var rm = context.Promotions.Find(id);
                context.Promotions.Remove(rm);
                context.SaveChanges();
                return Ok(new { data = "Thành công", message = HttpStatusCode.OK });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("api/admin/promotion")]
        [HttpPut]
        public IHttpActionResult EditPromotion(Promotion pr)
        {
            try
            {
                Promotion ed = context.Promotions.Find(pr.ID);
                ed.Name = pr.Name;
                ed.Alias = pr.Alias;
                ed.Image = pr.Image;
                ed.BlogID = pr.BlogID;
                ed.Description = pr.Description;
                ed.ActiveDay = pr.ActiveDay;
                ed.EndDay = pr.EndDay;
                ed.Content = pr.Content;
                ed.CreatedDate = pr.CreatedDate;
                ed.CreatedBy = pr.CreatedBy;
                ed.UpdatedDate = pr.UpdatedDate;
                ed.UpdatedBy = pr.UpdatedBy;
                ed.HotFlag = pr.HotFlag;
                ed.ViewCount = pr.ViewCount;
                ed.Status = pr.Status;
                ed.TypeID = pr.TypeID;



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