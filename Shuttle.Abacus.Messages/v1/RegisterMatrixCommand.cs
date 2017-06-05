using System;
using System.Collections.Generic;
using Shuttle.Abacus.Messages.v1.TransferObjects;

namespace Shuttle.Abacus.Messages.v1
{
    public class RegisterMatrixCommand
    {
        public Guid MatrixId { get; set; }
        public string Name { get; set; }
        public string RowArgumentName { get; set; }
        public string ColumnArgumentName { get; set; }
        public string ValueType { get; set; }

        public RegisterMatrixCommand()
        {
            Elements = new List<MatrixElement>();
            Constraints = new List<MatrixConstraint>();
        }

        public List<MatrixElement> Elements { get; set; }
        public List<MatrixConstraint> Constraints { get; set; }
    }
}