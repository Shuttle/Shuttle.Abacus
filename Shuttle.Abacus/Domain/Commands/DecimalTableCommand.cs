using System;
using System.Collections.Generic;
using Shuttle.Abacus.DTO;

namespace Shuttle.Abacus.Domain
{
    public class DecimalTableCommand
    {
        public Guid DecimalTableId { get; set; }
        public string DecimalTableName { get; set; }
        public Guid RowArgumentId { get; set; }
        public Guid ColumnArgumentId { get; set; }
        public List<DecimalValueDTO> DecimalValueDTOs { get; set; }
    }
}