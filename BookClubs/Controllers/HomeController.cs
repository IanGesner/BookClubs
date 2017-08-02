using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookClubs.Controllers
{
    [RequireHttps]
    // Add the RequireHttps attribute to the Home controller to require all requests must use HTTPS. 
    // A more secure approach is to add the RequireHttps filter to the application. 
    // See the section "Protect the Application with SSL and the Authorize Attribute" in 
    // my tutoral Create an ASP.NET MVC app with auth and SQL DB and deploy to Azure App Service. 
    // A portion of the Home controller is shown below.
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}