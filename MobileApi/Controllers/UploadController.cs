using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using MobileApi.Models;
using System.IO.Compression;
using System.Diagnostics;
using System.Data;
using System.Data.OleDb;
using System.Web.Script.Serialization;
using System.Web.Hosting;

namespace MobileApi.Controllers
{
    public class UploadController : Controller
    {
        //Variables
        private HttpPostedFileBase uploadFile;
        private string dbType;
        public WoodyPlantsMobileApiContext woodyDb = new WoodyPlantsMobileApiContext();
        public WoodyPlant[] woodyPlantCollection;
        private string uploadStatus; //TODO: send a success/ErrorMessage to this file can use to fail out of any other method
        private string fileLocation;
        private string archiveLocation;
        private string uploadWhat;// TODO: indicate what is being uploaded.....
        private List<string> ImageNames;
        private string routeSavePath;

        //public string UploadStatus { get => uploadStatus; }
        public UploadController()
        {
            //Create routeSavePath and routeArchivePath if not exist
            this.routeSavePath = HostingEnvironment.MapPath("~/DataFolder/");
            string routeArchivePath = routeSavePath + "Archive\\";
            DirectoryInfo di = Directory.CreateDirectory(routeSavePath);
            di = Directory.CreateDirectory(routeArchivePath);
            //Assign Default Variables
            this.fileLocation = routeSavePath;
            this.archiveLocation = routeArchivePath;
            this.uploadStatus = "Started";
        }
        public ActionResult Index()
        {
            ViewBag.Title = "Home";
            // System.Web.Mvc.Controller
            return View();
        }
        public ActionResult IndexSuccess()
        {
            ViewBag.Title = "Success";
            // System.Web.Mvc.Controller
            return View();
        }
        public ActionResult IndexFail()
        {
            ViewBag.Title = "Fail";
            // System.Web.Mvc.Controller
            return View();
        }
        public ActionResult Messy()
        {
            ViewBag.Title = "Messy";
            // System.Web.Mvc.Controller
            return View();
        }
        public ActionResult IndexBackup()
        {
            ViewBag.Title = "BackUp";
            // System.Web.Mvc.Controller
            return View();
        }

        [HttpPost]
        public ActionResult UploadImages(HttpPostedFileBase uploadFile, string dbType)
        {
            try
            {
                bool imageFound = false; //TODO: give feedback about file found state
                this.uploadWhat = "image";
                string fileExt = Path.GetExtension(uploadFile.FileName).ToLower();
                if (fileExt == ".zip")
                {
                    string[] dbFilePaths = AssignDBFileSavePaths(dbType, "images", fileExt);
                    //Save copy to Archive File
                    string zipPath = dbFilePaths[0];
                    uploadFile.SaveAs(zipPath);

                    // Normalizes the path.
                    string extractPath = dbFilePaths[1]; // Path.GetFullPath(extractPath);

                    /*Ensures that the last character on the extraction path 
                     * is the directory separator char.
                     * Without this, a malicious zip file could try to traverse outside of the expected
                     * extraction path.
                    */
                    if (!extractPath.EndsWith(Path.DirectorySeparatorChar.ToString()))
                        extractPath += Path.DirectorySeparatorChar;

                    using (ZipArchive archive = ZipFile.OpenRead(zipPath))
                    {
                        foreach (ZipArchiveEntry entry in archive.Entries)
                        {
                            if (entry.FullName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                            {
                                // Gets the full path to ensure that relative segments are removed.
                                string destinationPath = Path.GetFullPath(Path.Combine(extractPath, entry.Name));
                                System.IO.FileInfo file = new System.IO.FileInfo(destinationPath);
                                file.Directory.Create();
                                // Ordinal match is safest, case-sensitive volumes can be mounted within volumes that
                                // are case-insensitive.
                                if (destinationPath.StartsWith(extractPath, StringComparison.Ordinal))
                                    entry.ExtractToFile(destinationPath, true);
                                imageFound = true;
                            }
                        }
                    }
                    if (imageFound)
                        return RedirectToAction("IndexSuccess");
                    else
                    {
                        System.IO.FileInfo file = new System.IO.FileInfo(zipPath);
                        file.Delete();
                        return RedirectToAction("IndexFail");
                    }
                }
            }
            catch (IOException e)
            {
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult UploadFiles(HttpPostedFileBase uploadFile, string dbType)
        {
            this.uploadFile = uploadFile;
            bool uploadSuccess = false;
            JsonResult result = new JsonResult();

            try
            {
                string[] dbFilePaths = AssignDBFileSavePaths(dbType, "data", Path.GetExtension(uploadFile.FileName).ToLower());
                if (DataFile_Verify())
                {
                    try
                    {
                        //uploadFile.SaveAs("nonsense"); //create ARCHIVE file
                        uploadFile.SaveAs(dbFilePaths[0]); //create ARCHIVE file
                        switch (dbType)
                        {
                            case "WoodyPlant":
                                uploadSuccess = UploadWoodyData(dbFilePaths[0]);
                                //uploadSuccess = Testing(dbFilePaths[0]);
                                break;
                            case "Wetland":
                                //TODO: create UploadWetlandData(dbfilepaths[0]);
                                break;
                            default:
                                break;
                        }
                    }
                    catch (IOException e)
                    {
                        if (System.IO.File.Exists(dbFilePaths[0]))
                        {
                            System.IO.File.Delete(dbFilePaths[0]);
                        }
                        string status = "failed to save or copy upload " + e;
                        Debug.WriteLine(status);
                        result.Data = status;
                        //return RedirectToAction("IndexFail");
                        return result;
                    }
                }
                else
                {
                    //ERROR Status
                    this.uploadStatus = "Empty or Incorrect file extension";
                    result.Data = "File Type incorrect";
                    return result;
                }
            }
            catch
            {
                uploadSuccess = false;
            }
            if (uploadSuccess)
            {
                //SuccessMessage: return a successful dude
                this.uploadStatus = "Upload Successful";
                //System.IO.File.Copy(fileLocation, archiveLocation, true);
                System.IO.File.Copy(archiveLocation, fileLocation, true);
                result.Data = "Upload Successful";
                return result;

            }
            else
            {
                //TODO: return upload FAIL dude; file not archived.
                if (System.IO.File.Exists(this.fileLocation))
                {
                    System.IO.File.Delete(this.fileLocation);
                }
                result.Data = "Upload Processing Failure";
                return result;
            }
            // return RedirectToAction("Index");
        }

        public bool DataFile_Verify()
        {
            Boolean fileOK = false;
            if (uploadFile != null && uploadFile.ContentLength > 0)
            {
                String fileExtension = Path.GetExtension(uploadFile.FileName).ToLower();
                String[] allowedExtensions = { ".xlsx" };
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])
                    {
                        fileOK = true;
                        break;
                    }
                }
            }
            return fileOK;
        }
        public bool ImageUpload_NameVerify(string fileName)
        {
            this.ImageNames = GetAllWoodyImageNames();

            return true;
        }

        [HttpPost]
        public ActionResult RevertDatabase(string dbType)
        {
            string[] dbSavePaths = AssignDBFileSavePaths(dbType, "data", ".xlsx");
            try
            {
                System.IO.File.Copy(dbSavePaths[2], dbSavePaths[1], true); //copy current DB to archive folder
            }
            catch (IOException e)
            {
                Debug.WriteLine("failed to save or copy  " + e);
            }



            return RedirectToAction("Index");
        }

        public string[] AssignDBFileSavePaths(string dbTypeFolder, string attrib, string fileExt)
        {
            // Append with DataBase Type Paths
            if (!(dbTypeFolder == null || dbTypeFolder == ""))
            {
                this.archiveLocation += dbTypeFolder + "\\";
                this.fileLocation += dbTypeFolder + "\\";
            }
            //Create directory if not exist
            DirectoryInfo die = Directory.CreateDirectory(archiveLocation);
            die = Directory.CreateDirectory(fileLocation);
            // Standardize filenames
            var fileNameArchive = dbTypeFolder + "_Archive_" + attrib + DateTime.Now.ToString("yyyy-MM-dd--HH-mm-ss") + fileExt;
            var fileName = dbTypeFolder + "_" + attrib + fileExt;
            string saveArchivePath = Path.Combine(archiveLocation, fileNameArchive);
            string savePath = Path.Combine(fileLocation, fileName);
            //Assign Global Variables Archive and Main Database file
            this.archiveLocation = saveArchivePath;
            this.fileLocation = savePath;

            string[] filePaths = { saveArchivePath, savePath };
            return filePaths;
        }

        public string[] AssignImageFileSavePaths(string dbType, string fileExt)
        {
            string[] names = AssignDBFileSavePaths(dbType, "images", fileExt);

            string[] filePaths = { names[0], names[1] };
            return filePaths;
        }

        public bool UploadWoodyData(string dbFilePath)
        {
            bool uploadSuccess = false;
            string sSheetName = "Sheet1";
            string sConnection = null;
            DataTable dtTablesList = default(DataTable);
            OleDbCommand oleExcelCommand = default(OleDbCommand);
            OleDbDataReader oleExcelReader = default(OleDbDataReader);
            OleDbConnection oleExcelConnection = default(OleDbConnection);
            IList<KeyValuePair<String, Int32>> idNamePair = new List<KeyValuePair<String, Int32>>();//Need to have id as number because names are way too long

            WoodyPlantsMobileApiContext plantDb = new WoodyPlantsMobileApiContext();
            List<WoodyPlant> plantCollection = new List<WoodyPlant>();

            //Int32 currentId = 0;
            //////////////////////Int32 uniqueIdNum = 1;

            try
            {
                sConnection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + dbFilePath + ";Extended Properties=\"Excel 12.0;HDR=No;IMEX=1\"";

                oleExcelConnection = new OleDbConnection(sConnection);
                oleExcelConnection.Open();
                dtTablesList = oleExcelConnection.GetSchema("Tables");
                int attribCount = 0;

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
                    attribCount = oleExcelReader.FieldCount;
                    string[] plantAttribs = new string[attribCount];

                    var firstRecord = true;
                    int debugCounter = 0;


                    while (oleExcelReader.Read())
                    {

                        var newPlant = new WoodyPlant();
                        if (debugCounter++ == 198)
                            Console.Write(debugCounter);

                        for (int i = 0; i < attribCount; i++)
                        {
                            if (i == 49)
                                Console.Write(debugCounter);
                            if (firstRecord)
                            {
                                plantAttribs[i] = oleExcelReader.GetValue(i).ToString();
                            }
                            else
                            {
                                if (plantAttribs[i] == "plant_imported_id")
                                {
                                    var theDude = oleExcelReader.GetValue(i);
                                    newPlant.GetType().GetProperty(plantAttribs[i]).SetValue(newPlant, Convert.ToInt32(oleExcelReader.GetValue(i)), null);
                                }
                                else
                                {
                                    string plantAttrib = plantAttribs[i];
                                    string plantAtrribVal = oleExcelReader.GetValue(i).ToString();
                                    newPlant.GetType().GetProperty(plantAttrib).SetValue(newPlant, plantAtrribVal, null);
                                }
                            }


                        }
                        uploadSuccess = false;
                        if (!firstRecord)
                            plantCollection.Add(newPlant);
                        uploadSuccess = true;
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
                //uploadSuccess = false;
                return false;
            }

            if (uploadSuccess)
            {
                int yelpMe = plantDb.Plants.AddRange(plantCollection).Count();
                if (yelpMe > 0)
                {
                    int flippyCup = plantDb.Plants.RemoveRange(plantDb.Plants).Count();
                    plantDb.SaveChanges();
                }
                else
                {
                    //TODO: create message about why failed to return
                    uploadSuccess = false;
                }
            }

            JavaScriptSerializer systemSerializer = new JavaScriptSerializer();
            systemSerializer.MaxJsonLength = Int32.MaxValue;

            return uploadSuccess;
        }

        [HttpPost]
        public Boolean RemoveAllData(WoodyPlantsMobileApiContext woodyDbRemove)
        {
            try
            {
                //////////////////foreach (var entity in woodyDbRemove.Plants)
                ///////////////woodyDbRemove.Plants.Remove(entity);
                int flippyCup = woodyDbRemove.Plants.RemoveRange(woodyDbRemove.Plants).Count();
                woodyDbRemove.SaveChanges();
                return true;
            }
            catch
            { return false; }
        }

        public List<string> GetAllWoodyImageNames()
        {
            List<string> imageNamesDB = new List<string>();
            WoodyPlantsMobileApiContext plantDb = new WoodyPlantsMobileApiContext();
            foreach (var plant in plantDb.Plants)
            {
                List<string> names = plant.imageNames.Split(',').ToList<string>();
                foreach (var name in names)
                    imageNamesDB.Add(name.Trim());
            }
            return imageNamesDB;
        }


        [HttpPost]
        public ActionResult Balls()
        {
            string[] randomNames = { "abies_arizonica_1", "abies_arizonica_2", "abies_arizonica_3", "abies_arizonica_4", "yucca_glauca_1", "yucca_glauca_2", "yucca_glauca_3", "yucca_glauca_4" };
            bool thereQuestion;
            List<string> tits = GetAllWoodyImageNames();
            foreach (string name in randomNames)
            {
                thereQuestion = false;
                if (!tits.Contains(name))
                    break;

                thereQuestion = true;
            }

            int i = 1;

            return RedirectToAction("IndexSuccess");
        }
        [HttpPost]
        public ActionResult GetPreviousDataFiles(string dbType)
        //public ActionResult GetPreviousDataFiles(string dbType)
        {
            string suckit = dbType;
            dbType = "WoodyPlant";
            string path = routeSavePath +"Archive"+"\\"+dbType;
            JsonResult result = new JsonResult();
            DirectoryInfo d = new DirectoryInfo(path);
            FileInfo[] Files = d.GetFiles("*.xlsx");
            string str = "[";
            foreach (FileInfo file in Files)
            {
                str += "{\"name\":\"" + file.Name + "\",";
                str += "\"date\":\"" + file.CreationTime + "\"},";
            }
            str += "]";
            //MessageBox.Show(suckit);
            result.Data = str;

            return result;

        }
        [HttpPost]
        public void GetPreviousDataFilesVoid()
        //public ActionResult GetPreviousDataFiles(string dbType)
        {
            string path = routeSavePath + "Archive" + "\\WoodyPlant";
            JsonResult result = new JsonResult();
            DirectoryInfo d = new DirectoryInfo(path);
            FileInfo[] Files = d.GetFiles("*.xlsx");
            string str = "";
            foreach (FileInfo file in Files)
            {
                str += "{\"name\":\"" + file.Name + "\",";
                str += "\"date\":\""+file.CreationTime+"\"}";
            }
            result.Data = str;
            result.Data = "killmeNOW";
            MessageBox.Show("killmeVoid");
        }
    }
}