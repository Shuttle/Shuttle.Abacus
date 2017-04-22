using System;

namespace Shuttle.Abacus.Domain
{
    public class MethodResultValueSourceFactory : IValueSourceFactory
    {
        private readonly IMethodRepository _methodRepository;

        public MethodResultValueSourceFactory(IMethodRepository methodRepository)
        {
            _methodRepository = methodRepository;
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
            return new MethodResultValueSource(_methodRepository.Get(new Guid(value)));
        }
    }
}
