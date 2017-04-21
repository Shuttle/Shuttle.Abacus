using System;

namespace Shuttle.Abacus.Domain
{
    public class ChangeLoginNameCommand 
    {
        public Guid SystemUserId { get; set; }
        public string NewLoginName { get; set; }
    }
}
