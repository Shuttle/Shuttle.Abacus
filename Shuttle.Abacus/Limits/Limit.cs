/*
    This file forms part of Shuttle.Abacus.

    Shuttle.Abacus - A constraint-based calculation engine.
    Copyright (C) 2016  Eben Roux

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus
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

        public void ProcessCommand(IChangeFormulaOrderCommand command, IFormulaOwnerService service)
        {
            service.ProcessCommand(command, this);
        }

        public void AssignFormulas(FormulaCollection collection)
        {
            calculation.AssignFormulas(collection);
        }

        public void RemoveFormula(Guid formulaId)
        {
            //TODO: DomainEvents.Raise(new FormulaRemoved(Formulas.Remove(formulaId), this));
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
    }
}
