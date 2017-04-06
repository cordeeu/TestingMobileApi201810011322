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
using System.Web.Http.OData;

namespace MobileApi.Controllers
{ 

    public class BaseController : ApiController
    {
        public MobileApiContext db = new MobileApiContext();
        public string currentRepository;

        // This action accepts no params (other than the repository) and also supports OData querying params
        // e.g. api/pumas?$top=10 (gets top ten records)
        // e.g. api/pumas?$filter=Name eq 'Cougar' (returns pumas with name equaling 'Cougar')
        [EnableQuery]
        [Route("api/{repository}")] 
        public IQueryable<Puma> GetPumas()
        {
            return db.Pumas.AsQueryable();
        }

        [Route("api/{repository}/{resourceId}")]
        public async Task<IHttpActionResult> Get(int resourceId)
        {
            Puma puma = await db.Pumas.FindAsync(resourceId);
            if (puma == null)
            {
                return NotFound();
            }

            return Ok(puma);
        }

        [Route("api/{repository}/{resourceId}/images")]
        public IEnumerable<PumaImage> GetImages(int resourceId)
        {
            Repository<PumaImage> currentRepository = new Repository<PumaImage>();
            return currentRepository.GetImages(resourceId);
        }

        [System.Web.Http.Route("api/{repository}/{resourceId}/images/{imageId}")]
        public async Task<HttpResponseMessage> GetImage(int resourceId, int imageId)
        {
            Repository<PumaImage> currentRepository = new Repository<PumaImage>();
            PumaImage pumaImage = await currentRepository.GetImage(imageId);
            if (pumaImage == null)
            {
                //return NotFound();
            }

            var result = new HttpResponseMessage(HttpStatusCode.OK);
            var directory = "~/images/" + resourceId + "/" + pumaImage.ImageFilename;
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