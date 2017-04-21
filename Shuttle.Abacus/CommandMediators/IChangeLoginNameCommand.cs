using System;

namespace Abacus.CommandMediators
{
    public interface IChangeLoginNameCommand
    {
        Guid SystemUserId { get; set; }
        string NewLoginName { get; set; }
    }
}
