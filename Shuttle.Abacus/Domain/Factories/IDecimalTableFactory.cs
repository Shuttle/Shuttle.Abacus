using System;

namespace Shuttle.Abacus
{
    public interface IDecimalTableFactory
    {
        DecimalTable Create(Guid decimalTableId, IDecimalTableCommand command);
    }
}
