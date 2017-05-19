using System.Collections.Generic;

namespace Shuttle.Abacus.Shell.Core.Messaging
{
    public interface ISubscriberProvider
    {
        IEnumerable<object> Subscribers { get; }
    }
}
