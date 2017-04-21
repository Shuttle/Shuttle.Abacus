using System.Collections.Generic;
using Abacus.DTO;

namespace Abacus.CommandMediators
{
    public interface IDecimalTableCommand
    {
        string DecimalTableName { get; set; }
        ArgumentDTO RowArgumentDto { get; set; }
        ArgumentDTO ColumnArgumentDTO { get; set; }
        List<DecimalValueDTO> DecimalValueDTOs { get; set; }
    }
}
