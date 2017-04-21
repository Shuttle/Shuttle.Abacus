using System.Web.Mvc;

namespace Abacus.Endpoints
{
    public class DefaultController : Controller
    {
        public ActionResult Index()
        {
            return View("Welcome");
        }
    }
}
