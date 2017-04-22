using Abacus.Domain;
using Shuttle.Abacus.Domain;

namespace Shuttle.Abacus.DomainEventHandlers
{
    public class CalculationAddedHandler : IHandleEvent<CalculationAdded>
    {
        private readonly ICalculationRepository repository;

        public CalculationAddedHandler(ICalculationRepository repository)
        {
            this.repository = repository;
        }

        public void Handle(CalculationAdded args)
        {
            repository.Add(args.Method, args.Owner, args.Calculation);

            repository.SaveOrdered(args.Method);
        }
    }
}
