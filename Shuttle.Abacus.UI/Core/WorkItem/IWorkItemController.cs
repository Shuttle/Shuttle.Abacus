using System;

namespace Abacus.UI
{
    public interface IWorkItemController : IDisposable
    {
        void AssignWorkItem(IWorkItem workItem);
    }
}
