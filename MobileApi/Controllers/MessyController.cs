using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace MobileApi.Controllers
{
    public class MessyController : Controller
    {
        public Boolean wetlandsKey = false;
        public Boolean woodyKey = false;
        public Boolean flowerKey = false;

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            // System.Web.Mvc.Controller
            return View();
        }

        public ActionResult Index2()
        {
            ViewBag.Title = "Home Page";

            return View();
        }


        [HttpPost]
        public ActionResult ButtonPressed(HttpPostedFileBase uploadFile, string dbType)
        {
            HomeController homeController = new HomeController();
            homeController.UploadImages(uploadFile, dbType);

            return RedirectToAction("../Home/Index");
        }
        [HttpGet]
        public ActionResult ValidateKey(String key)
        {

            /*   WetlandSetting wetS = wetlandDb.WetlandSettings.Where(b => b.name == "Key").FirstOrDefault();
               WoodySetting woodyS = woodyDb.WoodySettings.Where(b => b.name == "Key").FirstOrDefault();
               FlowerSetting flowerS = flowerDb.FlowerSettings.Where(b => b.name == "Key").FirstOrDefault();

               if (wetS != null) 
                   if (wetS.valuetext == key)
                       wetlandsKey = true;

               if (woodyS != null)
                   if (woodyS.valuetext == key)
                       woodyKey = true;

               if (flowerS != null)
                   if (flowerS.valuetext == key)
                       flowerKey = true;

               if (wetlandsKey == true)
               return View("~/Views/Stuff/wetland.cshtml");

               if (woodyKey == true)
                   return View("~/Views/Stuff/woody.cshtml");

               if (flowerKey == true)
                   return View("~/Views/Stuff/flower.cshtml");

               else*/
            return View("~/Views/Home/index.cshtml");
        }

        [HttpGet]
        public ActionResult copySpecificFile(String uploadFile)
        {

            string FileToCopy = null;
            string NewCopy = null;
            string nameNew = "farts.txt";

            if (uploadFile == null)
            {
                nameNew = "nullCunt.txt";
                FileToCopy = "C:\\Temp\\Farts\\Toots.txt";
                NewCopy = "C:\\Temp\\Toots\\" + nameNew;

            }
            else
            {
                FileToCopy = uploadFile;
                nameNew = System.IO.Path.GetFileNameWithoutExtension(uploadFile);
                nameNew = System.IO.Path.GetFileName(uploadFile);
                NewCopy = "C:\\Temp\\Toots\\" + nameNew;
            }



            if (System.IO.File.Exists(FileToCopy) == true && !System.IO.File.Exists(NewCopy))
            {
                System.IO.File.Copy(FileToCopy, NewCopy);
            }
            else if (System.IO.File.Exists(FileToCopy) == true && System.IO.File.Exists(NewCopy))
            {
                System.IO.File.Delete(NewCopy);
            }

            return null;
            //return View("/Views/Home/index.cshtml");
        }
        [HttpGet]
        public ActionResult UploadFile(FileUpload fileUpload)
        {


            Boolean fileOK = false;
            String path = Server.MapPath("~/UploadedImages/");
            Label label1=new Label();
            if (fileUpload.HasFile)
            {
                String fileExtension =
                    System.IO.Path.GetExtension(fileUpload.FileName).ToLower();
                String[] allowedExtensions =
                    {".csv", ".xlsx", ".txt", ".jpg"};
                for (int i = 0; i < allowedExtensions.Length; i++)
                {
                    if (fileExtension == allowedExtensions[i])
                    {
                        fileOK = true;
                        break;
                    }
                }
            }

            if (fileOK)
            {
                try
                {
                    fileUpload.PostedFile.SaveAs(path
                        + fileUpload.FileName);
                    label1.Text = "File uploaded!";
                }
                catch (Exception)
                {
                    label1.Text = "File could not be uploaded.";
                }
            }
            else
            {
                label1.Text = "Cannot accept files of this type.";
            }

                return null;
        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            // Verify that the user selected a file
            if (file != null && file.ContentLength > 0)
            {
                // extract only the filename
                var fileName = Path.GetFileName(file.FileName);
                // store the file inside ~/App_Data/uploads folder
                var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                file.SaveAs(path);
            }
            // redirect back to the index action to show the form once again
            return RedirectToAction("Index");
        }


    }
}
