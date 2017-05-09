using System;
using System.Collections.Generic;
using Shuttle.Abacus.Messages.v1.TransferObjects;

namespace Shuttle.Abacus.Messages.v1
{
    public class DecimalTableCommand
    {
        public Guid MatrixId { get; set; }
        public string DecimalTableName { get; set; }
        public Guid RowArgumentId { get; set; }
        public Guid ColumnArgumentId { get; set; }
        public List<DecimalValue> DecimalValues { get; set; }
    }
}