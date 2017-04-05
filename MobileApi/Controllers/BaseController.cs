using System;
using System.Collections.Generic;
using System.Web.Http;
using MobileApi.Models;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net.Http.Headers;
using System.Linq;

namespace MobileApi.Controllers
{

    public class BaseController : ApiController
    {
        public MobileApiContext db = new MobileApiContext();
        public string currentRepository;

        [System.Web.Http.Route("api/{repository}")]
        public IEnumerable<PumaType> GetAll(string repository)
        {
            PumaType currentRepository = new PumaType();
            return currentRepository.GetAll();
        }

        [System.Web.Http.Route("api/{repository}/{resourceId}")]
        public async Task<IHttpActionResult> Get(int resourceId)
        {
            PumaType pumaType = await db.PumaTypes.FindAsync(resourceId);
            if (pumaType == null)
            {
                return NotFound();
            }

            return Ok(pumaType);
        }

        [System.Web.Http.Route("api/{repository}/{resourceId}/images")]
        public IEnumerable<PumaTypeImage> GetImages(int resourceId)
        {
            Repository<PumaTypeImage> currentRepository = new Repository<PumaTypeImage>();
            return currentRepository.GetImages(resourceId);
        }

        [System.Web.Http.Route("api/{repository}/{resourceId}/images/{imageId}")]
        public async Task<HttpResponseMessage> GetImage(int resourceId, int imageId)
        {
            Repository<PumaTypeImage> currentRepository = new Repository<PumaTypeImage>();
            PumaTypeImage pumaTypeImage = await currentRepository.GetImage(imageId);
            if (pumaTypeImage == null)
            {
                //return NotFound();
            }

            var result = new HttpResponseMessage(HttpStatusCode.OK);
            var directory = "~/images/" + resourceId + "/" + pumaTypeImage.ImageFilename;
            String filePath = HostingEnvironment.MapPath(directory);
            FileStream fileStream = new FileStream(filePath, FileMode.Open);
            Image image = Image.FromStream(fileStream);
            MemoryStream memoryStream = new MemoryStream();
            image.Save(memoryStream, ImageFormat.Jpeg);
            result.Content = new ByteArrayContent(memoryStream.ToArray());
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");

            return result;
        }

    }
}