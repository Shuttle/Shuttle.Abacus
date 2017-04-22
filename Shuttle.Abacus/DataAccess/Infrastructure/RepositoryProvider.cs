using System.Collections.Generic;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Abacus.Localisation;

namespace Shuttle.Abacus.DataAccess
{
    public class RepositoryProvider : IRepositoryProvider
    {
        private readonly Dictionary<string, IRepository> repositories = new Dictionary<string, IRepository>();

        public RepositoryProvider()
        {
            DependencyResolver.Resolver.ResolveAssignable<IRepository>().ForEach(
                repository => repositories.Add(repository.Name.ToLower(), repository));
        }

        public IRepository Get(string name)
        {
            if (!repositories.ContainsKey(name.ToLower()))
            {
                throw new KeyNotFoundException(string.Format(Resources.KeyNotFoundException, name, "RepositoryProvider"));
            }

            return repositories[name.ToLower()];
        }

        public IRepository Get<T>()
        {
            return Get(typeof(T).Name);
        }
    }
}