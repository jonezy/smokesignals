using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Smokesignals.MvcExample.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            ViewBag.Message = "Welcome to ASP.NET MVC!";
            this.StoreInfo("Set from Home/Index");
            return View();
        }

        public ActionResult About() {
            return View();
        }
    }
}
