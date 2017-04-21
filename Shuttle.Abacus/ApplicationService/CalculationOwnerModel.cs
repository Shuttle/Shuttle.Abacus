using Abacus.Domain;

namespace Abacus.Application
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
