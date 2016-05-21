using System;
using System.Collections.Generic;

namespace Shuttle.Abacus
{
    public interface ICalculationOwner : IOwner
    {
        CalculationCollection Calculations { get; }

        void AssignCalculations(IEnumerable<Calculation> collection);

        CalculationCollection AddCalculation(Calculation calculation);
        ICalculationOwner FindOwner(Guid ownerId);
    }
}
