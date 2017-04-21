using System;

namespace Shuttle.Abacus
{
    public class NullDependencyException: Exception
    {
        public NullDependencyException(string message) : base(message)
        {
        }
    }
}
