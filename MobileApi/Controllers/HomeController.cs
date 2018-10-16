using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace MobileApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            // System.Web.Mvc.Controller
            return View();
        }

        [HttpPost]
        public ActionResult AddFileUploads(HttpPostedFileBase file)
        {
            Boolean fileOK = false;
            // Verify that the user selected a file
            if (file != null && file.ContentLength > 0)
            {
                // extract only the filename
                var fileName = Path.GetFileName(file.FileName);
                // store the file inside ~/App_Data/uploads folder
                var path = Path.Combine(Server.MapPath("~/App_Data/"), fileName);
                file.SaveAs(path);
            }
            // redirect back to the index action to show the form once again
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult UploadFile_Verify(FileUpload fileUpload)
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
    }
}