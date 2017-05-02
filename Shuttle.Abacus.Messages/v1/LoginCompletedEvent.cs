using System.Collections.Generic;
using Shuttle.Abacus.Messages.v1.TransferObjects;

namespace Shuttle.Abacus.Messages.v1
{
    public class LoginCompletedEvent 
    {
        public List<Permission> Permissions { get; set; }
    }
}
