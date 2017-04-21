using System;

namespace Shuttle.Abacus
{
    public class NavigationItemNotFoundForMessageException : Exception
    {
        public NavigationItemNotFoundForMessageException(string message) : base(message)
        {
        }
    }
}
