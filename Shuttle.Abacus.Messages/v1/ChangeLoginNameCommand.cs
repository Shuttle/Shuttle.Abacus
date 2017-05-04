using System;

namespace Shuttle.Abacus.Messages.v1
{
    public class ChangeLoginNameCommand 
    {
        public Guid SystemUserId { get; set; }
        public string NewLoginName { get; set; }
    }
}
