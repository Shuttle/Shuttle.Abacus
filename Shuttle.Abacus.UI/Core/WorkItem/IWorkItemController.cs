using System;

namespace Shuttle.Abacus.UI.Core.WorkItem
{
    public interface IWorkItemController : IDisposable
    {
        void AssignWorkItem(IWorkItem workItem);
    }
}
