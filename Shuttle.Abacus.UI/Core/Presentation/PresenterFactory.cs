using Abacus.Infrastructure;

namespace Abacus.UI
{
    public class PresenterFactory : IPresenterFactory
    {
        private readonly IShell shell;

        public PresenterFactory(IShell shell)
        {
            this.shell = shell;
        }

        public T Create<T>() where T : IPresenter
        {
            var result = default(T);

            shell.Invoke(() => result = DependencyResolver.Resolve<T>());

            return result;
        }
    }
}
