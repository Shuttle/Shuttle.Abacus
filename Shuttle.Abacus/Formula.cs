using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Shuttle.Abacus.Events.Formula.v1;
using Shuttle.Core.Contract;

namespace Shuttle.Abacus
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

        public bool HasOperations => _operations.Count > 0;
        public bool Removed { get; private set; }

        public OperationAdded AddOperation(Guid id, string operation, string valueProviderName, string inputParameter)
        {
            Guard.AgainstNullOrEmptyString(operation, nameof(operation));
            Guard.AgainstNullOrEmptyString(valueProviderName, nameof(valueProviderName));
            Guard.AgainstNullOrEmptyString(inputParameter, nameof(inputParameter));

            return On(new OperationAdded
            {
                Id = id,
                SequenceNumber = _operations.Count + 1,
                Operation = operation,
                ValueProviderName = valueProviderName,
                InputParameter = inputParameter
            });
        }

        private OperationAdded On(OperationAdded operationAdded)
        {
            Guard.AgainstNull(operationAdded, nameof(operationAdded));

            _operations.Add(
                new FormulaOperation(
                    operationAdded.Id, 
                    operationAdded.SequenceNumber,
                    operationAdded.Operation,
                    operationAdded.ValueProviderName,
                    operationAdded.InputParameter));

            return operationAdded;
        }

        public ConstraintAdded AddConstraint(Guid id, string argumentName, string comparison, string value)
        {
            Guard.AgainstNullOrEmptyString(argumentName, nameof(argumentName));
            Guard.AgainstNullOrEmptyString(comparison, nameof(comparison));
            Guard.AgainstNullOrEmptyString(value, nameof(value));

            return On(new ConstraintAdded
            {
                Id = id,
                ArgumentName = argumentName,
                Comparison = comparison,
                Value = value
            });
        }

        private ConstraintAdded On(ConstraintAdded constraintAdded)
        {
            Guard.AgainstNull(constraintAdded, nameof(constraintAdded));

            _constraints.Add(
                new FormulaConstraint(
                    constraintAdded.Id,
                    constraintAdded.ArgumentName,
                    constraintAdded.Comparison,
                    constraintAdded.Value));

            return constraintAdded;
        }


        public Registered Register(string name)
        {
            Guard.AgainstNullOrEmptyString(name, nameof(name));

            return On(new Registered
            {
                Name = name
            });
        }

        private Registered On(Registered registered)
        {
            Guard.AgainstNull(registered, nameof(registered));

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
            Guard.AgainstNull(removed, nameof(removed));

            Removed = true;

            return removed;
        }

        public bool IsNamed(string name)
        {
            Guard.AgainstNullOrEmptyString(name, nameof(name));

            return Name.Equals(name, StringComparison.InvariantCultureIgnoreCase);
        }

        public Renamed Rename(string name)
        {
            Guard.AgainstNullOrEmptyString(name, nameof(name));

            if (IsNamed(name))
            {
                throw new DomainException($"Already named '{name}'.");
            }

            return On(new Renamed
            {
                Name = name
            });
        }

        private Renamed On(Renamed renamed)
        {
            Guard.AgainstNull(renamed, nameof(renamed));

            Name = renamed.Name;

            return renamed;
        }

        public static string Key(string name)
        {
            return $"[formula]:name={name}";
        }

        public OperationRemoved RemoveOperation(Guid id)
        {
            return On(new OperationRemoved
            {
                Id = id
            });
        }

        private OperationRemoved On(OperationRemoved operationRemoved)
        {
            Guard.AgainstNull(operationRemoved, nameof(operationRemoved));

            _operations.RemoveAll(item => item.Id.Equals(operationRemoved.Id));

            return operationRemoved;
        }

        public ConstraintRemoved RemoveConstraint(Guid id)
        {
            return On(new ConstraintRemoved
            {
                Id = id
            });
        }

        private ConstraintRemoved On(ConstraintRemoved constraintRemoved)
        {
            Guard.AgainstNull(constraintRemoved, nameof(constraintRemoved));

            _constraints.RemoveAll(item => item.Id.Equals(constraintRemoved.Id));

            return constraintRemoved;
        }
    }
}