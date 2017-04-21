using Abacus.Domain;
using Shuttle.Abacus.Domain;

namespace Shuttle.Abacus.ApplicationService
{
    public class CalculationOwnerModel : OwnerModel
    {
        public CalculationOwnerModel(Method method, IEntity owner, Calculation calculation) : base(owner, calculation)
        {
            Method = method;
        }

        public Method Method { get; private set; }
    }
}
