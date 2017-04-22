using Shuttle.Abacus.UI.Core.Presentation;
using Shuttle.Abacus.UI.Core.WorkItem;

namespace Shuttle.Abacus.UI.Messages.Core
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
