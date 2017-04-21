using System;

namespace Shuttle.Abacus.Infrastructure
{
    public class NavigationItemNotFoundForMessageException : Exception
    {
        public NavigationItemNotFoundForMessageException(string message) : base(message)
        {
        }
    }
}
