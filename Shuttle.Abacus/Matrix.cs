using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Shuttle.Abacus.Events.Matrix.v1;
using Shuttle.Core.Contract;

namespace Shuttle.Abacus
{
    public class Matrix
    {
        private readonly List<Constraint> _constraints = new List<Constraint>();
        private readonly List<Element> _elements = new List<Element>();

        public Matrix(Guid id)
        {
            Id = id;
        }

        public string DataTypeName { get; private set; }

        public Guid Id { get; }

        public string Name { get; private set; }

        public IEnumerable<Element> Elements => new ReadOnlyCollection<Element>(_elements);
        public IEnumerable<Constraint> Constraints => new ReadOnlyCollection<Constraint>(_constraints);

        public Guid RowArgumentId { get; private set; }
        public Guid? ColumnArgumentId { get; private set; }

        public Registered Register(string name, Guid rowArgumentId, Guid? columnArgumentId, string dataTypeName)
        {
            Guard.AgainstNullOrEmptyString(name, nameof(name));

            return On(new Registered
            {
                Name = name,
                RowArgumentId = rowArgumentId,
                ColumnArgumentId = columnArgumentId,
                DataTypeName = dataTypeName
            });
        }

        private Registered On(Registered registered)
        {
            Guard.AgainstNull(registered, nameof(registered));

            Name = registered.Name;
            RowArgumentId = registered.RowArgumentId;
            ColumnArgumentId = registered.ColumnArgumentId;
            DataTypeName = registered.DataTypeName;

            _constraints.Clear();
            _elements.Clear();

            return registered;
        }

        public static string Key(string name)
        {
            return $"[matrix]:name={name}";
        }

        public ElementAdded AddElement(int row, int column, string value)
        {
            if (HasElement(row, column))
            {
                throw new DomainException($"There is already a value for row '{row}' and column '{column}'.");
            }

            return On(new ElementAdded
            {
                Row = row,
                Column = column,
                Value = value
            });
        }

        private ElementAdded On(ElementAdded elementAdded)
        {
            Guard.AgainstNull(elementAdded, nameof(elementAdded));

            _elements.Add(new Element(elementAdded.Row, elementAdded.Column, elementAdded.Value));

            return elementAdded;
        }

        public bool HasElement(int row, int column)
        {
            return _elements.Find(item => item.Row == row && item.Column == column) != null;
        }

        public bool IsNamed(string name)
        {
            return Name.Equals(name, StringComparison.InvariantCultureIgnoreCase);
        }

        public ConstraintAdded AddConstraint(string axis, int sequenceNumber, string comparison, string value)
        {
            Guard.AgainstNullOrEmptyString(axis, nameof(axis));
            Guard.AgainstNullOrEmptyString(comparison, nameof(comparison));

            if (HasConstraint(axis, sequenceNumber))
            {
                throw new DomainException(
                    $"There is already a constraint for axis '{axis}' and sequence number '{sequenceNumber}'.");
            }

            return On(new ConstraintAdded
            {
                Axis = axis,
                SequenceNumber = sequenceNumber,
                Comparison = comparison,
                Value = value
            });
        }

        private ConstraintAdded On(ConstraintAdded constraintAdded)
        {
            Guard.AgainstNull(constraintAdded, nameof(constraintAdded));

            _constraints.Add(new Constraint(constraintAdded.Axis, constraintAdded.SequenceNumber,
                constraintAdded.Comparison, constraintAdded.Value));

            return constraintAdded;
        }

        public bool HasConstraint(string axis, int sequenceNumber)
        {
            return
                _constraints.Exists(
                    item =>
                        item.Axis.Equals(axis, StringComparison.InvariantCultureIgnoreCase) &&
                        item.SequenceNumber == sequenceNumber);
        }

        public string GetValue(IConstraintComparison constraintComparison, ExecutionContext executionContext,
            Argument rowArgument, Argument columnArgument)
        {
            Guard.AgainstNull(constraintComparison, nameof(constraintComparison));
            Guard.AgainstNull(executionContext, nameof(executionContext));
            Guard.AgainstNull(rowArgument, nameof(rowArgument));

            if (ColumnArgumentId.HasValue)
            {
                Guard.AgainstNull(columnArgument, nameof(columnArgument));
            }

            var row = FindConstraint("Row", constraintComparison, rowArgument.DataType,
                executionContext.GetArgumentValue(RowArgumentId));
            var column = ColumnArgumentId.HasValue
                ? FindConstraint("Column", constraintComparison, columnArgument.DataType,
                    executionContext.GetArgumentValue(ColumnArgumentId.Value))
                : 1;

            var element = _elements.FirstOrDefault(item => item.Row == row && item.Column == column);

            if (element == null)
            {
                throw new InvalidOperationException(
                    $"Could not an element for matrix '{Name}' at intersection of row '{row}' and column '{column}'.");
            }

            return element.Value;
        }

        private int FindConstraint(string axis, IConstraintComparison constraintComparison, string dataTypeName, string value)
        {
            var constraint = _constraints.FirstOrDefault(item =>
                item.Axis.Equals(axis, StringComparison.InvariantCultureIgnoreCase)
                &&
                constraintComparison.IsSatisfiedBy(dataTypeName, item.Value, item.Comparison, value)
            );

            if (constraint == null)
            {
                throw new InvalidOperationException(
                    $"There is no {axis.ToLower()} constraint in matrix '{Name}' where argument '{RowArgumentId}' is satisfied by '{value}'.");
            }

            return constraint.SequenceNumber;
        }

        public class Constraint
        {
            public Constraint(string axis, int sequenceNumber, string comparison, string value)
            {
                if (!axis.Equals("Row", StringComparison.InvariantCultureIgnoreCase)
                    &&
                    !axis.Equals("Column", StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new DomainException("Axis may only be 'Row' or 'Column'.");
                }

                Axis = axis;
                SequenceNumber = sequenceNumber;
                Comparison = comparison;
                Value = value;
            }

            public string Axis { get; }
            public int SequenceNumber { get; }
            public string Comparison { get; }
            public string Value { get; }
        }

        public class Element
        {
            public Element(int row, int column, string value)
            {
                Row = row;
                Column = column;
                Value = value;
            }

            public int Row { get; }
            public int Column { get; }
            public string Value { get; }
        }
    }
}