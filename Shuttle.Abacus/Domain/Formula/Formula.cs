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

        //public bool IsSatisfiedBy(IMethodContext collectionMethodContext)
        //{
        //    return
        //        OperationsSatisfied(collectionMethodContext)
        //        &&
        //        ConstraintSatisfied(collectionMethodContext);
        //}

        //public IEnumerable<Guid> RequiredCalculationIds()
        //{
        //    var result = new List<Guid>();

        //    _operations.ForEach(operation =>
        //    {
        //        var source = operation.ValueSource as ICalculationValueSource;

        //        if (source != null)
        //        {
        //            var id = new Guid(source.ValueSelection);

        //            if (!result.Contains(id))
        //            {
        //                result.Add(id);
        //            }
        //        }
        //    });

        //    return result;
        //}

        //private bool ConstraintSatisfied(IMethodContext collectionContext)
        //{
        //    foreach (var constraint in constraints)
        //    {
        //        if (!constraint.IsSatisfiedBy(collectionContext))
        //        {
        //            return false;
        //        }
        //    }

        //    return true;
        //}

        //private bool OperationsSatisfied(IMethodContext collectionContext)
        //{
        //    var result = true;

        //    //foreach (var operation in _operations)
        //    //{
        //    //    if (!operation.IsSatisfiedBy(collectionContext))
        //    //    {
        //    //        result = false;
        //    //    }
        //    //}

        //    return result;
        //}

        public OperationAdded AddOperation(int sequenceNumber, string operation, string valueSource, string valueSelection)
        {
            return On(new OperationAdded {
                SequenceNumber= sequenceNumber,
                Operation= operation,
                ValueSource= valueSource,
                ValueSelection = valueSelection});
        }

        public OperationAdded On(OperationAdded operationAdded)
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

        public ConstraintAdded On(ConstraintAdded constraintAdded)
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

        //public decimal Execute(IMethodContext methodContext, IFormulaCalculationContext calculationContext)
        //{
        //    Guard.AgainstNull(calculationContext, "context");

        //    throw new NotImplementedException();

        //    //if (methodContext.LoggerEnabled)
        //    //{
        //    //    methodContext.Log("Executing formula:");

        //    //    if (constraints.Count > 0)
        //    //    {
        //    //        constraints.ForEach(
        //    //            constraint => methodContext.Log("\t{0}", constraint.Description()));
        //    //    }
        //    //    else
        //    //    {
        //    //        methodContext.Log("\t(no contraints)");
        //    //    }

        //    //    methodContext.Log();
        //    //}

        //    //calculationContext.ZeroFormulaTotal();

        //    //foreach (var operation in _operations)
        //    //{
        //    //    var operand = operation.Operand(methodContext, calculationContext);

        //    //    if (methodContext.LoggerEnabled)
        //    //    {
        //    //        methodContext.Log("{0}\t{1}", operation.Symbol,
        //    //            operation.ValueSource.Description(operand, methodContext));
        //    //    }

        //    //    calculationContext.SetFormulaTotal(operation.Execute(calculationContext.RunningTotal, operand));

        //    //    if (!methodContext.OK)
        //    //    {
        //    //        return 0;
        //    //    }
        //    //}

        //    //if (methodContext.LoggerEnabled)
        //    //{
        //    //    methodContext.Log();
        //    //}

        //    //return calculationContext.RunningTotal;
        //}

        public Formula Copy()
        {
            throw new NotImplementedException();
            //var result = new Formula();

            //_constraints.ForEach(constraint => result.AddConstraint(constraint));
            //_operations.ForEach(operation => result.AddOperation(operation));

            //return result;
        }

        public Registered Register(string name)
        {
            Guard.AgainstNullOrEmptyString(name, "name");

            return On(new Registered
            {
                Name = name
            });
        }

        public Registered On(Registered registered)
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

        public Removed On(Removed removed)
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

        public Renamed On(Renamed renamed)
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

        public OperationsRemoved On(OperationsRemoved operationsRemoved)
        {
            Guard.AgainstNull(operationsRemoved, "operationsRemoved");

            _operations.Clear();

            return operationsRemoved;
        }

        public ConstraintsRemoved RemoveConstraints()
        {
            return On(new ConstraintsRemoved());
        }

        public ConstraintsRemoved On(ConstraintsRemoved constraintsRemoved)
        {
            Guard.AgainstNull(constraintsRemoved, "constraintsRemoved");

            _constraints.Clear();

            return constraintsRemoved;
        }

        public bool IsSatisfiedBy(ExecutionContext executionContext)
        {
            Guard.AgainstNull(executionContext, "executionContext");

            foreach (var constraint in _constraints)
            {
                if (!constraint.IsSatisfiedBy(executionContext))
                {
                    return false;
                }
            }

            return true;
        }
    }
}