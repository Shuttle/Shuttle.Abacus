using System;

namespace Shuttle.Abacus
{
    public class CalculationResultValueSourceFactory : IValueSourceFactory
    {
        private readonly IUnitOfWorkProvider unitOfWorkProvider;

        public CalculationResultValueSourceFactory(IUnitOfWorkProvider unitOfWorkProvider)
        {
            this.unitOfWorkProvider = unitOfWorkProvider;
        }

        public string Name
        {
            get { return "CalculationResult"; }
        }

        public string Text
        {
            get { return "Calculation Result"; }
        }

        public string Type
        {
            get { return "Selection"; }
        }

        public IValueSource Create(string value)
        {
            return new CalculationResultValueSource(unitOfWorkProvider.Current.Get<Calculation>(new Guid(value)));
        }
    }
}
