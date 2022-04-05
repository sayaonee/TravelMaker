using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using System.Web.Mvc;
using TravelMaker.Models;

namespace TravelMaker.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ImageController : ApiController
    {
        [System.Web.Http.HttpPost]
        // POST: api/Image/ImageUpload
        public async Task<HttpResponseMessage> ImageUpload()
        {
            //public IHttpActionResult ImageUpload(HttpPostedFileBase photo)
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            string root = HttpContext.Current.Server.MapPath("~/Image");
            var provider = new MultipartFormDataStreamProvider(root);
            List<string> imgNames = new List<string>();
            MD5 md5 = new MD5CryptoServiceProvider();
            await Request.Content.ReadAsMultipartAsync(provider);
            foreach (MultipartFileData file in provider.FileData)
            {
                FileStream fs = new FileStream(file.LocalFileName, FileMode.Open);
                FileInfo fi = new FileInfo(file.LocalFileName);
                byte[] imgHash = md5.ComputeHash(fs);
                fs.Dispose();
                string imgHashStr = BitConverter.ToString(imgHash).Replace("-", "");
                string filename = file.Headers.ContentDisposition.FileName.Trim('"');
                string fileExt = filename.Substring(filename.LastIndexOf('.'));
                string saveUrl = Path.Combine(root, imgHashStr + fileExt);
                try
                { 
                    fi.MoveTo(saveUrl);
                }
                catch{}
                imgNames.Add(imgHashStr + fileExt);
            }
            return Request.CreateResponse(imgNames);
        }
        [System.Web.Http.HttpGet]
        // GET: api/Image/GetImage
        public IHttpActionResult GetImage(string imgName)
        {
            string root = HttpContext.Current.Server.MapPath("~/Image");
            string path = Path.Combine(root, imgName);
            string fileExt = imgName.Substring(imgName.LastIndexOf('.'));
            var mimeType = MimeMapping.GetMimeMapping(path);
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            try
            {
                var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                response.Content = new StreamContent(fs);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue(mimeType);
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = HttpUtility.UrlPathEncode(imgName)
                };
                response.Content.Headers.ContentLength = fs.Length;

                return ResponseMessage(response);
            }
            catch
            {
                return BadRequest("檔案不存在");
            }
        }
    }
}
