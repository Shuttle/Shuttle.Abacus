using System.Collections.Generic;
using Shuttle.Abacus.Infrastructure;

namespace Shuttle.Abacus.Domain
{
    public class LoginCompletedEvent 
    {
        public List<Permission> Permissions { get; set; }
    }
}
