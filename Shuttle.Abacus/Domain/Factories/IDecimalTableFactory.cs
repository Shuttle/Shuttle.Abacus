using System;

namespace Shuttle.Abacus.Domain
{
    public interface IDecimalTableFactory
    {
        DecimalTable Create(Guid decimalTableId, DecimalTableCommand command);
    }
}
