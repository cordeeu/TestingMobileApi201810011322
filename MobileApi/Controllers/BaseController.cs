using System;
using System.Collections.Generic;
using System.Web.Http;
using MobileApi.Models;
using MobileApi.Helpers;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Net.Http;
using System.Net;
using System.IO;
using NetTopologySuite.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net.Http.Headers;
using System.Linq;
using System.Web.Http.OData;
using System.Data;
using System.Data.OleDb;
using System.Web.Script.Serialization;
using System.Web;
using System.Data.Entity;
using Ionic.Zip;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using NetTopologySuite.IO.GML2;
using NetTopologySuite.Geometries;
using MobileApi.Models.projection;

namespace MobileApi.Controllers
{ 
    public class BaseController : ApiController
    {
        public WoodyPlantsMobileApiContext woodyDb = new WoodyPlantsMobileApiContext();
        public WetlandPlantsMobileApiContext wetlandDb = new WetlandPlantsMobileApiContext();
        public FlowerPlantsMobileApiContext flowerDb= new FlowerPlantsMobileApiContext();
        public string currentRepository;

        // This action accepts no params (other than the repository) and also supports OData querying params
        // e.g. api/wetland?$top=10 (gets top ten records)
        // e.g. api/wetland?$filter=commonname eq 'Acer' (returns pumas with name equaling 'Acer')
        [EnableQuery]
        [Route("api/wetland")]
        public IQueryable<WetlandPlant> GetWetlandPlants()
        {
            wetlandDb.Configuration.ProxyCreationEnabled = false;
            //DbSet<WetlandPlant> allPlants = wetlandDb.Plants;
            List<WetlandPlant> allPlants = wetlandDb.Plants.Include(x => x.Images).Include(x => x.References).Include(x => x.SimilarSpecies).Include(x => x.FruitWetland).Include(x => x.DivisionWetland).Include(x => x.ShapeWetland).Include(x => x.ArrangementWetland).Include(x => x.SizeWetland).Include(x => x.CountyPlantWetland).Include(x => x.SizeWetland).Include(x => x.RegionWetland).ToList().ToList();
            return allPlants.AsQueryable();
        }

        [EnableQuery]
        [Route("api/wetland/fruits")]
        public IQueryable<FruitWetland> GetWetlandFruits()
        {
            wetlandDb.Configuration.ProxyCreationEnabled = false;
            //DbSet<WetlandPlant> allPlants = wetlandDb.Plants;
            List<FruitWetland> allFruits = wetlandDb.FruitWetland.Include(x => x.WetlandPlant).ToList();
            //.Include(x=>x.SimilarSpeciesWetland).Include(x=>x.CountyPlantWetland)
            //
            //.Include(x=>x.SizeWetland).Include(x=>x.RegionWetland).ToList();
            return allFruits.AsQueryable();
        }

        [EnableQuery]
        [Route("api/woody")]
        public IQueryable<WoodyPlant> GetWoodyPlants()
        {
            woodyDb.Configuration.ProxyCreationEnabled = false;
            List<WoodyPlant> allPlants = woodyDb.Plants.ToList();
                //.Include(x => x.Images).ToList();
            return allPlants.AsQueryable();
        }

        [EnableQuery]
        [Route("api/flower")]
        public IQueryable<Flower> GetFlowerPlants()
        {
            flowerDb.Configuration.ProxyCreationEnabled = false;
            List<Flower> allPlants = flowerDb.Plants.ToList();
            //.Include(x => x.Images).ToList();
            return allPlants.AsQueryable();
        }

        [EnableQuery]
        [Route("api/wetland_settings/{settingName}")]
        public IHttpActionResult GetWetlandSetting(string settingName)
        {
            WetlandSetting setting = wetlandDb.WetlandSettings.Where(b => b.name == settingName).FirstOrDefault();
            if (setting == null)
            {
                return NotFound();
            }
            return Ok(setting);
        }

        [EnableQuery]
        [Route("api/woody_settings/{settingName}")]
        public IHttpActionResult GetWoodySetting(string settingName)
        {
            WoodySetting setting = woodyDb.WoodySettings.Where(b => b.name == settingName).FirstOrDefault();
            if (setting == null)
            {
                return NotFound();
            }
            return Ok(setting);
        }

        [EnableQuery]
        [Route("api/flower_settings/{settingName}")]
        public IHttpActionResult GetFlowerSetting(string settingName)
        {
            FlowerSetting setting = flowerDb.FlowerSettings.Where(b => b.name == settingName).FirstOrDefault();
            if (setting == null)
            {
                return NotFound();
            }
            return Ok(setting);
        }

        [EnableQuery]
        [Route("api/wetland_settings/images")]
        public List<WetlandSetting> GetWetlandImageZipFileSettings()
        {
            List<WetlandSetting> wetlandImageZipFileSettings = wetlandDb.WetlandSettings.Where(b => b.name == "ImagesZipFile").ToList();
            return wetlandImageZipFileSettings;
        }

        [EnableQuery]
        [Route("api/wetland_glossary")]
        public List<WetlandGlossary> GetWetlandGlossary()
        {
            List<WetlandGlossary> wetlandGlossary = wetlandDb.WetlandGlossary.ToList();
            return wetlandGlossary;
        }

        // Get zipped images
        [Route("api/wetland/image_zip_files/{fileName}")]
           public HttpResponseMessage GetImagesZip(string fileName)
           {
                var directory = (Debugger.IsAttached == true) ? "C:\\Users\\kbrown\\Documents\\Visual Studio 2017\\Projects\\" + fileName + ".zip" : "~/Resources/Images/Wetlands/" + fileName + ".zip";
                String filePath = HostingEnvironment.MapPath(directory);
                using (ZipFile zip = ZipFile.Read(filePath))
                {
                    var pushStreamContent = new PushStreamContent((stream, content, context) =>
                    {
                        zip.Save(stream);
                        stream.Close();
                    }, "application/zip");

                    HttpResponseMessage response = new HttpResponseMessage();
                    response.StatusCode = HttpStatusCode.OK;
                    response.Content = pushStreamContent;
                    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = fileName + ".zip" };

                    return response;
                }
            
           }

        [Route("api/woody/image_zip_files/{fileName}")]
        public HttpResponseMessage GetImagesZipWoody(string fileName)
        {
            var directory = (Debugger.IsAttached == true) ? "C:\\Users\\kbrown\\Documents\\Visual Studio 2017\\Projects\\" + fileName + ".zip" : "~/Resources/Images/Woody/" + fileName + ".zip";
            String filePath = HostingEnvironment.MapPath(directory);
            using (ZipFile zip = ZipFile.Read(filePath))
            {
                var pushStreamContent = new PushStreamContent((stream, content, context) =>
                {
                    zip.Save(stream);
                    stream.Close();
                }, "application/zip");

                HttpResponseMessage response = new HttpResponseMessage();
                response.StatusCode = HttpStatusCode.OK;
                response.Content = pushStreamContent;
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = fileName + ".zip" };

                return response;
            }

        }

        [Route("api/flower/image_zip_files/{fileName}")]
        public HttpResponseMessage GetImagesZipFlower(string fileName)
        {
            var directory = (Debugger.IsAttached == true) ? "C:\\Users\\kbrown\\Documents\\Visual Studio 2017\\Projects\\" + fileName + ".zip" : "~/Resources/Images/Flower/" + fileName + ".zip";
            String filePath = HostingEnvironment.MapPath(directory);
            using (ZipFile zip = ZipFile.Read(filePath))
            {
                var pushStreamContent = new PushStreamContent((stream, content, context) =>
                {
                    zip.Save(stream);
                    stream.Close();
                }, "application/zip");

                HttpResponseMessage response = new HttpResponseMessage();
                response.StatusCode = HttpStatusCode.OK;
                response.Content = pushStreamContent;
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = fileName + ".zip" };

                return response;
            }

        }

        //[AuthenticationTokenWetlands]
        [EnableQuery]
        [Route("api/wetland/images/{Id}")]
        public HttpResponseMessage GetImage(int Id)
        {

            ImagesWetland wetlandImage = wetlandDb.ImagesWetland.FirstOrDefault(b => b.id == Id);
            if (wetlandImage == null)
            {
                return this.Request.CreateResponse(HttpStatusCode.NotFound, "Image not found");
            }
            else
            {
                var filePath = "~/Resources/Images/Wetlands/" + wetlandImage.filename;
                var result = GetImageFile(filePath);
                return result;
            }
        }

        [Route("api/wetland/image_name/{filename}")]
        public HttpResponseMessage GetImageByFileName(string filename)
        {
            var filePath = "~/Resources/Images/Wetlands/" + filename + ".jpg";
            var result = GetImageFile(filePath);
            return result;
        }

        [Route("api/woody/image_name/{filename}")]
        public HttpResponseMessage GetImageByFileNameWoody(string filename)
        {
            var filePath = "~/Resources/Images/Woody/" + filename + ".jpg";
            var result = GetImageFile(filePath);
            return result;
        }

        [Route("api/flower/image_name/{filename}")]
        public HttpResponseMessage GetImageByFileNameFlower(string filename)
        {
            var filePath = "~/Resources/Images/Flower/" + filename + ".jpg";
            var result = GetImageFile(filePath);
            return result;
        }

        //[AuthenticationTokenWetlands]
        [Route("api/wetland/image_icons/{filename}")]
        public HttpResponseMessage GetImageIcon(string filename)
        {
            var filePath = "~/Resources/Images/Wetlands/" + filename + "_icon.jpg";
            var result = GetImageFile(filePath);
            return result;
        }

        //[AuthenticationTokenWetlands]
        [Route("api/wetland/range_images/{filename}")]
        public HttpResponseMessage GetRangeImage(string filename)
        {
            var filePath = "~/Resources/Images/Wetlands/" + filename + ".png";
            var result = GetImageFile(filePath);
            return result;
        }

        [Route("api/woody/range_images/{filename}")]
        public HttpResponseMessage GetRangeImageWoody(string filename)
        {
            var filePath = "~/Resources/Images/Woody/" + filename + ".png";
            var result = GetImageFile(filePath);
            return result;
        }

        [Route("api/flower/range_images/{filename}")]
        public HttpResponseMessage GetRangeImageFlower(string filename)
        {
            var filePath = "~/Resources/Images/Flower/" + filename + ".png";
            var result = GetImageFile(filePath);
            return result;
        }

        // GET api/Account/UploadCsvFile
        [HttpGet]
        [Route("api/uploadDataWetlands")]
        public Task<IHttpActionResult> UploadDataWetlandsAsync()
        {
            string appRoot = HttpContext.Current.Server.MapPath("~");
            string filename = appRoot + "/DataFolder/wetland_data_6.js";
            char[] result;
            StringBuilder builder = new StringBuilder();
            IList<WetlandPlant> wetlandPlants = new List<WetlandPlant>();

            using (StreamReader reader = File.OpenText(filename))
            {
                result = new char[reader.BaseStream.Length];
                 reader.Read(result, 0, (int)reader.BaseStream.Length);
            }
            foreach (char c in result)
            {
                    builder.Append(c);
            }

            JsonTextReader readers = new JsonTextReader(new StringReader(builder.ToString()));
            JObject obj = null;

            try
            {
                //Loading json data with json text reader
                while (readers.Read())
                {
                    if (readers.TokenType == JsonToken.StartObject)
                    {

                         obj = JObject.Load(readers);

                    }
                }
                
                
                if (obj.HasValues) {

                    var listOfAllPlants = (JArray)obj["plants"];

                    //Each one of these is new Wetland plant
                    foreach (JObject value in listOfAllPlants)
                    {
                        WetlandPlant newPlant = new WetlandPlant();

                        foreach (var property in value.Properties())
                        {
                            Console.WriteLine("  {0}: {1}", property.Name, property.Value);
                            setPlantAttribute(newPlant, property);                        
                        }

                        wetlandPlants.Add(newPlant);
                        try
                        {
                            wetlandDb.Plants.Add(newPlant);
                            wetlandDb.SaveChanges();
                        }
                        catch (Exception e) {


                        }
             
                    }


             //       wetlandDb.Plants.AddRange(wetlandPlants);
               //     wetlandDb.SaveChanges();
                }
            }

            catch (Exception e) {
                Console.WriteLine("Error {0}", e);
            }

            return null;
        }

        // GET api/uploadDataWetlandsGlossary
        [HttpGet]
        [Route("api/uploadShapefile")]
        public async Task<IHttpActionResult> UploadShapefile()
        {
            
            return null;
            
        }

        /// <summary>
        /// Extracts the contents of a zip file and returns the
        /// name of the Shapefile, if one exists.
        /// </summary>
        /// <param name="zipFilePath"></param>
        /// <returns></returns>
        private String extractZipFile(String zipFilePath, String extractPath)
        {
            String shapefileName = String.Empty;

            using (ZipFile zipFile = new ZipFile(zipFilePath))
            {

                foreach (ZipEntry entry in zipFile)
                {
                    if (entry.FileName.ToLower().EndsWith(".shp") ||
                         entry.FileName.ToLower().EndsWith(".prj") ||
                         entry.FileName.ToLower().EndsWith(".dbf"))
                    {
                        entry.Extract(extractPath, ExtractExistingFileAction.OverwriteSilently);
                    }

                    if (entry.FileName.EndsWith(".shp"))
                    {
                        shapefileName = entry.FileName;
                    }
                }
            }

            return shapefileName;
        }

        private Int32 getFieldOrdinal(ShapefileDataReader shapefileReader, String field)
        {
            Int32 ordinal = -1;

            try
            {
                ordinal = shapefileReader.GetOrdinal(field.Trim());
            }
            catch
            {
                // throw an error only if the user specified a column
                if (!String.IsNullOrEmpty(field.Trim()))
                {

                }
            }

            return ordinal;
        }


        private String readProjection(String shapefile)
        {
            using (StreamReader reader = new StreamReader(shapefile.ToLower().Replace(".shp", ".prj")))
            {
                return reader.ReadToEnd();
            }
        }

        private void AddFromShapefile(ShapefileDataReader shapefileReader, String projectionWkt)
        {
            Int16 recordNumber = 1;
            IList<String> names = new List<String>();
            IList<GeoAPI.Geometries.IGeometry> allGeos = new List<GeoAPI.Geometries.IGeometry>();

            Int32 areaOrdinal = getFieldOrdinal(shapefileReader, "");
            Int32 nameOrdinal = getFieldOrdinal(shapefileReader, "");

           // while (shapefileReader.Read())
          //  {
                String mecator2 = "Mercator_2SP";
                String mecator1 = "Mercator_1SP";
                GeoAPI.Geometries.IGeometry geometry = shapefileReader.Geometry;

                if (!projectionWkt.Contains(mecator1) || projectionWkt.Contains(mecator2))
                {
                    ProjectionUtil.ConvertToGCSWGS84(geometry, projectionWkt);

                    ProjectionUtil.ConvertToGCSWGS84(geometry);

                    ProjectionUtil.ConvertGCSWGS84ToGoogleMercator(geometry);
                }

                allGeos.Add(geometry);

                if ((geometry is MultiPolygon))
                {

                    GeometryCollection newGeoC = (GeometryCollection)geometry;

                    foreach (GeoAPI.Geometries.IGeometry polyGeo in newGeoC.Geometries)
                    {

                        GeoAPI.Geometries.IGeometry clone = (GeoAPI.Geometries.IGeometry)geometry.Clone();
                        ProjectionUtil.ConvertToGCSWGS84(clone);
                        recordNumber++;

                    }
                }

                else
                {
                    GeoAPI.Geometries.IGeometry clone = (GeoAPI.Geometries.IGeometry)geometry.Clone();
                    ProjectionUtil.ConvertToGCSWGS84(clone);
                    ProjectionUtil.ConvertToGCSWGS84(clone);
                    ProjectionUtil.ConvertGCSWGS84ToNorthAmericanAlbersEqualArea(clone);

                    if ((clone is NetTopologySuite.Geometries.Point))
                    {
                       
                    }

                    if ((clone is MultiPoint))
                    {
                       
                    }

                    String name = (nameOrdinal > -1)
                                           ? shapefileReader.GetString(nameOrdinal)
                                           : String.Format("Project Activity Area {0}", recordNumber);
                    if (names.Contains(name))
                                           name = name + "_" + recordNumber;
                    
                    else
                                            names.Add(name);
                    
                }
         //   }


        }

        // GET api/Account/UploadCsvFile
        [HttpGet]
        [Route("api/uploadDataWetlandsGlossary")]
        public async Task<IHttpActionResult> UploadDataWetlandsGlossaryAsync()
        {

            string appRoot = HttpContext.Current.Server.MapPath("~");
            string filename = appRoot + "/DataFolder/glossary.js";
            char[] result;
            StringBuilder builder = new StringBuilder();

            IList<WetlandGlossary> wetlandGlossaries = new List<WetlandGlossary>();

            using (StreamReader reader = File.OpenText(filename))
            {
                result = new char[reader.BaseStream.Length];
                await reader.ReadAsync(result, 0, (int)reader.BaseStream.Length);
            }

            foreach (char c in result)
            {

                builder.Append(c);

            }


            JsonTextReader readers = new JsonTextReader(new StringReader(builder.ToString()));
            JObject obj = null;

            try
            {
                //Loading json data with json text reader
                while (readers.Read())
                {
                    if (readers.TokenType == JsonToken.StartObject)
                    {

                        obj = JObject.Load(readers);

                    }
                }


                if (obj.HasValues)
                {

                    var listOfAllPlants = (JArray)obj["glossary"];

                    //Each one of these is new Wetland plant
                    foreach (JObject value in listOfAllPlants)
                    {
                        WetlandGlossary newGlossary = new WetlandGlossary();

                        foreach (var property in value.Properties())
                        {                            
                            setGlossaryAttribute(newGlossary, property);
                        }

                        wetlandGlossaries.Add(newGlossary);
                        wetlandDb.WetlandGlossary.Add(newGlossary);
                        wetlandDb.SaveChanges();
                    }


                    //  wetlandDb.Plants.AddRange(wetlandPlants);
                    //  wetlandDb.Plants.SaveChanges();
                }
            }

            catch (Exception e)
            {
                Console.WriteLine("Error {0}", e);
            }

            return null;
        }

        private HttpResponseMessage GetImageFile(string filePath)
        {
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            String fileLocation = HostingEnvironment.MapPath(filePath);
            using (FileStream fileStream = new FileStream(fileLocation, FileMode.Open))
            {
                Image image = Image.FromStream(fileStream);
                MemoryStream memoryStream = new MemoryStream();
                image.Save(memoryStream, ImageFormat.Jpeg);
                result.Content = new ByteArrayContent(memoryStream.ToArray());
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            }

            return result;
        }

        private void setGlossaryAttribute(WetlandGlossary newGlossary, JProperty property)
        {
            switch (property.Name.ToLower())
            {
                case "id":
                    newGlossary.id = int.Parse(property.Value.ToString());
                    break;
                case "name":
                    newGlossary.name = property.Value.ToString();
                    break;
                case "definition":
                    newGlossary.definition = property.Value.ToString();
                    break;
            }

        }

        private void setPlantAttribute(WetlandPlant newPlant, JProperty property)
        {
            switch (property.Name.ToLower())
            {
                case "id":
                    newPlant.id = int.Parse(property.Value.ToString());
                    break;
                case "sections":
                    newPlant.sections = property.Value.ToString();
                    break;
                case "scinameauthor":
                    newPlant.scinameauthor = property.Value.ToString();
                    break;
                case "scinamenoauthor":
                    newPlant.scinamenoauthor = property.Value.ToString();
                    break;
                case "family":
                    newPlant.family = property.Value.ToString();
                    break;

                case "commonname":
                    newPlant.commonname = property.Value.ToString();
                    break;

                case "plantscode":
                    newPlant.plantscode = property.Value.ToString();
                    break;

                case "mapimg":
                    newPlant.mapimg = property.Value.ToString();
                    break;

                case "itiscode":
                    newPlant.itiscode = property.Value.ToString();
                    break;
                case "awwetcode":
                    newPlant.awwetcode = property.Value.ToString();
                    break;
                case "gpwetcode":
                    newPlant.gpwetcode = property.Value.ToString();
                    break;

                case "wmvcwetcode":
                    newPlant.wmvcwetcode = property.Value.ToString();
                    break;

                case "cvalue":
                    newPlant.cvalue = property.Value.ToString();
                    break;

                case "grank":
                    newPlant.grank = property.Value.ToString();
                    break;

                case "federalstatus":
                    newPlant.federalstatus = property.Value.ToString();
                    break;

                case "cosrank":
                    newPlant.cosrank = property.Value.ToString();
                    break;

                case "mtsrank":
                    newPlant.mtsrank = property.Value.ToString();
                    break;

                case "ndsrank":
                    newPlant.ndsrank = property.Value.ToString();
                    break;

                case "sdsrank":
                    newPlant.sdsrank = property.Value.ToString();
                    break;

                case "utsrank":
                    newPlant.utsrank = property.Value.ToString();
                    break;

                case "wysrank":
                    newPlant.wysrank = property.Value.ToString();
                    break;

                case "nativity":
                    newPlant.nativity = property.Value.ToString();
                    break;
                case "noxiousweed":
                    newPlant.noxiousweed = property.Value.ToString();
                    break;
                case "elevminfeet":
                    newPlant.elevminfeet = int.Parse(property.Value.ToString());
                    break;

                case "elevminm":
                    newPlant.elevminm = int.Parse(property.Value.ToString());
                    break;

                case "elevmaxfeet":
                    newPlant.elevmaxfeet = int.Parse(property.Value.ToString());
                    break;

                case "elevmaxm":
                    newPlant.elevmaxm = int.Parse(property.Value.ToString());
                    break;

                case "keychar1":
                    newPlant.keychar1 = property.Value.ToString();
                    break;

                case "keychar2":
                    newPlant.keychar2 = property.Value.ToString();
                    break;
                case "keychar3":
                    newPlant.keychar3 = property.Value.ToString();
                    break;
                case "keychar4":
                    newPlant.keychar4 = property.Value.ToString();
                    break;
                case "keychar5":
                    newPlant.keychar5 = property.Value.ToString();
                    break;

                case "keychar6":
                    newPlant.keychar6 = property.Value.ToString();
                    break;

                case "similarsp":
                    newPlant.similarsp = property.Value.ToString();
                    break;

                case "habitat":
                    newPlant.habitat = property.Value.ToString();
                    break;

                case "comments":
                    newPlant.comments = property.Value.ToString();
                    break;

                case "animaluse":
                    newPlant.animaluse = property.Value.ToString();
                    break;

                case "ecologicalsystems":
                    newPlant.ecologicalsystems = property.Value.ToString();
                    break;

                case "synonyms":
                    newPlant.synonyms = property.Value.ToString();
                    break;

                case "topimgtopimg":
                    newPlant.topimgtopimg = property.Value.ToString();
                    break;

                case "duration":
                    newPlant.duration = property.Value.ToString();
                    break;

                case "images":
                    populateWetlandImages(newPlant, property.Value);
                    break;

                case "similarspecies":
                    populateSimilarSpecies(newPlant, property.Value);
                    break;

                case "counties":
                    populateCounties(newPlant, property.Value);
                    break;
                case "references":
                    populateReferences(newPlant, property.Value);
                    break;

                case "fruits":
                    populateFruits(newPlant, property.Value);
                    break;
                case "division":
                    populateDivision(newPlant, property.Value);
                    break;
                case "shape":
                    populateShape(newPlant, property.Value);
                    break;
                case "arrangement":
                    populateArrangement(newPlant, property.Value);
                    break;
                case "size":
                    populateSize(newPlant, property.Value);
                    break;
                case "regions":
                    populateRegions(newPlant, property.Value);
                    break;
                default:
                    break;
            }
        }

        private void populateRegions(WetlandPlant newPlant, JToken value)
        {
            ICollection<RegionWetland> objList = new List<RegionWetland>();

            var listOfValues = (JArray)value;


            //Each one of these is new Wetland plant
            foreach (JObject valueItem in listOfValues)
            {
                RegionWetland newObj = new RegionWetland();

                foreach (var property in valueItem.Properties())
                    setRegionWetlandAttribute(newObj, property);

                    objList.Add(newObj);
            }


            newPlant.RegionWetland = objList;
        }

        private void setRegionWetlandAttribute(RegionWetland newObj, JProperty property)
        {
            switch (property.Name.ToLower())
            {
                case "valueid":
                    newObj.valueid = int.Parse(property.Value.ToString());
                    break;

                default:
                    break;

            }
        }

        private void populateSize(WetlandPlant newPlant, JToken value)
        {
            ICollection<SizeWetland> objList = new List<SizeWetland>();

            var listOfValues = (JArray)value;


            //Each one of these is new Wetland plant
            foreach (JObject valueItem in listOfValues)
            {
                SizeWetland newObj = new SizeWetland();

                foreach (var property in valueItem.Properties())
                    setSizeWetlandAtrribute(newObj, property);

                    objList.Add(newObj);

            }


            newPlant.SizeWetland = objList;
        }

        private void setSizeWetlandAtrribute(SizeWetland newObj, JProperty property)
        {
            switch (property.Name.ToLower())
            {
                case "valueid":
                    newObj.valueid = int.Parse(property.Value.ToString());
                    break;

                default:
                    break;

            }
        }

        private void populateArrangement(WetlandPlant newPlant, JToken value)
        {
            ICollection<ArrangementWetland> objList = new List<ArrangementWetland>();

            var listOfValues = (JArray)value;


            //Each one of these is new Wetland plant
            foreach (JObject valueItem in listOfValues)
            {
                ArrangementWetland newObj = new ArrangementWetland();

                foreach (var property in valueItem.Properties())
                    setArrangementWetlandAttribute(newObj, property);

                    objList.Add(newObj);
            }


            newPlant.ArrangementWetland = objList;
        }

        private void setArrangementWetlandAttribute(ArrangementWetland newObj, JProperty property)
        {
            switch (property.Name.ToLower())
            {
                case "valueid":
                    newObj.valueid = int.Parse(property.Value.ToString());
                    break;

                default:
                    break;

            }
        }

        private void populateShape(WetlandPlant newPlant, JToken value)
        {
            ICollection<ShapeWetland> objList = new List<ShapeWetland>();

            var listOfValues = (JArray)value;


            //Each one of these is new Wetland plant
            foreach (JObject valueItem in listOfValues)
            {
                ShapeWetland newObj = new ShapeWetland();

                foreach (var property in valueItem.Properties())
                    setShapeWetlandAttribute(newObj, property);

                    objList.Add(newObj);
            }


            newPlant.ShapeWetland = objList;
        }

        private void setShapeWetlandAttribute(ShapeWetland newObj, JProperty property)
        {
            switch (property.Name.ToLower())
            {
                case "valueid":
                    newObj.valueid = int.Parse(property.Value.ToString());
                    break;

                default:
                    break;

            }
        }

        private void populateDivision(WetlandPlant newPlant, JToken value)
        {
            ICollection<DivisionWetland> objList = new List<DivisionWetland>();

            var listOfValues = (JArray)value;


            //Each one of these is new Wetland plant
            foreach (JObject valueItem in listOfValues)
            {
                DivisionWetland newObj = new DivisionWetland();

                foreach (var property in valueItem.Properties())
                    setDivisionWetlandAttribute(newObj, property);

                    objList.Add(newObj);
            }


            newPlant.DivisionWetland = objList;
        }

        private void setDivisionWetlandAttribute(DivisionWetland newObj, JProperty property)
        {
            switch (property.Name.ToLower())
            {
                case "valueid":
                    newObj.valueid = int.Parse(property.Value.ToString());
                    break;

                default:
                    break;

            }
        }

        private void populateFruits(WetlandPlant newPlant, JToken value)
        {
            ICollection<FruitWetland> objList = new List<FruitWetland>();

            var listOfValues = (JArray)value;


            //Each one of these is new Wetland plant
            foreach (JObject valueItem in listOfValues)
            {
                FruitWetland newObj = new FruitWetland();

                foreach (var property in valueItem.Properties())
                 setFruitWetlandAttribute(newObj, property);

                    objList.Add(newObj);
             }


            newPlant.FruitWetland = objList;
        }

        private void setFruitWetlandAttribute(FruitWetland newObj, JProperty property)
        {
            switch (property.Name.ToLower())
            {
                case "valueid":
                    newObj.valueid = int.Parse(property.Value.ToString());
                    break;

                default:
                    break;

            }
        }

        private void populateReferences(WetlandPlant newPlant, JToken value)
        {
            ICollection<ReferenceWetland> objList = new List<ReferenceWetland>();

            var listOfValues = (JArray)value;

            foreach (JObject valueItem in listOfValues)
            {
                ReferenceWetland newObj = new ReferenceWetland();

                foreach (var property in valueItem.Properties())
                 setReferenceWetlandAttribute(newObj, property);

                    objList.Add(newObj);
            }

            newPlant.References = objList;
        }

        private void setReferenceWetlandAttribute(ReferenceWetland newObj, JProperty property)
        {
            switch (property.Name.ToLower())
            {
                case "id":
                    newObj.id = int.Parse(property.Value.ToString());
                    break;

                case "reference":
                    newObj.reference = property.Value.ToString();
                    break;

                case "fullcitation":
                    newObj.fullcitation = property.Value.ToString();
                    break;

                default:
                    break;

            }
        }

        private void populateCounties(WetlandPlant newPlant, JToken value)
        {
            ICollection<CountyPlantWetland> objList = new List<CountyPlantWetland>();

            var listOfValues = (JArray)value;

            //Each one of these is new Wetland plant
            foreach (JObject valueItem in listOfValues)
            {
                CountyPlantWetland newObj = new CountyPlantWetland();

                foreach (var property in valueItem.Properties())
                    setCountyPlantWetlandAttribute(newObj, property);

                    objList.Add(newObj);
            }


            newPlant.CountyPlantWetland = objList;
        }

        private void setCountyPlantWetlandAttribute(CountyPlantWetland newObj, JProperty property)
        {
            switch (property.Name.ToLower())
            {
                case "id":
                    newObj.county_id = int.Parse(property.Value.ToString());
                    break;

                case "name":
                    newObj.name = property.Value.ToString();
                    break;

                default:
                    break;

            }
        }

        private void populateSimilarSpecies(WetlandPlant newPlant, JToken value)
        {
            ICollection<SimilarSpeciesWetland> objList = new List<SimilarSpeciesWetland>();

            var listOfValues = (JArray)value;

            //Each one of these is new Wetland plant
            foreach (JObject valueItem in listOfValues)
            {
                SimilarSpeciesWetland newObj = new SimilarSpeciesWetland();

                foreach (var property in valueItem.Properties())                               
                    setSimilarSpeciesWetlandAttribute(newObj, property);
                
                objList.Add(newObj);
            }

            newPlant.SimilarSpecies = objList;
        }

        private void setSimilarSpeciesWetlandAttribute(SimilarSpeciesWetland newObj, JProperty property)
        {
            switch (property.Name.ToLower())
            {
                case "id":
                    newObj.id = int.Parse(property.Value.ToString());
                    break;

                case "similarspicon":
                    newObj.similarspicon = property.Value.ToString();
                    break;

                case "similarspscinameauthor":
                    newObj.similarspscinameauthor = property.Value.ToString();
                    break;

                case "reason":
                    newObj.reason = property.Value.ToString();
                    break;

                default:
                    break;

            }
        }

        private void populateWetlandImages(WetlandPlant newPlant, JToken value)
        {
            ICollection<ImagesWetland> images = new List<ImagesWetland>();

            var listOfImages = (JArray)value;


            //Each one of these is new Wetland plant
            foreach (JObject imageValue in listOfImages)
            {
                ImagesWetland newImage = new ImagesWetland();

                foreach (var property in imageValue.Properties())
                {
     
                    setWetlantImageAttribute(newImage, property);                
                }

                images.Add(newImage);

            }


            newPlant.Images = images;
        }

        private void setWetlantImageAttribute(ImagesWetland newImage, JProperty property)
        {
            switch (property.Name.ToLower())
            {
                case "id":
                    newImage.id = int.Parse(property.Value.ToString());
                    break;

                case "filename":
                    newImage.filename = property.Value.ToString();
                    break;

                case "credit":
                    newImage.credit = property.Value.ToString();
                    break;

                default:
                    break;

            }

         }
        private string[] GetWoodyAttributeOrder() {
            string[] woodyAttributeOrder =
                {
                "plant_imported_id",
                "family",
                "commonName",
                "scientificNameWeber",
                "commonNameSecondary",
                "derivation",
                "scientificNameOther",
                "scientificNameMeaning",
                "keyCharacteristics",
                "flowerColor",
                "leafType",
                "seasonOfBloom",
                "monoecious",
                "lifeZone",
                "edibility",
                "toxicity",
                "landscapingUse",
                "matureHeight",
                "matureSpread",
                "siteRequirements",
                "soilRequirements",
                "moistureRequirements",
                "ecologicalRelationships",
                "frequency",
                "endemicLocation",
                "alien",
                "comments",
                "habitat",
                "cultivar",
                "fiber",
                "otherUses",
                "availability",
                "fruitColor",
                "fruitType",
                "familyCharacteristics",
                "flowerShape",
                "flowerSymmetry",
                "flowerCluster",
                "flowerSize",
                "petalNumber",
                "leafShape",
                "flowerStructure",
                "weedManagement",
                "leafArrangement",
                "twigTexture",
                "barkTexture",
                "barkDescription",
                "flowerDescription",
                "fruitDescription",
                "imageNames"
                };
            return woodyAttributeOrder;
        }
        private string[] GetWoodyAttributeOrder(WoodyPlant woodyPlant)
        {
            string[] woodyAttributeOrder =
                {
                "plant_imported_id",
                "family",
                "commonName",
                "scientificNameWeber",
                "commonNameSecondary",
                "derivation",
                "scientificNameOther",
                "scientificNameMeaning",
                "keyCharacteristics",
                "flowerColor",
                "leafType",
                "seasonOfBloom",
                "monoecious",
                "lifeZone",
                "edibility",
                "toxicity",
                "landscapingUse",
                "matureHeight",
                "matureSpread",
                "siteRequirements",
                "soilRequirements",
                "moistureRequirements",
                "ecologicalRelationships",
                "frequency",
                "endemicLocation",
                "alien",
                "comments",
                "habitat",
                "cultivar",
                "fiber",
                "otherUses",
                "availability",
                "fruitColor",
                "fruitType",
                "familyCharacteristics",
                "flowerShape",
                "flowerSymmetry",
                "flowerCluster",
                "flowerSize",
                "petalNumber",
                "leafShape",
                "flowerStructure",
                "weedManagement",
                "leafArrangement",
                "twigTexture",
                "barkTexture",
                "barkDescription",
                "flowerDescription",
                "fruitDescription",
                "imageNames"
                };
            return woodyAttributeOrder;
        }

        // GET api/uploadData
        [HttpGet]
        [Route("api/uploadData")]
        public async Task<IHttpActionResult> UploadData()
        {
            string appRoot = HttpContext.Current.Server.MapPath("~");
            string sSheetName = "Sheet1";
            string sConnection = null;
            string[] plantOrder = GetWoodyAttributeOrder();
            DataTable dtTablesList = default(DataTable);
            OleDbCommand oleExcelCommand = default(OleDbCommand);
            OleDbDataReader oleExcelReader = default(OleDbDataReader);
            OleDbConnection oleExcelConnection = default(OleDbConnection);
            IList<KeyValuePair<String, Int32>> idNamePair = new List<KeyValuePair<String, Int32>>();//Need to have id as number because names are way too long
            //Int32 currentId = 0;
            Int32 uniqueIdNum = 1;

            try
            {
                sConnection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + appRoot + "/DataFolder/WoodyPlant/WoodyPlant_DataBase.xlsx" + ";Extended Properties=\"Excel 12.0;HDR=No;IMEX=1\"";
                //sConnection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + "C:\\Temp\\UpdatedWoodyPlantApp.xlsx" + ";Extended Properties=\"Excel 12.0;HDR=No;IMEX=1\"";

                oleExcelConnection = new OleDbConnection(sConnection);
                oleExcelConnection.Open();
                dtTablesList = oleExcelConnection.GetSchema("Tables");

                if (dtTablesList.Rows.Count > 0)
                {
                    sSheetName = dtTablesList.Rows[0]["TABLE_NAME"].ToString();
                }

                dtTablesList.Clear();
                dtTablesList.Dispose();


                if (!string.IsNullOrEmpty(sSheetName))
                {

                    oleExcelCommand = oleExcelConnection.CreateCommand();
                    oleExcelCommand.CommandText = "Select * From [" + sSheetName + "]";
                    oleExcelCommand.CommandType = CommandType.Text;
                    oleExcelReader = oleExcelCommand.ExecuteReader();

                    var firstRecord = true;
                    while (oleExcelReader.Read())
                    {
                        if (!firstRecord)
                        {
                            /*
                            WoodyPlant newPlant = new WoodyPlant
                            {
                                plant_imported_id = Convert.ToInt32(oleExcelReader.GetValue(0)),//
                                family = oleExcelReader.GetValue(1).ToString(),//
                                commonName = oleExcelReader.GetValue(2).ToString(),//
                                scientificNameWeber = oleExcelReader.GetValue(3).ToString(),//
                                commonNameSecondary = oleExcelReader.GetValue(4).ToString(),//
                                derivation = oleExcelReader.GetValue(5).ToString(),//
                                scientificNameOther = oleExcelReader.GetValue(6).ToString(),//
                                scientificNameMeaning = oleExcelReader.GetValue(7).ToString(),//
                                keyCharacteristics = oleExcelReader.GetValue(8).ToString(),//
                                flowerColor = oleExcelReader.GetValue(9).ToString(),//
                                leafType = oleExcelReader.GetValue(10).ToString(),//
                                seasonOfBloom = oleExcelReader.GetValue(11).ToString(),//
                                monoecious = oleExcelReader.GetValue(12).ToString(),//
                                lifeZone = oleExcelReader.GetValue(13).ToString(),//
                                edibility = oleExcelReader.GetValue(14).ToString(),//
                                toxicity = oleExcelReader.GetValue(15).ToString(),//
                                landscapingUse = oleExcelReader.GetValue(16).ToString(),//
                                matureHeight = oleExcelReader.GetValue(17).ToString(),//
                                matureSpread = oleExcelReader.GetValue(18).ToString(),//
                                siteRequirements = oleExcelReader.GetValue(19).ToString(),//
                                soilRequirements = oleExcelReader.GetValue(20).ToString(),//
                                moistureRequirements = oleExcelReader.GetValue(21).ToString(),//
                                ecologicalRelationships = oleExcelReader.GetValue(22).ToString(),//
                                frequency = oleExcelReader.GetValue(23).ToString(),//
                                endemicLocation = oleExcelReader.GetValue(24).ToString(),//
                                alien = oleExcelReader.GetValue(25).ToString(),//
                                comments = oleExcelReader.GetValue(26).ToString(),//
                                habitat = oleExcelReader.GetValue(27).ToString(),//
                                cultivar = oleExcelReader.GetValue(28).ToString(),//
                                fiber = oleExcelReader.GetValue(29).ToString(),//
                                otherUses = oleExcelReader.GetValue(30).ToString(),//
                                availability = oleExcelReader.GetValue(31).ToString(),//
                                fruitColor = oleExcelReader.GetValue(32).ToString(),//
                                fruitType = oleExcelReader.GetValue(33).ToString(),//
                                familyCharacteristics = oleExcelReader.GetValue(34).ToString(),//
                                flowerShape = oleExcelReader.GetValue(35).ToString(),//
                                flowerSymmetry = oleExcelReader.GetValue(36).ToString(),//
                                flowerCluster = oleExcelReader.GetValue(37).ToString(),//
                                flowerSize = oleExcelReader.GetValue(38).ToString(),//
                                petalNumber = oleExcelReader.GetValue(39).ToString(),//
                                leafShape = oleExcelReader.GetValue(40).ToString(),//
                                flowerStructure = oleExcelReader.GetValue(41).ToString(),//
                                weedManagement = oleExcelReader.GetValue(42).ToString(),//
                                leafArrangement = oleExcelReader.GetValue(43).ToString(),//
                                twigTexture = oleExcelReader.GetValue(44).ToString(),//
                                barkTexture = oleExcelReader.GetValue(45).ToString(),//
                                barkDescription = oleExcelReader.GetValue(46).ToString(),//
                                flowerDescription = oleExcelReader.GetValue(47).ToString(),//
                                fruitDescription = oleExcelReader.GetValue(48).ToString(),//
                                imageNames = oleExcelReader.GetValue(49).ToString(),//
                            };
                            */
                            WoodyPlant newPlant = new WoodyPlant();
                            string plantAttrib = plantOrder[0];
                            string woodyAtrribVal;
                            newPlant.GetType().GetProperty(plantAttrib).SetValue(newPlant, Convert.ToInt32(oleExcelReader.GetValue(0)), null);
                            for (int i = 1; i < plantOrder.Length; i++)
                            {
                                plantAttrib = plantOrder[i];
                                woodyAtrribVal = oleExcelReader.GetValue(i).ToString();
                                newPlant.GetType().GetProperty(plantAttrib).SetValue(newPlant, woodyAtrribVal, null);
                            }


                            woodyDb.Plants.Add(newPlant);
                            woodyDb.SaveChanges();
                            Debug.WriteLine("{0}: {1}", newPlant.plant_id, newPlant.scientificNameWeber);

                            uniqueIdNum++;
                        }

                        firstRecord = false;
                    }

                    oleExcelReader.Close();
                }
                oleExcelConnection.Close();
            }
            catch (Exception e)
            {

                oleExcelReader.Close();
                oleExcelConnection.Close();
                Debug.WriteLine("{0}: {1}", e.Message);
            }


            JavaScriptSerializer systemSerializer = new JavaScriptSerializer();
            systemSerializer.MaxJsonLength = Int32.MaxValue;

            return null;
        }


        [HttpGet]
        [Route("api/uploadDataFlower")]
        public async Task<IHttpActionResult> UploadDataFlower()
        {
            string appRoot = HttpContext.Current.Server.MapPath("~");
            string sSheetName = "Sheet1";
            string sConnection = null;
            DataTable dtTablesList = default(DataTable);
            OleDbCommand oleExcelCommand = default(OleDbCommand);
            OleDbDataReader oleExcelReader = default(OleDbDataReader);
            OleDbConnection oleExcelConnection = default(OleDbConnection);
            IList<KeyValuePair<String, Int32>> idNamePair = new List<KeyValuePair<String, Int32>>();//Need to have id as number because names are way too long
            //Int32 currentId = 0;
            Int32 uniqueIdNum = 1;

            try
            {
                sConnection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + appRoot + "/DataFolder/flowers_db.xlsx" + ";Extended Properties=\"Excel 12.0;HDR=No;IMEX=1\"";

                oleExcelConnection = new OleDbConnection(sConnection);
                oleExcelConnection.Open();


                dtTablesList = oleExcelConnection.GetSchema("Tables");

                if (dtTablesList.Rows.Count > 0)
                {
                    sSheetName = dtTablesList.Rows[0]["TABLE_NAME"].ToString();
                }

                dtTablesList.Clear();
                dtTablesList.Dispose();


                if (!string.IsNullOrEmpty(sSheetName))
                {

                    oleExcelCommand = oleExcelConnection.CreateCommand();
                    oleExcelCommand.CommandText = "Select * From [" + sSheetName + "]";
                    oleExcelCommand.CommandType = CommandType.Text;
                    oleExcelReader = oleExcelCommand.ExecuteReader();

                    var firstRecord = true;
                    while (oleExcelReader.Read())
                    {
                        if (!firstRecord)
                        {
                            Flower newPlant = new Flower
                            {
                                id = Convert.ToInt32(oleExcelReader.GetValue(0)),//
                                familyScientific = oleExcelReader.GetValue(1).ToString(),//
                                familyScientific2 = oleExcelReader.GetValue(2).ToString(),//
                                familyCommon = oleExcelReader.GetValue(3).ToString(),//
                                genusSpeciesWeber = oleExcelReader.GetValue(4).ToString(),//
                                genusSpeciesAckerfield = oleExcelReader.GetValue(5).ToString(),//
                                genusWeber = oleExcelReader.GetValue(6).ToString(),//
                                speciesWeber = oleExcelReader.GetValue(7).ToString(),//
                                genusAckerfield = oleExcelReader.GetValue(8).ToString(),//
                                commonName1 = oleExcelReader.GetValue(9).ToString(),//
                                commonName2 = oleExcelReader.GetValue(10).ToString(),//
                                color = oleExcelReader.GetValue(11).ToString(),//
                                month = oleExcelReader.GetValue(12).ToString(),//
                                zone = oleExcelReader.GetValue(13).ToString(),//
                                origin = oleExcelReader.GetValue(14).ToString(),//
                                noxious = oleExcelReader.GetValue(15).ToString(),//
                                description = oleExcelReader.GetValue(16).ToString(),//
                                similar = oleExcelReader.GetValue(17).ToString(),//
                                photos = oleExcelReader.GetValue(18).ToString(),//
                                thumbnail = oleExcelReader.GetValue(19).ToString(),//
                            };

                            try
                            {
                                flowerDb.Plants.Add(newPlant);
                            }
                            catch (Exception e)
                            {


                                Debug.WriteLine("{0}: {1}", e.Message);
                            }
                            flowerDb.SaveChanges();
                            Debug.WriteLine("{0}: {1}", newPlant.plant_id, newPlant.familyScientific);

                            uniqueIdNum++;
                        }

                        firstRecord = false;
                    }

                    oleExcelReader.Close();
                }
                oleExcelConnection.Close();
            }
            catch (Exception e)
            {


                Debug.WriteLine("{0}: {1}", e.Message);
            }


            JavaScriptSerializer systemSerializer = new JavaScriptSerializer();
            systemSerializer.MaxJsonLength = Int32.MaxValue;

            return null;
        }


    }
}