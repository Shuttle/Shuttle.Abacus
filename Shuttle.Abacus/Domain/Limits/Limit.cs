using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus.Domain
{
    public class Limit :
        IFormulaOwner,
        IConstraintOwner
    {
        private readonly List<OwnedConstraint> _constraints = new List<OwnedConstraint>();
        private readonly List<OwnedFormula> _formulas = new List<OwnedFormula>();
        private readonly FormulaCalculation calculation;
        private readonly List<IConstraint> constraints = new List<IConstraint>();

        public Limit(string name, string type)
            : this(Guid.NewGuid(), name, type)
        {
        }

        public Limit(Guid id, string name, string type)
        {
            Id = id;
            Name = name;
            Type = type;

            calculation = new FormulaCalculation(name, false);
        }

        public string Name { get; private set; }
        public string Type { get; private set; }

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

        public Guid Id { get; }

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

        public string OwnerName
        {
            get { return "Limit"; }
        }

        public void AddFormula(OwnedFormula item)
        {
            Guard.AgainstNull(item, "item");

            _formulas.Add(item);
        }

        public LimitResultBuilder ApplyTo(ICalculationResult result)
        {
            switch (Type.ToLower())
            {
                case "maximum":
                {
                    return new MaximumLimitResultBuilder(calculation, result);
                }
                case "minimum":
                {
                    return new MinimumLimitResultBuilder(calculation, result);
                }
                default:
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public Limit Copy()
        {
            var result = new Limit(Name, Type);

            result.AssignFormulas(((FormulaCalculation) calculation.Copy(new Dictionary<Guid, Guid>())).Formulas);
            constraints.ForEach(constraint => result.AddConstraint(constraint.Copy()));

            return result;
        }

        public void ProcessCommand(ChangeLimitCommand command)
        {
            Guard.AgainstNull(command, "command");

            Name = command.Name;
            Type = command.Type;
        }
    }
}