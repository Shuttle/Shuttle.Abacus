using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Shuttle.Abacus.Events.Formula.v1;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Domain
{
    public class Formula
    {
        private readonly List<FormulaConstraint> _constraints = new List<FormulaConstraint>();
        private readonly List<FormulaOperation> _operations = new List<FormulaOperation>();

        public Formula(Guid id)
        {
            Id = id;
        }

        public IEnumerable<FormulaOperation> Operations => new ReadOnlyCollection<FormulaOperation>(_operations);

        public Guid Id { get; }
        public string Name { get; set; }
        public string MaximumFormulaName { get; private set; }
        public string MinimumFormulaName { get; private set; }

        public IEnumerable<FormulaConstraint> Constraints => new ReadOnlyCollection<FormulaConstraint>(_constraints);

        public string OwnerName => "Formula";
        public bool HasOperations => _operations.Count > 0;
        public bool Removed { get; private set; }

        public OperationAdded AddOperation(int sequenceNumber, string operation, string valueSource, string valueSelection)
        {
            return On(new OperationAdded {
                SequenceNumber= sequenceNumber,
                Operation= operation,
                ValueSource= valueSource,
                ValueSelection = valueSelection});
        }

        private OperationAdded On(OperationAdded operationAdded)
        {
            Guard.AgainstNull(operationAdded, "operationAdded");

            _operations.Add(
                new FormulaOperation(
                    operationAdded.SequenceNumber,
                    operationAdded.Operation,
                    operationAdded.ValueSource,
                    operationAdded.ValueSelection));

            return operationAdded;
        }

        public ConstraintAdded AddConstraint(int sequenceNumber, string argumentName, string comparison, string value)
        {
            return On(new ConstraintAdded {
                SequenceNumber= sequenceNumber,
                ArgumentName = argumentName,
                Comparison= comparison,
                Value = value});
        }

        private ConstraintAdded On(ConstraintAdded constraintAdded)
        {
            Guard.AgainstNull(constraintAdded, "constraintAdded");

            _constraints.Add(
                new FormulaConstraint(
                    constraintAdded.SequenceNumber,
                    constraintAdded.ArgumentName,
                    constraintAdded.Comparison,
                    constraintAdded.Value));

            return constraintAdded;
        }


        public Registered Register(string name)
        {
            Guard.AgainstNullOrEmptyString(name, "name");

            return On(new Registered
            {
                Name = name
            });
        }

        private Registered On(Registered registered)
        {
            Guard.AgainstNull(registered, "registered");

            Name = registered.Name;

            return registered;
        }

        public Removed Remove()
        {
            if (Removed)
            {
                throw new DomainException("Already removed.");
            }

            return On(new Removed());
        }

        private Removed On(Removed removed)
        {
            Guard.AgainstNull(removed, "removed");

            Removed = true;

            return removed;
        }

        public bool IsNamed(string name)
        {
            Guard.AgainstNullOrEmptyString(name, "name");

            return Name.Equals(name, StringComparison.InvariantCultureIgnoreCase);
        }

        public Renamed Rename(string name)
        {
            Guard.AgainstNullOrEmptyString(name, "name");

            if (IsNamed(name))
            {
                throw new DomainException(string.Format("Already named '{0}'.", name));
            }

            return On(new Renamed
            {
                Name = name
            });
        }

        private Renamed On(Renamed renamed)
        {
            Guard.AgainstNull(renamed, "renamed");

            Name = renamed.Name;

            return renamed;
        }

        public static string Key(string name)
        {
            return string.Format("[formula]:name={0}", name);
        }

        public OperationsRemoved RemoveOperations()
        {
            return On(new OperationsRemoved());
        }

        private OperationsRemoved On(OperationsRemoved operationsRemoved)
        {
            Guard.AgainstNull(operationsRemoved, "operationsRemoved");

            _operations.Clear();

            return operationsRemoved;
        }

        public ConstraintsRemoved RemoveConstraints()
        {
            return On(new ConstraintsRemoved());
        }

        private ConstraintsRemoved On(ConstraintsRemoved constraintsRemoved)
        {
            Guard.AgainstNull(constraintsRemoved, "constraintsRemoved");

            _constraints.Clear();

            return constraintsRemoved;
        }
    }
}