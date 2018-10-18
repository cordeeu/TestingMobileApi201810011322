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
        public ActionResult UploadFiles(HttpPostedFileBase uploadFile, string fileType)
        {
            /*
             * assumed:  
             *     fileType does not include '/'
             *     a folder named *fileType* exists
             *     File must be .xlsx
             * not accounted for: 
             *     what to do with "new" or "undefined" fileTypes (undefined drops into route of filePath)
             *     error messages
            */
            this.uploadFile = uploadFile;


            if (UploadFile_Verify()){
                // Set Paths
                String tempPath = Path.GetTempPath();
                String filePath = "~/DataFolder/";
                String filePathArchive = filePath + "Archive/";
                // Standardize filenames
                String fileExt = Path.GetExtension(uploadFile.FileName).ToLower();
                var fileName = fileType+"_DataBase"+ fileExt;
                var fileNameArchive = fileType+"_DataBaseArchive"+ fileExt;
                var fileNameTemp = fileType + "_DBUploaded"+ DateTime.Now.ToString("yyyy-MM-dd--HH-mm-ss")+fileExt;

                if (!(fileType == null || fileType == ""))
                {
                    filePath += fileType + "/";
                    filePathArchive += fileType + "/";
                    //TODO: create folder if doesnt exist
                }
                //Temp save- Archive current file and store new file (~/DataFolder/*Archive*/*FileType*)
                var saveTempPath = Path.Combine(tempPath, fileNameTemp);
                var savePath = Path.Combine(Server.MapPath(filePath), fileName);
                var saveArchivePath = Path.Combine(Server.MapPath(filePathArchive), fileNameArchive);

                try
                {
                uploadFile.SaveAs(saveTempPath);
                System.IO.File.Copy(savePath, saveArchivePath,true);
                }
                catch(IOException e) {
                    Debug.WriteLine("failed to save or copy  "+e);
                }
                uploadFile.SaveAs(savePath);
            }
            else {
            //TODO: return "INCORRECT UPLOAD" to view
            }

            
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
                    {".xlsx"};
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
        public ActionResult Testing(HttpPostedFileBase file)//, string fileType)
        {
            String filePath = "c:\\temp\\";
            String filePathArchive = "C:\\temp\\MobileAPI\\";
            // Standardize filename
            var fileName = file.FileName;
            var fileNameArchive = "_DataBaseArchive"+fileName;


            
            // Archive current file and store new file (~/DataFolder/*Archive*/*FileType*)
            var path = Path.Combine(filePath, fileName);
            var pathArchive = Path.Combine(filePathArchive, fileNameArchive);
            try
            {
                System.IO.File.Copy(path, pathArchive);
            }
            catch
            {

            }
            file.SaveAs(path);

            return RedirectToAction("Index");
        }
    }
}