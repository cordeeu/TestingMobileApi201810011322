using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileApi.Controllers
{
    public class HomeController : Controller
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
    }
}
