using System;

namespace Shuttle.Abacus.Domain
{
    public class MatrixElement
    {
        public MatrixElement(Guid id, int column, int row, decimal value, string rowComparison, string rowValue)
        {
            Id = id;
            Row = row;
            Column = column;
            Value = value;
            RowComparison = rowComparison;
            RowValue = rowValue;
        }

        public Guid Id { get; }
        public int Row { get; }
        public int Column { get; }
        public decimal Value { get; }
        public string RowComparison { get; }
        public string RowValue { get; }

        //                          ? "where "
        //        result.Append(result.Length == 0
        //    {

        //    foreach (var constraint in constraints)
        //    var result = new StringBuilder();
        //{

        //public string Description(IMethodContext collectionContext)
        //                          : " and ");

        //        result.Append(constraint.Description());
        //    }

        //    return result.ToString();
        //}

        //public MatrixElement Copy()
        //{
        //    var result = new MatrixElement(Guid.NewGuid(), Column, Row, Value);

        //    constraints.ForEach(constraint => result.AddConstraint(constraint.Copy()));

        //    return result;
        //}
    }
}