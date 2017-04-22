using System.Linq;
using Shuttle.Abacus.Infrastructure;

namespace Shuttle.Abacus.Domain
{
    public class FormulaOwnerService : IFormulaOwnerService
    {
        //TODO: HandleEvent
        public FormulaOrderChanged ProcessCommand(ChangeFormulaOrderCommand command, IFormulaOwner owner)
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

            return new FormulaOrderChanged(owner);
        }
    }
}
