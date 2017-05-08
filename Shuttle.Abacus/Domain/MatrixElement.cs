using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Domain
{
    public class MatrixElement :
        IConstraintOwner,
        ISpecification<IMethodContext>
    {
        private readonly List<IConstraint> constraints = new List<IConstraint>();
        private readonly List<OwnedConstraint> _constraints = new List<OwnedConstraint>();

        public MatrixElement(decimal value)
            : this(Guid.NewGuid(), -1, -1, value)
        {
        }

        public MatrixElement(Guid id, int column, int row, decimal value)
        {
            Id = id;
            Row = row;
            Column = column;
            Value = value;
        }

        public Guid Id { get; }
        public int Row { get; private set; }
        public int Column { get; private set; }
        public decimal Value { get; private set; }

        public string OwnerName
        {
            get { return "MatrixElement"; }
        }

        public IEnumerable<OwnedConstraint> Constraints
        {
            get { return new ReadOnlyCollection<OwnedConstraint>(_constraints); }
        }

        public IConstraintOwner AddConstraint(IConstraint constraint)
        {
            Guard.AgainstNull(constraint, "constraint");

            constraints.Add(constraint);

            return this;
        }

        public void AddConstraint(OwnedConstraint item)
        {
            Guard.AgainstNull(item, "item");

            _constraints.Add(item);
        }

        public bool IsSatisfiedBy(IMethodContext collectionMethodContext)
        {
            foreach (var constraint in constraints)
            {
                if (!constraint.IsSatisfiedBy(collectionMethodContext))
                {
                    return false;
                }
            }

            return true;
        }

        public string Description(IMethodContext collectionContext)
        {
            var result = new StringBuilder();

            foreach (var constraint in constraints)
            {
                result.Append(result.Length == 0
                                  ? "where "
                                  : " and ");

                result.Append(constraint.Description());
            }

            return result.ToString();
        }

        public MatrixElement Copy()
        {
            var result = new MatrixElement(Guid.NewGuid(), Column, Row, Value);

            constraints.ForEach(constraint => result.AddConstraint(constraint.Copy()));

            return result;
        }
    }
}
