using System.Collections.Generic;
using Abacus.Infrastructure;

namespace Abacus.UI
{
    public class WorkspaceProvider : IWorkspaceProvider
    {
        private readonly Dictionary<string, IWorkspacePresenter> singletons =  new Dictionary<string, IWorkspacePresenter>();

        public T Get<T>() where T : IWorkspacePresenter
        {
            var key = typeof (T).FullName;

            return singletons.ContainsKey(key) ? (T)singletons[key] : DependencyResolver.Resolver.Resolve<T>();
        }

        public T RegisterSingleton<T>() where T : IWorkspacePresenter
        {
            var presenter = DependencyResolver.Resolver.Resolve<T>();

            singletons.Add(typeof(T).FullName, presenter);

            return presenter;
        }
    }
}
