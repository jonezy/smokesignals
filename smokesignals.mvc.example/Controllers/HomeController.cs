using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace smokesignals.mvc.example.Controllers {
    [HandleError]
    public class HomeController : Controller {
        public ActionResult Index() {
            ViewData["Message"] = "Welcome to ASP.NET MVC!";
            
            this.StoreInfo("This wa5 flashed from the HomeController/Home Action");

            return View();
        }

        public ActionResult About() {
            return View();
        }
    }
}
