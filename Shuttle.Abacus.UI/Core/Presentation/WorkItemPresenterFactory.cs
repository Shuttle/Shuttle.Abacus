using Shuttle.Abacus.Infrastructure;

namespace Shuttle.Abacus.Shell.Core.Presentation
{
    public class WorkItemPresenterFactory : IWorkItemPresenterFactory
    {
        private readonly IApplicationShell applicationShell;

        public WorkItemPresenterFactory(IApplicationShell applicationShell)
        {
            this.applicationShell = applicationShell;
        }

        public IWorkItemPresenter Create<TWorkItemPresenter>() where TWorkItemPresenter : IWorkItemPresenter
        {
            IWorkItemPresenter result = null;

            applicationShell.Invoke(() => result = DependencyResolver.Resolve<TWorkItemPresenter>());

            return result;
        }
    }
}
