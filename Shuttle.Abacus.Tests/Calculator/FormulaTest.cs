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

using NUnit.Framework;
using Shuttle.Abacus;

namespace Abacus.Test.Unit
{
    [TestFixture]
    public class FormulaTest
    {
        [Test]
        public void Should_be_able_to_create_a_formula_with_an_initial_value()
        {
            var formula = new Formula(new DecimalValueSource(250));

            var methodContext = new MethodContext();

            Assert.AreEqual(250, formula.Execute(methodContext, new FormulaCalculationContext(methodContext)));
        }

        [Test]
        public void Should_be_able_to_execute_a_simple_formula()
        {
            var formula = new Formula();

            formula.AddOperation(new AdditionOperation(new DecimalValueSource(10)));
            formula.AddOperation(new SubtractionOperation(new DecimalValueSource(2)));
            formula.AddOperation(new MultiplicationOperation(new DecimalValueSource(5)));
            formula.AddOperation(new DivisionOperation(new DecimalValueSource(2)));

            var methodContext = new MethodContext();

            Assert.AreEqual(20, formula.Execute(methodContext, new FormulaCalculationContext(methodContext)));
        }

        [Test]
        public void Should_be_able_to_execute_a_working_example()
        {
            var formula = new Formula();

            formula.AddOperation(new AdditionOperation(new ArgumentAnswerValueSource(new Argument
                                                                                   {
                                                                                       Name = "VoluntaryExcess"
                                                                                   })));
            formula.AddOperation(new DivisionOperation(new ArgumentAnswerValueSource(new Argument
                                                                                   {
                                                                                       Name = "SumInsured"
                                                                                   })));
            formula.AddOperation(new SquareRootOperation(new FormulaTotalValueSource()));
            formula.AddOperation(new MultiplicationOperation(new DecimalValueSource(-150)));
            formula.AddOperation(new RoundingOperation(new DecimalValueSource(2)));
            formula.AddOperation(
                new MultiplicationOperation(new CalculationSubTotalValueSource(new FormulaCalculation("TOTAL", true))));
            formula.AddOperation(new DivisionOperation(new DecimalValueSource(100)));

            var context =
                new MethodContext("test")
                    .AddArgumentAnswer(new DecimalArgumentAnswer("VoluntaryExcess", 1000))
                    .AddArgumentAnswer(new DecimalArgumentAnswer("SumInsured", 980000));

            var result = new CalculationCollectionResult(new CalculationCollection("TOTAL"), 636.99m);

            context.IncrementSubTotal(result);
            context.AddResult(result);

            Assert.AreEqual(-30.51m, formula.Execute(context, new FormulaCalculationContext(context)).RoundToCents());
        }
    }
}
