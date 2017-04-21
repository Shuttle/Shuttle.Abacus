using System;

namespace Shuttle.Abacus
{
    public interface IChangeLoginNameCommand
    {
        Guid SystemUserId { get; set; }
        string NewLoginName { get; set; }
    }
}
