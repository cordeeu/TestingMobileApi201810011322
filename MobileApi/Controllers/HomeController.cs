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
    public class HomeController : Controller
    {
        //VIEWS
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
        public ActionResult MessyAround()
        {
            ViewBag.Title = "BackUp";
            // System.Web.Mvc.Controller
            return View();
        }
        public ActionResult RevertTesting()
        {
            ViewBag.Title = "BackUp";
            // System.Web.Mvc.Controller
            return View();
        }

    }
}