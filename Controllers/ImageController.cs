using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using PikyServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace PikyServer.Controllers
{
    [Route("api/{controller}")]
    public class ImageController : Controller
    {

        [HttpPost, Authorize(Roles = "Admin")]      
        public ActionResult UploadImage( )
        {      
            
            string imageName = null;
            var httpRequest = HttpContext.Request;
            //Upload Image
            var postedFile = httpRequest.Form.Files["Image"];
            //Create custom filename
            imageName = new String(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray()).Replace(" ", "-");
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedFile.FileName);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", imageName);

            using (var bits = new FileStream(path, FileMode.Create))
            {
                postedFile.CopyTo(bits);
            }

            return Ok( new
            {
                imageName
            });
           // return Request.CreateResponse(HttpStatusCode.Created, filePath);
        }
        [HttpGet]        
        public ActionResult ImageGet([FromHeader]string name)
        {
            
            var path = $"wwwroot//images//{name}";
            byte[] b = System.IO.File.ReadAllBytes(path);
            var contents = "data:image/png;base64," + Convert.ToBase64String(b);

            return Ok( new {
                image = contents
            } );
        }
    }
}
