using System.Web.Mvc;
using Abacus.Infrastructure;

namespace Abacus.Endpoints
{
    public class CacheController : Controller
    {
        private readonly ICache cache;

        public CacheController(ICache cache)
        {
            this.cache = cache;
        }

        public ActionResult Flush()
        {
            cache.Flush();

            return new EmptyResult();
        }
    }
}
