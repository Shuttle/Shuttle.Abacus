using Shuttle.Abacus.Shell.Core.Presentation;
using Shuttle.Abacus.Shell.Core.WorkItem;

namespace Shuttle.Abacus.Shell.Messages.Core
{
    public class ShowPresenterMessage : NullPermissionMessage
    {
        public IWorkItem WorkItem { get; private set; }
        public IPresenter Presenter { get; private set; }

        public ShowPresenterMessage(IWorkItem workItem, IPresenter presenter)
        {
            WorkItem = workItem;
            Presenter = presenter;
        }
    }
}
