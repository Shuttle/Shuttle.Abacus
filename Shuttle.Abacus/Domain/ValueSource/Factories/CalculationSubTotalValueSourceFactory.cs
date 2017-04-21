using System;

namespace Shuttle.Abacus.Domain
{
    public class CalculationSubTotalValueSourceFactory : IValueSourceFactory
    {
        private readonly IUnitOfWorkProvider unitOfWorkProvider;

        public CalculationSubTotalValueSourceFactory(IUnitOfWorkProvider unitOfWorkProvider)
        {
            this.unitOfWorkProvider = unitOfWorkProvider;
        }

        public string Name
        {
            get { return "CalculationSubTotal"; }
        }

        public string Text
        {
            get { return "Calculation Sub-Total"; }
        }

        public string Type
        {
            get { return "Selection"; }
        }

        public IValueSource Create(string value)
        {
            return new CalculationSubTotalValueSource(unitOfWorkProvider.Current.Get<Calculation>(new Guid(value)));
        }
    }
}
