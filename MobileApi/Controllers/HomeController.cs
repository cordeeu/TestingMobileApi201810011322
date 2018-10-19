using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace MobileApi.Controllers
{
    public class HomeController : Controller
    {
        HttpPostedFileBase uploadFile;

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            // System.Web.Mvc.Controller
            return View();
        }

        [HttpPost]
        public ActionResult UploadFiles(HttpPostedFileBase uploadFile, string dbType)
        {


            this.uploadFile = uploadFile;//maybe shouldnt be using a global variable?

            if (UploadFile_Verify()){

                string[] dbSavePaths=AssignDBFileSavePaths(dbType);

                try
                {
                    uploadFile.SaveAs(dbSavePaths[0]); //create TEMP file
                    System.IO.File.Copy(dbSavePaths[1], dbSavePaths[2], true); //copy current DB to archive folder
                }
                catch (IOException e)
                {
                    Debug.WriteLine("failed to save or copy  " + e);
                }
                uploadFile.SaveAs(dbSavePaths[1]);
            }
            else {
            //TODO: return "INCORRECT UPLOAD" to view
            }

            BaseController baseController = new BaseController();
            baseController.UploadData();


            // redirect back to the index action to show the form once again
            return RedirectToAction("Index");
        }

        [HttpGet]
        public Boolean UploadFile_Verify()
        {
            Boolean fileOK = false;
            if (uploadFile != null&&uploadFile.ContentLength>0)
            {
                String fileExtension = Path.GetExtension(uploadFile.FileName).ToLower();
                String[] allowedExtensions =
                    {".xlsx",".txt"};
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
            string[] dbSavePaths = AssignDBFileSavePaths(dbType);
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
        public string[] AssignDBFileSavePaths(string dbType)
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
            // Standardize filenames
            string fileExt = Path.GetExtension(uploadFile.FileName).ToLower();
            var fileName = dbType + "_DataBase" + fileExt;
            var fileNameArchive = dbType + "_DataBaseArchive" + fileExt;
            var fileNameTemp = dbType + "_DBUploaded" + DateTime.Now.ToString("yyyy-MM-dd--HH-mm-ss") + fileExt;

            if (!(dbType == null || dbType == ""))
            {
                filePath += dbType + "/";
                filePathArchive += dbType + "/";
                //TODO: create folder if doesnt exist
            }
            //Return Location&filename strings of Temporary DB--current DB--Archive DB 
            string saveTempPath = Path.Combine(tempPath, fileNameTemp);
            string savePath = Path.Combine(Server.MapPath(filePath), fileName);
            string saveArchivePath = Path.Combine(Server.MapPath(filePathArchive), fileNameArchive);

            string[] filePaths = {saveTempPath,savePath,saveArchivePath};
            return filePaths;
        }

        [HttpPost]
        public ActionResult Testing(HttpPostedFileBase file)//, string dbType)
        {
            String filePath = "c:\\temp\\";
            String filePathArchive = "C:\\temp\\MobileAPI\\";
            // Standardize filename
            var fileName = file.FileName;
            var fileNameArchive = "_DataBaseArchive"+fileName;


            
            // Archive current file and store new file (~/DataFolder/*Archive*/*dbType*)
            var path = Path.Combine(filePath, fileName);
            var pathArchive = Path.Combine(filePathArchive, fileNameArchive);
            try
            {
                System.IO.File.Copy(path, pathArchive);
            }
            catch(IOException e)
            {
                Debug.WriteLine("failed to save or copy  " + e);
            }
            file.SaveAs(path);

            return RedirectToAction("Index");
        }
    }
}