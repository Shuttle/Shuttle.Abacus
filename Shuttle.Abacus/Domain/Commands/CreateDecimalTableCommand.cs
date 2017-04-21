using System;
using System.Collections.Generic;
using Shuttle.Abacus.DTO;

namespace Shuttle.Abacus.Domain
{
    public class CreateDecimalTableCommand
    {
        public Guid DecimalTableId { get; set; }
        public string DecimalTableName { get; set; }
        public ArgumentDTO RowArgumentDto { get; set; }
        public ArgumentDTO ColumnArgumentDTO { get; set; }
        public List<DecimalValueDTO> DecimalValueDTOs { get; set; }
    }
}
