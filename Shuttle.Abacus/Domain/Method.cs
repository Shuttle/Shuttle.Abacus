using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Shuttle.Abacus.Infrastructure;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Domain
{
    public class Method :
        ICalculationOwner,
        ILimitOwner,
        IHaveInvariants
    {
        private MethodCalculationCollection calculations = new MethodCalculationCollection();
        private readonly List<CalculationItem> _calculations = new List<CalculationItem>();
        private readonly List<OwnedLimit> _limits = new List<OwnedLimit>();
        private readonly List<MethodTestItem> _tests = new List<MethodTestItem>();

        public Method(string methodName)
            : this(Guid.NewGuid(), methodName)
        {
        }

        public Method(Guid id, string methodName)
        {
            Id = id;
            MethodName = methodName;
        }

        public Guid Id { get; private set; }
        public string MethodName { get; private set; }

        public CalculationCollection CalculationCollection
        {
            get { return calculations; }
        }

        public CalculationCollection AddCalculation(Calculation calculation)
        {
            calculations.AddCalculation(calculation);

            return calculations;
        }

        public ICalculationOwner FindOwner(Guid ownerId)
        {
            return Id.Equals(ownerId)
                       ? this
                       : calculations.FindOwner(ownerId);
        }

        public void AddCalculation(CalculationItem calculation)
        {
            Guard.AgainstNull(calculation, "calculation");

            _calculations.Add(calculation);
        }

        CalculationCollection ICalculationOwner.Calculations
        {
            get { return calculations; }
        }

        public void AssignCalculations(IEnumerable<Calculation> collection)
        {
            calculations = new MethodCalculationCollection();

            foreach (var calculation in collection)
            {
                calculations.AddCalculation(calculation);
            }
        }

        public string OwnerName
        {
            get { return "Method"; }
        }

        public ILimitOwner AddLimit(Limit limit)
        {
            calculations.AddLimit(limit);

            return this;
        }

        public IEnumerable<Limit> Limits
        {
            get { return calculations.Limits; }
        }

        public void Calculate(IMethodContext methodContext)
        {
            calculations.AssignName(MethodName);

            Stopwatch sw = null;

            if (methodContext.LoggerEnabled)
            {
                sw = new Stopwatch();
                sw.Start();
            }

            calculations.Execute(methodContext, null);

            if (!methodContext.OK)
            {
                return;
            }

            if (!methodContext.LoggerEnabled || sw == null)
            {
                return;
            }

            sw.Stop();

            methodContext.LogResults();
            methodContext.Log();
            methodContext.LogSubTotals();
            methodContext.Log();
            methodContext.Log("<< completed in {0} ms >>", sw.ElapsedMilliseconds.ToString("#,##0"));
        }

        public Method Copy()
        {
            var result = new Method(MethodName = MethodName);

            result.AssignCalculations((CalculationCollection)calculations.Copy(new Dictionary<Guid, Guid>()));

            foreach (var limit in Limits)
            {
                result.AddLimit(limit.Copy());
            }

            return result;
        }

        public void EnforceInvariants()
        {
            var flattened = calculations.Flattened();

            var defined = new List<Guid>();

            foreach (var calculation in flattened)
            {
                var current = calculation as FormulaCalculation;

                if (current != null)
                {
                    foreach (var id in current.RequiredCalculationIds())
                    {
                        if (!defined.Contains(id))
                        {
                            var uses = flattened.Find(id, true);

                            if (uses != null)
                            {
                                throw new InvalidCalculationOrderException(
                                    string.Format(
                                        "Calculation '{0}' uses calculation '{1}' but '{1}' has been moved after '{0}'.",
                                        current.Name, uses.Name));
                            }

                            throw new InvalidCalculationOrderException(
                                string.Format(
                                    "Calculation '{0}' uses a calculation with id '{1}' that does not exist.  It may have been removed during the current operation.",
                                    current.Name, id));
                        }
                    }
                }

                defined.Add(calculation.Id);
            }
        }

        public void AddLimit(OwnedLimit limit)
        {
            Guard.AgainstNull(limit, "limit");

            _limits.Add(limit);
        }

        public void AddMethodTest(MethodTestItem item)
        {
            Guard.AgainstNull(item, "item");

            _tests.Add(item);
        }
    }
}
