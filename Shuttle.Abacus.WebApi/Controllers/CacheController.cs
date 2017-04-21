using System.Web.Http;
using Shuttle.Abacus.Infrastructure;

namespace Shuttle.Abacus.WebApi
{
    public class CacheController : ApiController
    {
        private readonly ICache cache;

        public CacheController(ICache cache)
        {
            this.cache = cache;
        }

        public IHttpActionResult Flush()
        {
            cache.Flush();

            return Ok();
        }
    }
}
