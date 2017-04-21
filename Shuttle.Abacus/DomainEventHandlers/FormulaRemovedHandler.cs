using Abacus.Domain;

namespace Abacus.DomainEventHandlers
{
    public class FormulaRemovedHandler : IHandleEvent<FormulaRemoved>
    {
        private readonly IFormulaRepository repository;

        public FormulaRemovedHandler(IFormulaRepository repository)
        {
            this.repository = repository;
        }

        public void Handle(FormulaRemoved args)
        {
            repository.Remove(args.Formula);
            repository.SaveOrdered(args.Owner);
        }
    }
}
