using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace LTTHver2._2.Area.Admin
{
    public class aUploadImageController : ApiController
    {
        [Route("api/ImageAPI/UploadFiles")]
        [HttpPost]
        public HttpResponseMessage UploadFiles()
        {
            //Create the Directory.
            string path = HttpContext.Current.Server.MapPath("~/Content/food/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //Fetch the File.
            HttpPostedFile postedFile = HttpContext.Current.Request.Files[0];

            //Fetch the File Name.
            //string fileName = Path.GetFileName(postedFile.FileName);
            string fileName_ = HttpContext.Current.Request.Form["filename"];

            //Save the File.
            postedFile.SaveAs(path + fileName_);

            //Send OK Response to Client.
            return Request.CreateResponse(HttpStatusCode.OK, fileName_);
        }

        [HttpPost]
        [Route("api/ImageAPI/GetFiles")]
        public HttpResponseMessage GetFiles()
        {
            string path = HttpContext.Current.Server.MapPath("~/Content/food");

            //Fetch the Image Files.
            List<string> images = new List<string>();

            //Extract only the File Names to save data.
            foreach (string file in Directory.GetFiles(path))
            {
                images.Add(Path.GetFileName(file));
            }

            return Request.CreateResponse(HttpStatusCode.OK, images);
        }
    }
}
