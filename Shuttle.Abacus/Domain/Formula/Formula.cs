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

        public void AddConstraint(FormulaConstraint item)
        {
            Guard.AgainstNull(item, "item");

            _constraints.Add(item);
        }

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

        public Formula AddOperation(FormulaOperation operation)
        {
            _operations.Add(operation);

            return this;
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

        //    //    calculationContext.SetFormulaTotal(operation.Execute(calculationContext.FormulaTotal, operand));

        //    //    if (!methodContext.OK)
        //    //    {
        //    //        return 0;
        //    //    }
        //    //}

        //    //if (methodContext.LoggerEnabled)
        //    //{
        //    //    methodContext.Log();
        //    //}

        //    //return calculationContext.FormulaTotal;
        //}

        public Formula Copy()
        {
            throw new NotImplementedException();
            //var result = new Formula();

            //_constraints.ForEach(constraint => result.AddConstraint(constraint));
            //_operations.ForEach(operation => result.AddOperation(operation));

            //return result;
        }

        public Registered Register(string name, string maximumFormulaName, string minimumFormulaName)
        {
            Guard.AgainstNullOrEmptyString(name, "name");

            return On(new Registered
            {
                Name = name,
                MinimumFormulaName = minimumFormulaName ?? string.Empty,
                MaximumFormulaName = maximumFormulaName ?? string.Empty
            });
        }

        public Registered On(Registered registered)
        {
            Guard.AgainstNull(registered, "registered");

            Name = registered.Name;
            MinimumFormulaName = registered.MinimumFormulaName;
            MaximumFormulaName = registered.MaximumFormulaName;

            return registered;
        }

        public Removed Remove()
        {
            return On(new Removed());
        }

        public Removed On(Removed removed)
        {
            Guard.AgainstNull(removed, "removed");

            Removed = true;

            return removed;
        }
    }
}