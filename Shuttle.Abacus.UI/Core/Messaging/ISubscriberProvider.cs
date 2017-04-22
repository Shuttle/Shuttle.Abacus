using System.Collections.Generic;

namespace Shuttle.Abacus.UI.Core.Messaging
{
    public interface ISubscriberProvider
    {
        IEnumerable<object> Subscribers { get; }
    }
}
