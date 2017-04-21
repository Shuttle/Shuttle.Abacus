using System;

namespace Shuttle.Abacus.Domain
{
    public class DecimalTableValueSourceFactory : IValueSourceFactory
    {
        private readonly IUnitOfWorkProvider unitOfWorkProvider;

        public DecimalTableValueSourceFactory(IUnitOfWorkProvider unitOfWorkProvider)
        {
            this.unitOfWorkProvider = unitOfWorkProvider;
        }

        public string Name
        {
            get { return "DecimalTable"; }
        }

        public string Text
        {
            get { return "Decimal Table"; }
        }

        public string Type
        {
            get { return "Selection"; }
        }

        public IValueSource Create(string value)
        {
            return new DecimalTableValueSource(unitOfWorkProvider.Current.Get<DecimalTable>(new Guid(value)));
        }
    }
}
