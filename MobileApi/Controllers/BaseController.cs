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
using System.Data;
using System.Data.OleDb;
using System.Web.Script.Serialization;
using System.Web;
using System.Data.Entity;
using Ionic.Zip;

namespace MobileApi.Controllers
{ 

    public class BaseController : ApiController
    {
        public WoodyPlantsMobileApiContext woodyDb = new WoodyPlantsMobileApiContext();
        public WetlandPlantsMobileApiContext wetlandDb = new WetlandPlantsMobileApiContext();
        public string currentRepository;

        // This action accepts no params (other than the repository) and also supports OData querying params
        // e.g. api/pumas?$top=10 (gets top ten records)
        // e.g. api/pumas?$filter=Name eq 'Cougar' (returns pumas with name equaling 'Cougar')
        [EnableQuery]
        [Route("api/woody")] 
        public IQueryable<WoodyPlant> GetWoodyPlants()
        {
            DbSet<WoodyPlant> allPlants = woodyDb.Plants;
            return allPlants.AsQueryable();
        }

        [EnableQuery]
        [Route("api/wetland")]
        public IQueryable<WetlandPlant> GetWetlandPlants()
        {
            wetlandDb.Configuration.ProxyCreationEnabled = false;
            //DbSet<WetlandPlant> allPlants = wetlandDb.Plants;
            List<WetlandPlant> allPlants = wetlandDb.Plants.Include(x => x.Images).ToList();
                //.Include(x=>x.SimilarSpeciesWetland).Include(x=>x.CountyPlantWetland)
                //.Include(x=>x.FruitWetland).Include(x=>x.DivisionWetland).Include(x=>x.ShapeWetland).Include(x=>x.ArrangementWetland)
                //.Include(x=>x.SizeWetland).Include(x=>x.RegionWetland).ToList();

            return allPlants.AsQueryable();
        }

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

        /*   [Route("api/{repository}/{resourceId}")]
           public async Task<IHttpActionResult> Get(int resourceId)
           {
               Puma puma = await db.Pumas.FindAsync(resourceId);
               if (puma == null)
               {
                   return NotFound();
               }

               return Ok(puma);
           }
           */

            // Get zipped images
           [Route("api/wetland/images_zipped")]
           public HttpResponseMessage GetImagesZip()
           {
                //var result = new HttpResponseMessage(HttpStatusCode.OK);
                var directory = "~/Resources/Images/wetlands/images.zip";
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
                    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = "images.zip" };

                    return response;
                }
            
           }
           
           [Route("api/wetland/images/{imageId}")]
           public IHttpActionResult GetImage(int imageId)
           {

               ImagesWetland wetlandImage = wetlandDb.ImagesWetland.Where(b => b.imageid == imageId).FirstOrDefault();
               if (wetlandImage == null)
               {
                    return NotFound();
               }
               else
               {
                    var result = new HttpResponseMessage(HttpStatusCode.OK);
                    var directory = "~/Resources/Images/wetlands/" + wetlandImage.filename;
                    String filePath = HostingEnvironment.MapPath(directory);
                    FileStream fileStream = new FileStream(filePath, FileMode.Open);
                    Image image = Image.FromStream(fileStream);
                    MemoryStream memoryStream = new MemoryStream();
                    image.Save(memoryStream, ImageFormat.Jpeg);
                    result.Content = new ByteArrayContent(memoryStream.ToArray());
                    result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");

                    return Ok(result);
                }
               
           }


        // GET api/Account/UploadCsvFile
        [HttpGet]
        [Route("api/uploadDataWetlands")]
        public IHttpActionResult UploadDataWetlands()
        {
            string appRoot = HttpContext.Current.Server.MapPath("~");
            string sSheetName = "Sheet1";
            string sConnection = null;
            DataTable dtTablesList = default(DataTable);
            OleDbCommand oleExcelCommand = default(OleDbCommand);
            OleDbDataReader oleExcelReader = default(OleDbDataReader);
            OleDbConnection oleExcelConnection = default(OleDbConnection);
            IList<object> plants = new List<object>();
            IList<KeyValuePair<String, Int32>> idNamePair = new List<KeyValuePair<String, Int32>>();//Need to have id as number because names are way too long
            Int32 currentId = 0;
            Int32 uniqueIdNum = 1;

            sConnection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + appRoot + "/plant_db.xlsx" + ";Extended Properties=\"Excel 12.0;HDR=No;IMEX=1\"";

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
                        plants.Add(new
                        {
                            plantId = oleExcelReader.GetValue(0),
                            commonFN = oleExcelReader.GetValue(1),
                            scientinficFN = oleExcelReader.GetValue(2),
                            family = oleExcelReader.GetValue(3),
                            family2 = oleExcelReader.GetValue(4),
                            commonName = oleExcelReader.GetValue(5),
                            scientificNameWeber = oleExcelReader.GetValue(6),
                            subspecies = oleExcelReader.GetValue(7),
                            iety = oleExcelReader.GetValue(8),
                            forma = oleExcelReader.GetValue(9),
                            familyAckerfield = oleExcelReader.GetValue(10),
                            scientificNameAckerField = oleExcelReader.GetValue(11),
                            ackerFieldPage = oleExcelReader.GetValue(12),
                            weber4thWestern = oleExcelReader.GetValue(13),
                            weber4thPage = oleExcelReader.GetValue(14),
                            commonNameSecondary = oleExcelReader.GetValue(15),
                            derivation = oleExcelReader.GetValue(16),
                            scientificNameOther = oleExcelReader.GetValue(17),
                            scientificNameMeaning = oleExcelReader.GetValue(18),
                            keyCharacteristics = oleExcelReader.GetValue(19),
                            flowerType = oleExcelReader.GetValue(20),
                            flowerColor = oleExcelReader.GetValue(21),
                            leafType = oleExcelReader.GetValue(22),
                            seasonOfBloom = oleExcelReader.GetValue(23),
                            growthForm = oleExcelReader.GetValue(24),
                            monocot = oleExcelReader.GetValue(25),
                            monoecious = oleExcelReader.GetValue(26),
                            lifeZone = oleExcelReader.GetValue(27),
                            edibility = oleExcelReader.GetValue(28),
                            toxicity = oleExcelReader.GetValue(29),
                            landscapingUse = oleExcelReader.GetValue(30),
                            matureHeight = oleExcelReader.GetValue(31),
                            matureSpread = oleExcelReader.GetValue(32),
                            siteRequirements = oleExcelReader.GetValue(33),
                            soilRequirements = oleExcelReader.GetValue(34),
                            moistureRequirements = oleExcelReader.GetValue(35),
                            ecologicalRelationships = oleExcelReader.GetValue(36),
                            frequency = oleExcelReader.GetValue(37),
                            endemicLocation = oleExcelReader.GetValue(38),
                            alien = oleExcelReader.GetValue(39),
                            comments = oleExcelReader.GetValue(40),
                            habitat = oleExcelReader.GetValue(41),
                            culti = oleExcelReader.GetValue(42),
                            fiber = oleExcelReader.GetValue(43),
                            otherUses = oleExcelReader.GetValue(44),
                            fruitColor = oleExcelReader.GetValue(45),
                            fruitType = oleExcelReader.GetValue(46),
                            print = oleExcelReader.GetValue(47),
                            familyCharacteristics = oleExcelReader.GetValue(48),
                            flowerShape = oleExcelReader.GetValue(49),
                            flowerSymmetry = oleExcelReader.GetValue(50),
                            flowerCluster = oleExcelReader.GetValue(51),
                            flowerSize = oleExcelReader.GetValue(52),
                            petalNumber = oleExcelReader.GetValue(53),
                            leafShape = oleExcelReader.GetValue(54),
                            flowerStructure = oleExcelReader.GetValue(55),
                            weedManagement = oleExcelReader.GetValue(56),
                            legalStatus = oleExcelReader.GetValue(57),
                            livestock = oleExcelReader.GetValue(58),
                            falcon12 = oleExcelReader.GetValue(59),
                            tellerCounty = oleExcelReader.GetValue(60),
                            greenMt12 = oleExcelReader.GetValue(61),
                            reynolds12 = oleExcelReader.GetValue(62),
                            bear12 = oleExcelReader.GetValue(63),
                            goldenGate = oleExcelReader.GetValue(64),
                            custerCounty = oleExcelReader.GetValue(65),
                            southValley = oleExcelReader.GetValue(66),
                            deerCreek = oleExcelReader.GetValue(67),
                            plainCc = oleExcelReader.GetValue(68),
                            maloitPark = oleExcelReader.GetValue(69),
                            vailNc = oleExcelReader.GetValue(70),
                            lovelandPass = oleExcelReader.GetValue(71),
                            noNameCreek = oleExcelReader.GetValue(72),
                            guanellaPass = oleExcelReader.GetValue(73),
                            southPlattePark = oleExcelReader.GetValue(74),
                            roxborough = oleExcelReader.GetValue(75),
                            castlewood = oleExcelReader.GetValue(76),
                            highPlains = oleExcelReader.GetValue(77),
                            dbg = oleExcelReader.GetValue(78),
                            grassesAtGreenMtn = oleExcelReader.GetValue(79),
                            scientificMeaningAckerField = oleExcelReader.GetValue(80),
                            eastPortal = oleExcelReader.GetValue(81),
                            mesaCounty = oleExcelReader.GetValue(82),
                            lmncMay = oleExcelReader.GetValue(83),
                        });

                        uniqueIdNum++;
                    }

                    firstRecord = false;
                }

                oleExcelReader.Close();
            }
            oleExcelConnection.Close();

            JavaScriptSerializer systemSerializer = new JavaScriptSerializer();
            systemSerializer.MaxJsonLength = Int32.MaxValue;

            return null;
        }



        // GET api/Account/UploadCsvFile
        [Route("api/uploadData")]
        public IHttpActionResult UploadData()
        {
            string appRoot = HttpContext.Current.Server.MapPath("~");
            string sSheetName = "Sheet1";
            string sConnection = null;
            DataTable dtTablesList = default(DataTable);
            OleDbCommand oleExcelCommand = default(OleDbCommand);
            OleDbDataReader oleExcelReader = default(OleDbDataReader);
            OleDbConnection oleExcelConnection = default(OleDbConnection);
            IList<object> plants = new List<object>();
            IList<KeyValuePair<String, Int32>> idNamePair = new List<KeyValuePair<String, Int32>>();//Need to have id as number because names are way too long
            Int32 currentId = 0;
            Int32 uniqueIdNum = 1;

            sConnection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + appRoot + "/plant_db.xlsx" + ";Extended Properties=\"Excel 12.0;HDR=No;IMEX=1\"";

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
                        plants.Add(new
                        {
                            plantId = oleExcelReader.GetValue(0),
                            commonFN = oleExcelReader.GetValue(1),
                            scientinficFN = oleExcelReader.GetValue(2),
                            family = oleExcelReader.GetValue(3),
                            family2 = oleExcelReader.GetValue(4),
                            commonName = oleExcelReader.GetValue(5),
                            scientificNameWeber = oleExcelReader.GetValue(6),
                            subspecies = oleExcelReader.GetValue(7),
                            iety = oleExcelReader.GetValue(8),
                            forma = oleExcelReader.GetValue(9),
                            familyAckerfield = oleExcelReader.GetValue(10),
                            scientificNameAckerField = oleExcelReader.GetValue(11),
                            ackerFieldPage = oleExcelReader.GetValue(12),
                            weber4thWestern = oleExcelReader.GetValue(13),
                            weber4thPage = oleExcelReader.GetValue(14),
                            commonNameSecondary = oleExcelReader.GetValue(15),
                            derivation = oleExcelReader.GetValue(16),
                            scientificNameOther = oleExcelReader.GetValue(17),
                            scientificNameMeaning = oleExcelReader.GetValue(18),
                            keyCharacteristics = oleExcelReader.GetValue(19),
                            flowerType = oleExcelReader.GetValue(20),
                            flowerColor = oleExcelReader.GetValue(21),
                            leafType = oleExcelReader.GetValue(22),
                            seasonOfBloom = oleExcelReader.GetValue(23),
                            growthForm = oleExcelReader.GetValue(24),
                            monocot = oleExcelReader.GetValue(25),
                            monoecious = oleExcelReader.GetValue(26),
                            lifeZone = oleExcelReader.GetValue(27),
                            edibility = oleExcelReader.GetValue(28),
                            toxicity = oleExcelReader.GetValue(29),
                            landscapingUse = oleExcelReader.GetValue(30),
                            matureHeight = oleExcelReader.GetValue(31),
                            matureSpread = oleExcelReader.GetValue(32),
                            siteRequirements = oleExcelReader.GetValue(33),
                            soilRequirements = oleExcelReader.GetValue(34),
                            moistureRequirements = oleExcelReader.GetValue(35),
                            ecologicalRelationships = oleExcelReader.GetValue(36),
                            frequency = oleExcelReader.GetValue(37),
                            endemicLocation = oleExcelReader.GetValue(38),
                            alien = oleExcelReader.GetValue(39),
                            comments = oleExcelReader.GetValue(40),
                            habitat = oleExcelReader.GetValue(41),
                            culti = oleExcelReader.GetValue(42),
                            fiber = oleExcelReader.GetValue(43),
                            otherUses = oleExcelReader.GetValue(44),
                            fruitColor = oleExcelReader.GetValue(45),
                            fruitType = oleExcelReader.GetValue(46),
                            print = oleExcelReader.GetValue(47),
                            familyCharacteristics = oleExcelReader.GetValue(48),
                            flowerShape = oleExcelReader.GetValue(49),
                            flowerSymmetry = oleExcelReader.GetValue(50),
                            flowerCluster = oleExcelReader.GetValue(51),
                            flowerSize = oleExcelReader.GetValue(52),
                            petalNumber = oleExcelReader.GetValue(53),
                            leafShape = oleExcelReader.GetValue(54),
                            flowerStructure = oleExcelReader.GetValue(55),
                            weedManagement = oleExcelReader.GetValue(56),
                            legalStatus = oleExcelReader.GetValue(57),
                            livestock = oleExcelReader.GetValue(58),
                            falcon12 = oleExcelReader.GetValue(59),
                            tellerCounty = oleExcelReader.GetValue(60),
                            greenMt12 = oleExcelReader.GetValue(61),
                            reynolds12 = oleExcelReader.GetValue(62),
                            bear12 = oleExcelReader.GetValue(63),
                            goldenGate = oleExcelReader.GetValue(64),
                            custerCounty = oleExcelReader.GetValue(65),
                            southValley = oleExcelReader.GetValue(66),
                            deerCreek = oleExcelReader.GetValue(67),
                            plainCc = oleExcelReader.GetValue(68),
                            maloitPark = oleExcelReader.GetValue(69),
                            vailNc = oleExcelReader.GetValue(70),
                            lovelandPass = oleExcelReader.GetValue(71),
                            noNameCreek = oleExcelReader.GetValue(72),
                            guanellaPass = oleExcelReader.GetValue(73),
                            southPlattePark = oleExcelReader.GetValue(74),
                            roxborough = oleExcelReader.GetValue(75),
                            castlewood = oleExcelReader.GetValue(76),
                            highPlains = oleExcelReader.GetValue(77),
                            dbg = oleExcelReader.GetValue(78),
                            grassesAtGreenMtn = oleExcelReader.GetValue(79),
                            scientificMeaningAckerField = oleExcelReader.GetValue(80),
                            eastPortal = oleExcelReader.GetValue(81),
                            mesaCounty = oleExcelReader.GetValue(82),
                            lmncMay = oleExcelReader.GetValue(83),
                        });

                        uniqueIdNum++;
                    }

                    firstRecord = false;
                }

                oleExcelReader.Close();
            }
            oleExcelConnection.Close();

            JavaScriptSerializer systemSerializer = new JavaScriptSerializer();
            systemSerializer.MaxJsonLength = Int32.MaxValue;

            return null;
        }



    }
}