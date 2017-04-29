using System;

namespace Shuttle.Abacus.UI.Core.WorkItem
{
    public interface ICallbackRepository
    {
        string Register(IWorkItem workItem, Action action, bool complete);
        Action Find(string id);
        void Remove(string id);
    }
}