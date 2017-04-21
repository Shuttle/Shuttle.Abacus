namespace Abacus.UI
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
