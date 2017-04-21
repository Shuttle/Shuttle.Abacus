namespace Shuttle.Abacus.WebApi
{
    public class DefaultController : Controller
    {
        public ActionResult Index()
        {
            return View("Welcome");
        }
    }
}
