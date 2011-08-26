using System.Web.Mvc;

namespace Smokesignals.MvcExample.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            ViewBag.Message = "Welcome to ASP.NET MVC!";
            this.StoreInfo("Set from Home/Index");
            return View();
        }

        public ActionResult About() {
            this.Receive(MessageType.Success, "Success yo!");
            this.Receive(MessageType.Error, "Error yo!");
            return View();
        }
    }
}
