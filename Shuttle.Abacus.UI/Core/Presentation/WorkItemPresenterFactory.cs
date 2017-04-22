using Shuttle.Abacus.Infrastructure;

namespace Shuttle.Abacus.UI.Core.Presentation
{
    public class WorkItemPresenterFactory : IWorkItemPresenterFactory
    {
        private readonly IShell shell;

        public WorkItemPresenterFactory(IShell shell)
        {
            this.shell = shell;
        }

        public IWorkItemPresenter Create<TWorkItemPresenter>() where TWorkItemPresenter : IWorkItemPresenter
        {
            IWorkItemPresenter result = null;

            shell.Invoke(() => result = DependencyResolver.Resolve<TWorkItemPresenter>());

            return result;
        }
    }
}
