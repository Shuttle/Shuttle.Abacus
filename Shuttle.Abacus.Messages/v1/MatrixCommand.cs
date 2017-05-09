using System;
using System.Collections.Generic;
using Shuttle.Abacus.Messages.v1.TransferObjects;

namespace Shuttle.Abacus.Messages.v1
{
    public class MatrixCommand
    {
        public Guid MatrixId { get; set; }
        public string DecimalTableName { get; set; }
        public string RowArgumentName { get; set; }
        public string ColumnArgumentName { get; set; }
        public List<MatrixElement> Elements { get; set; }
    }
}