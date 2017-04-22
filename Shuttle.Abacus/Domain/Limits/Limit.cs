using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Domain
{
    public abstract class Limit :
        IFormulaOwner,
        IConstraintOwner
    {
        protected readonly FormulaCalculation calculation;
        private readonly List<IConstraint> constraints = new List<IConstraint>();

        protected Limit(string name)
        {
            Name = name;

            calculation = new FormulaCalculation(name, false);
        }

        public string Name { get; protected set; }
        public abstract string Type { get; }

        public IEnumerable<IConstraint> Constraints
        {
            get { return new ReadOnlyCollection<IConstraint>(constraints); }
        }

        public IConstraintOwner AddConstraint(IConstraint constraint)
        {
            Guard.AgainstNull(constraint, "constraint");

            constraints.Add(constraint);

            return this;
        }

        public Formula AddFormula(Formula formula)
        {
            calculation.AddFormula(formula);

            return formula;
        }

        public FormulaCollection Formulas
        {
            get { return calculation.Formulas; }
        }

        public void ProcessCommand(ChangeFormulaOrderCommand command, IFormulaOwnerService service)
        {
            service.ProcessCommand(command, this);
        }

        public void AssignFormulas(FormulaCollection collection)
        {
            calculation.AssignFormulas(collection);
        }

        //TODO: Handle Event
        public FormulaRemoved RemoveFormula(Guid formulaId)
        {
            return new FormulaRemoved(Formulas.Remove(formulaId), this);
        }

        public abstract LimitResultBuilder ApplyTo(ICalculationResult result);

        public string OwnerName
        {
            get { return "Limit"; }
        }

        public abstract Limit Copy();

        protected void Copy(Limit limit)
        {
            limit.AssignFormulas(((FormulaCalculation)calculation.Copy(new Dictionary<Guid, Guid>())).Formulas);
            constraints.ForEach(constraint => limit.AddConstraint(constraint.Copy()));
        }

        public Guid Id { get; }
    }
}
