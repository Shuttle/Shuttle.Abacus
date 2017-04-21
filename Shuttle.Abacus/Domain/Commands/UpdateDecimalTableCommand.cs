using System;
using System.Collections.Generic;

namespace Shuttle.Abacus
{
    public class UpdateDecimalTableCommand : IUpdateDecimalTableCommand
    {
        public string DecimalTableName { get; set; }
        public ArgumentDTO RowArgumentDto { get; set; }
        public ArgumentDTO ColumnArgumentDTO { get; set; }
        public List<DecimalValueDTO> DecimalValueDTOs { get; set; }
        public Guid DecimalTableId { get; set; }
    }
}
