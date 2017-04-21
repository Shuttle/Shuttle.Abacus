using System;

namespace Shuttle.Abacus.Domain
{
    public interface ICalculationValueSource : IValueSelectionHolder
    {
        void AssignCalculationId(Guid id);
    }
}
