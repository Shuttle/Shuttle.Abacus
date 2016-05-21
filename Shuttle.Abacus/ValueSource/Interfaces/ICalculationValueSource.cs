using System;

namespace Shuttle.Abacus
{
    public interface ICalculationValueSource : IValueSelectionHolder
    {
        void AssignCalculationId(Guid id);
    }
}
