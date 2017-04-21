using System;
using System.Collections.Generic;

namespace Shuttle.Abacus
{
    public class CreateDecimalTableCommand : ICreateDecimalTableCommand
    {
        public Guid DecimalTableId { get; set; }
        public string DecimalTableName { get; set; }
        public ArgumentDTO RowArgumentDto { get; set; }
        public ArgumentDTO ColumnArgumentDTO { get; set; }
        public List<DecimalValueDTO> DecimalValueDTOs { get; set; }
    }
}
