using System.Collections.Generic;

namespace Shuttle.Abacus
{
    public interface IDecimalTableCommand
    {
        string DecimalTableName { get; set; }
        ArgumentDTO RowArgumentDto { get; set; }
        ArgumentDTO ColumnArgumentDTO { get; set; }
        List<DecimalValueDTO> DecimalValueDTOs { get; set; }
    }
}
