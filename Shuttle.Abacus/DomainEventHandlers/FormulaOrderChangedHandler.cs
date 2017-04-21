using Abacus.Domain;

namespace Abacus.DomainEventHandlers
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
