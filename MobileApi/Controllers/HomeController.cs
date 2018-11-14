using MobileApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace MobileApi.Controllers
{
    public class HomeController : Controller
    {
        private HttpPostedFileBase uploadFile;
        public WoodyPlantsMobileApiContext woodyDb = new WoodyPlantsMobileApiContext();
        public WoodyPlant[] woodyPlantCollection;
        private string uploadStatus;
        private string uploadFileArchivePath;
        private string uploadFilePath;

        //public string UploadStatus { get => uploadStatus; }

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
        public ActionResult UploadFiles(HttpPostedFileBase uploadFile, string dbType)
        {
            this.uploadFile = uploadFile;
            bool uploadSuccess = false;

            try
            {
                string[] dbFilePaths = AssignDBFileSavePaths(dbType, Path.GetExtension(uploadFile.FileName).ToLower());


                if (DBFile_Verify())
                {
                    try
                    {
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
                        Debug.WriteLine("failed to save or copy upload " + e);
                        return RedirectToAction("IndexFail");

                    }
                }
                else
                {
                    //ERROR Status
                    this.uploadStatus = "Empty or Incorrect file extension";
                    return RedirectToAction("IndexFail");
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
                System.IO.File.Copy(uploadFileArchivePath, uploadFilePath, true);
                return RedirectToAction("IndexSuccess");

            }
            else
            {
                //TODO: return upload FAIL dude; file not archived.
                if (System.IO.File.Exists(this.uploadFileArchivePath))
                {
                    System.IO.File.Delete(this.uploadFileArchivePath);
                }
                return RedirectToAction("IndexFail");
            }
            // return RedirectToAction("Index");
        }

        [HttpGet]
        public Boolean DBFile_Verify()
        {
            Boolean fileOK = false;
            if (uploadFile != null && uploadFile.ContentLength > 0)
            {
                String fileExtension = Path.GetExtension(uploadFile.FileName).ToLower();
                String[] allowedExtensions =
                    {".xlsx",".txt", ".csv"};
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

        [HttpPost]
        public ActionResult RevertDatabase(string dbType)
        {
            string[] dbSavePaths = AssignDBFileSavePaths(dbType, ".xlsx");
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

        [HttpGet]
        public string[] AssignDBFileSavePaths(string dbType, string fileExt)
        {
            /*  assumed:  
             *     dbType does not include '/'
             *     a folder named *dbType* exists
             *     File must be .xlsx
             *     
             *  not accounted for: 
             *     what to do with "new" or "undefined" dbType (undefined drops into route of filePath)
             *     error messages
            */

            // Set Paths
            string tempPath = Path.GetTempPath();
            string filePath = "~/DataFolder/";
            string filePathArchive = filePath + "Archive/";
            if (!(dbType == null || dbType == ""))
            {
                filePath += dbType + "/";
                filePathArchive += dbType + "/";
                //TODO: create folder if doesnt exist
            }
            // Standardize filenames
            var fileName = dbType + "_DataBase" + fileExt;
            var fileNameArchive = dbType + "_DataBaseArchive" + DateTime.Now.ToString("yyyy-MM-dd--HH-mm-ss") + fileExt;
            var fileNameTemp = dbType + "_DBUploaded" + DateTime.Now.ToString("yyyy-MM-dd--HH-mm-ss") + fileExt;


            //Return Location&filename strings of Temporary DB--current DB--Archive DB 
            string saveTempPath = Path.Combine(tempPath, fileNameTemp);
            string savePath = Path.Combine(Server.MapPath(filePath), fileName);
            string saveArchivePath = Path.Combine(Server.MapPath(filePathArchive), fileNameArchive);
            //Assign Global Variables Archive and Main Database file
            this.uploadFileArchivePath = saveArchivePath;
            this.uploadFilePath = savePath;

            string[] filePaths = { saveArchivePath, savePath, saveTempPath };
            return filePaths;
        }

        [HttpPost]
        public Boolean UploadWoodyData(string dbFilePath)
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
                        if (debugCounter++ == 190)
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

    }
}