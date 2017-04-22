using System;
using System.Collections;
using System.Collections.Generic;
using Shuttle.Abacus.DataAccess;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Domain
{
    public class CalculationCollection :
        Calculation,
        ICalculationOwner,
        IEnumerable<Calculation>
    {
        protected List<Calculation> calculations = new List<Calculation>();

        public CalculationCollection()
            : this(Guid.NewGuid(), null)
        {
        }

        public CalculationCollection(string name)
            : this(Guid.NewGuid(), name)
        {
        }

        public CalculationCollection(Guid id, string name)
            : base(name, false)
        {
            Id = id;
        }

        public override string Type
        {
            get { return "Collection"; }
        }

        public CalculationCollection Calculations
        {
            get { return this; }
        }


        public void AssignCalculations(IEnumerable<Calculation> collection)
        {
            calculations = new List<Calculation>(collection);
        }

        public CalculationCollection AddCalculation(Calculation calculation)
        {
            Guard.AgainstNull(calculation, "calculation");

            calculations.Add(calculation);

            return this;
        }

        public ICalculationOwner FindOwner(Guid ownerId)
        {
            if (Id.Equals(ownerId))
            {
                return this;
            }

            foreach (var calculation in calculations)
            {
                var collection = calculation as ICalculationOwner;

                if (collection == null)
                {
                    continue;
                }

                var result = collection.FindOwner(ownerId);

                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

        public IEnumerator<Calculation> GetEnumerator()
        {
            return calculations.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool ContainsName(string name, Guid ignore)
        {
            var result = calculations.Find(item => item.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));

            if (result != null)
            {
                return !ignore.Equals(result.Id);
            }

            foreach (var calculation in calculations)
            {
                var collection = calculation as CalculationCollection;

                if (collection != null)
                {
                    return collection.ContainsName(name, ignore);
                }
            }

            return false;
        }

        public override ICalculationResult Execute(IMethodContext methodContext,
            ICalculationContext calculationContext)
        {
            Guard.AgainstNull(methodContext, "methodContext");

            if (methodContext.LoggerEnabled)
            {
                methodContext.Log("Starting calculation: '{0}'", Name);
            }

            var total = new CalculationCollectionResult(this, 0);

            foreach (var calculation in calculations)
            {
                using (
                    var context =
                        calculation.CalculationContext(methodContext)
                            .AssignGraphNode(calculationContext.GraphNode.AddGraphNode(calculation.Name)))
                {
                    context.GraphNode.AddGraphNodeArguments(GraphNodeArguments);

                    var calculationResult = (AbstractCalculationResult) calculation.Execute(methodContext, context);
                    var subTotalCalculationResult = methodContext.GetSubTotal(calculation.Name);

                    context.PopulateGraphNode(calculationResult.Value, subTotalCalculationResult.Value);

                    total.Add(calculationResult);

                    if (!methodContext.OK)
                    {
                        return new CalculationCollectionResult(this);
                    }

                    if (!methodContext.LoggerEnabled)
                    {
                        continue;
                    }

                    methodContext.Log("{0}", total.Description());
                    methodContext.Log();
                }
            }

            ApplyLimits(methodContext, total);

            if (methodContext.LoggerEnabled)
            {
                methodContext.Log("Total for calculation: '{0}' = {1}", Name, methodContext.Total.Description());
                methodContext.Log();
            }

            methodContext.AddResult(total);

            return total;
        }

        public override Calculation Copy(IDictionary<Guid, Guid> idMap)
        {
            var result = new CalculationCollection(Name);

            Copy(result);

            calculations.ForEach(calculation =>
            {
                var copy = calculation.Copy(idMap);

                idMap.Add(calculation.Id, copy.Id);

                var formulaCalculation = copy as FormulaCalculation;

                if (formulaCalculation != null)
                {
                    foreach (var formula in formulaCalculation.Formulas)
                    {
                        foreach (var operation in formula.Operations)
                        {
                            var calculationValueSource =
                                operation.ValueSource as ICalculationValueSource;

                            if (calculationValueSource != null)
                            {
                                calculationValueSource
                                    .AssignCalculationId(
                                        idMap[new Guid(calculationValueSource.ValueSelection)]);
                            }
                        }
                    }
                }

                result.AddCalculation(copy);
            });

            return result;
        }

        public override ICalculationContext CalculationContext(IMethodContext methodContext)
        {
            return new CalculationCollectionContext(methodContext);
        }

        public bool Contains(Calculation calculation)
        {
            foreach (var item in calculations)
            {
                if (item.Id.Equals(calculation.Id))
                {
                    return true;
                }
            }

            return false;
        }

        public bool HierarchyContains(Calculation calculation)
        {
            return HierarchyContains(calculation.Id);
        }

        public bool HierarchyContains(Guid calculationId)
        {
            foreach (var item in calculations)
            {
                if (item.Id.Equals(calculationId))
                {
                    return true;
                }

                var collection = item as ICalculationOwner;

                if (collection == null)
                {
                    continue;
                }

                if (collection.Calculations.HierarchyContains(calculationId))
                {
                    return true;
                }
            }

            return false;
        }

        public CalculationCollection Flattened()
        {
            return CollectCalculations(new CalculationCollection());
        }

        private CalculationCollection CollectCalculations(CalculationCollection collector)
        {
            foreach (var calculation in calculations)
            {
                collector.AddCalculation(calculation);

                var collection = calculation as ICalculationOwner;

                if (collection != null)
                {
                    collection.Calculations.CollectCalculations(collector);
                }
            }

            return collector;
        }

        public Calculation Get(Guid id)
        {
            var result = Find(id, true);

            if (result == null)
            {
                throw Exceptions.MissingEntity("CalculationCollection", id);
            }

            return result;
        }

        public Calculation Find(Guid id, bool recurse)
        {
            var result = calculations.Find(calculation => calculation.Id.Equals(id));

            if (result == null && recurse)
            {
                foreach (var calculation in calculations)
                {
                    var collection = calculation as CalculationCollection;

                    if (collection == null)
                    {
                        continue;
                    }

                    result = collection.Find(id, true);

                    if (result != null)
                    {
                        return result;
                    }
                }
            }

            return result;
        }

        public bool Remove(Guid id)
        {
            var calculation = Find(id, false);

            if (calculation != null)
            {
                calculations.Remove(calculation);

                return true;
            }

            foreach (var item in calculations)
            {
                var collection = item as CalculationCollection;

                if (collection != null && collection.Remove(id))
                {
                    return true;
                }
            }

            return false;
        }

        public void Grab(Calculation calculation, ICalculationOwner root)
        {
            Grab(calculation.Id, root);
        }

        private void Grab(Guid id, ICalculationOwner root)
        {
            var calculation = root.Calculations.Get(id);

            root.Calculations.Remove(id);

            calculations.Add(calculation);
        }
    }
}