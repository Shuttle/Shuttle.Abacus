using Abacus.Domain;
using Shuttle.Abacus.Domain;

namespace Shuttle.Abacus.DomainEventHandlers
{
    public class FormulaOrderChangedHandler : IHandleEvent<FormulaOrderChanged>
    {
        private readonly IFormulaRepository repository;

        public FormulaOrderChangedHandler(IFormulaRepository repository)
        {
            this.repository = repository;
        }

        public void Handle(FormulaOrderChanged args)
        {
            repository.SaveOrdered(args.Owner);
        }
    }
}
