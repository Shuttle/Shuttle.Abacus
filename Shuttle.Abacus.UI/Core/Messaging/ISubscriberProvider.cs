using System.Collections.Generic;

namespace Abacus.UI
{
    public interface ISubscriberProvider
    {
        IEnumerable<object> Subscribers { get; }
    }
}
