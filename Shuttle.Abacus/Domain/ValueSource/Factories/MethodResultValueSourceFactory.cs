using System;

namespace Shuttle.Abacus.Domain
{
    public class MethodResultValueSourceFactory : IValueSourceFactory
    {
        private readonly IUnitOfWorkProvider unitOfWorkProvider;

        public MethodResultValueSourceFactory(IUnitOfWorkProvider unitOfWorkProvider)
        {
            this.unitOfWorkProvider = unitOfWorkProvider;
        }

        public string Name
        {
            get { return "MethodResult"; }
        }

        public string Text
        {
            get { return "Method Result"; }
        }

        public string Type
        {
            get { return "Selection"; }
        }

        public IValueSource Create(string value)
        {
            return new MethodResultValueSource(unitOfWorkProvider.Current.Get<Method>(new Guid(value)));
        }
    }
}
