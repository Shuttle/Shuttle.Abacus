using System.Linq;

namespace Shuttle.Abacus
{
    public class FormulaOwnerService : IFormulaOwnerService
    {
        public void ProcessCommand(IChangeFormulaOrderCommand command, IFormulaOwner owner)
        {
            if (owner.Formulas.Count() !=
                command.OrderedIds.Count)
            {
                throw new InvalidStateException(
                    "The formula collection has changed since you started the ordering.  Plase reload the formulas and try again.");
            }

            var result = new FormulaCollection();

            command.OrderedIds.ForEach(id=> result.Add(owner.Formulas.Get(id)));

            owner.AssignFormulas(result);

            //TODO: DomainEvents.Raise(new FormulaOrderChanged(owner));
        }
    }
}
