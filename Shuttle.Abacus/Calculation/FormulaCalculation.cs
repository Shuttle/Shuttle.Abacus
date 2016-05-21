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
using Shuttle.Core.Infrastructure;

namespace Shuttle.Abacus
{
    public class FormulaCalculation : Calculation, IFormulaOwner
    {
        public FormulaCalculation(string name)
            : this(name, false)
        {
        }

        public FormulaCalculation(Guid id, string name)
            : this(id, name, false)
        {
        }

        public FormulaCalculation(string name, bool required)
             : this(Guid.NewGuid(), name, required)
        {
        }

        public FormulaCalculation(Guid id, string name, bool required)
            : base(name, required)
        {
            Id = id;

            Formulas = new FormulaCollection();
        }

        public override string Type
        {
            get { return "Formula"; }
        }

        public FormulaCollection Formulas { get; private set; }

        public void ProcessCommand(IChangeFormulaOrderCommand command, IFormulaOwnerService service)
        {
            service.ProcessCommand(command, this);
        }

        public void AssignFormulas(FormulaCollection collection)
        {
            Formulas = collection;
        }

        public void RemoveFormula(Guid formulaId)
        {
            //TODO: DomainEvents.Raise(new FormulaRemoved(Formulas.Remove(formulaId), this));
        }

        public Formula AddFormula(Formula formula)
        {
            Guard.AgainstNull(formula, "formula");

            Formulas.Add(formula);

            return formula;
        }

        public override ICalculationResult Execute(IMethodContext methodContext, ICalculationContext calculationContext)
        {
            Guard.AgainstNull(methodContext, "methodContext");

            if (methodContext.LoggerEnabled)
            {
                methodContext.Log("Starting calculation '{0}'", Name);
            }

            var formula = Formulas.Find(methodContext);

            AbstractCalculationResult result;

            if (formula == null)
            {
                if (methodContext.LoggerEnabled)
                {
                    methodContext.Log(
                        "- (info) could not find a formula that is satisfied by the argument answers.");
                }

                if (Required)
                {
                    methodContext.AddErrorMessage(
                        string.Format("Calculation '{0}' is required but no formula could be found to execute.", Name));

                    return new FormulaCalculationResult(this, 0);
                }

                if (methodContext.LoggerEnabled)
                {
                    methodContext.Log("- (info) this calculation is optional.");
                    methodContext.Log();
                }

                result = new FormulaCalculationResult(this, 0);
            }
            else
            {
                var formulaResult = formula.Execute(methodContext, (IFormulaCalculationContext)calculationContext);

                result = new FormulaCalculationResult(this, formulaResult);

                if (!methodContext.OK)
                {
                    return new FormulaCalculationResult(this, 0);
                }
            }

            ApplyLimits(methodContext, result);

            methodContext.IncrementSubTotal(result);
            methodContext.AddResult(result);

            return result;
        }

        public override Calculation Copy(IDictionary<Guid, Guid> idMap)
        {
            var result = new FormulaCalculation(Name, Required);

            Copy(result);

            result.AssignFormulas(Formulas.Copy());

            return result;
        }

        public override ICalculationContext CalculationContext(IMethodContext methodContext)
        {
            return new FormulaCalculationContext(methodContext);
        }

        public IEnumerable<Guid> RequiredCalculationIds()
        {
            return Formulas.RequiredCalculationIds();
        }
    }
}
