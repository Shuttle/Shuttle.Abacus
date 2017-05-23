using System.Collections.Generic;
using Shuttle.Abacus.Domain;

namespace Shuttle.Abacus
{
    public class CachedArgumentProvider : ICachedArgumentProvider
    {
        private readonly object _lock = new object();
        private bool _cached;

        public CachedArgumentProvider()
        {
        }

        public IEnumerable<Argument> All()
        {
            throw new System.NotImplementedException();
        }

        public void Flush()
        {
            throw new System.NotImplementedException();
        }
    }
}