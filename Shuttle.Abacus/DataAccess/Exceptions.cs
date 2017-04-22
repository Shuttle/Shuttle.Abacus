using System;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.DataAccess
{
    public class Exceptions
    {
        public static Exception MissingEntity(string name, Guid id)
        {
            return new MissingEntityException(string.Format("Could not find entity '{0}' with an id of '{1}'.", name, id));
        }
    }
}