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

        public Method(CreateMethodCommand command)
        {
            MethodName = command.MethodName;
        }

        public Method(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }

        public Method() : this(Guid.NewGuid())
        {
        }

        public string MethodName { get; set; }

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

        CalculationCollection ICalculationOwner.Calculations
        {
            get { return calculations; }
        }

        public ICalculationOwner ProcessCommand(ChangeCalculationOrderCommand command)
        {
            var owner = FindOwner(command.OwnerId);

            if (owner.Calculations.Count() != command.OrderedIds.Count)
            {
                throw new InvalidStateException("The calculation collection has changed since you started the ordering.  Plase reload the calculations and try again.");
            }

            var result = new CalculationCollection(owner.Calculations.Name);

            foreach (var id in command.OrderedIds)
            {
                result.AddCalculation(owner.Calculations.Get(id));
            }

            owner.AssignCalculations(result);

            EnforceInvariants();

            return this;
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

        public Method ProcessCommand(ChangeMethodCommand command)
        {
            MethodName = command.MethodName;

            return this;
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

        public ICalculationOwner ProcessCommand(CreateCalculationCommand command, Calculation calculation)
        {
            Guard.Against<InvalidStateException>(calculations.ContainsName(command.OwnerName, Guid.Empty), string.Format("Cannot add calculation '{0}' since there is already a calculation with this name.", command.Name));

            var owner = FindOwner(command.OwnerId);

            if (owner == null)
            {
                throw new MissingEntityException(string.Format("ICalculationOwner with id '{0}'.", command.OwnerId));
            }

            owner.AddCalculation(calculation);

            throw new NotImplementedException();
            //return new CalculationAdded(this, owner, calculation));
        }

        public Method Copy()
        {
            var result = new Method
                         {
                             MethodName = MethodName
                         };

            result.AssignCalculations((CalculationCollection)calculations.Copy(new Dictionary<Guid, Guid>()));

            foreach (var limit in Limits)
            {
                result.AddLimit(limit.Copy());
            }

            return result;
        }

        public Method ProcessCommand(CopyMethodCommand command)
        {
            MethodName = command.MethodName;

            return this;
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

        public void ProcessCommand(DeleteCalculationCommand command)
        {
            calculations.Remove(command.CalculationId);

            EnforceInvariants();
        }

        public void ProcessCommand(ChangeCalculationCommand command)
        {
            Guard.Against<InvalidStateException>(calculations.ContainsName(command.OwnerName, command.CalculationId), string.Format("Cannot change calculation name to '{0}' since there is already a calculation with this name.", command.Name));

            calculations.Get(command.CalculationId).ProcessCommand(command);

            EnforceInvariants();
        }

        public void ProcessCommand(GrabCalculationsCommand command)
        {
            var grabber = (CalculationCollection)calculations.Get(command.GrabberCalculationId);

            foreach (var id in command.GrabbedCalculationIds)
            {
                grabber.Grab(calculations.Get(id), calculations);
            }

            EnforceInvariants();
        }
    }
}
