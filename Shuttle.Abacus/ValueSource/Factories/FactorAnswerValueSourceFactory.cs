using System;

namespace Shuttle.Abacus
{
    public class ArgumentAnswerValueSourceFactory : IValueSourceFactory
    {
        private readonly IUnitOfWorkProvider unitOfWorkProvider;

        public ArgumentAnswerValueSourceFactory(IUnitOfWorkProvider unitOfWorkProvider)
        {
            this.unitOfWorkProvider = unitOfWorkProvider;
        }

        public string Name
        {
            get { return "ArgumentAnswer"; }
        }

        public string Text
        {
            get { return "Argument Answer"; }
        }

        public string Type
        {
            get { return "Selection"; }
        }

        public IValueSource Create(string value)
        {
            return new ArgumentAnswerValueSource(unitOfWorkProvider.Current.Get<Argument>(new Guid(value)));
        }
    }
}
