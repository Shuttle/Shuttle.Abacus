using Shuttle.Abacus.Infrastructure;

namespace Shuttle.Abacus.Shell.Core.Presentation
{
    public class PresenterFactory : IPresenterFactory
    {
        private readonly IApplicationShell applicationShell;

        public PresenterFactory(IApplicationShell applicationShell)
        {
            this.applicationShell = applicationShell;
        }

        public T Create<T>() where T : IPresenter
        {
            var result = default(T);

            applicationShell.Invoke(() => result = DependencyResolver.Resolve<T>());

            return result;
        }
    }
}
