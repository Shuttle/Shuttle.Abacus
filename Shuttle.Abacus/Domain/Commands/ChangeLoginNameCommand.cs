using System;

namespace Shuttle.Abacus
{
    public class ChangeLoginNameCommand : IChangeLoginNameCommand
    {
        public Guid SystemUserId { get; set; }
        public string NewLoginName { get; set; }
    }
}
