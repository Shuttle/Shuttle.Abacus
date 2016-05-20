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
    public class MethodContextTest
    {
        [Test]
        public void Should_be_able_to_add_and_find_argument_answers()
        {
            var context = new MethodContext(string.Empty);

            context.AddArgumentAnswer(new DecimalArgumentAnswer("SumInsured", 300000));

            Assert.AreSame(ArgumentAnswer.Null, context.GetArgumentAnswer("blah"));
            Assert.AreEqual(300000, context.GetArgumentAnswer("SumInsured").Answer);
        }

        [Test]
        public void Should_be_able_to_add_and_retrieve_a_named_calculation_result()
        {
            var methodContext = new MethodContext(string.Empty);

            var result1 = new CalculationCollectionResult(new CalculationCollection("result1"), 123.45m);

            methodContext.AddResult(result1);
            methodContext.IncrementSubTotal(result1);

            var result2 = new CalculationCollectionResult(new CalculationCollection("result2"), 123.45m);

            methodContext.AddResult(result2);
            methodContext.IncrementSubTotal(result2);

            Assert.AreEqual(123.45m, methodContext.GetResult("result1").Value);
            Assert.AreEqual(123.45m, methodContext.GetResult("result2").Value);

            Assert.AreEqual(246.9m, methodContext.Total.Value);
        }

        [Test]
        public void Should_be_able_to_create_a_read_only_copy()
        {
            var methodContext = new MethodContext(string.Empty);

            methodContext.AddArgumentAnswer(new IntegerArgumentAnswer("input", 10));

            methodContext.IncrementSubTotal(new CalculationCollectionResult(new CalculationCollection("result"), 25));
            methodContext.AddResult(new FormulaCalculationResult(new FormulaCalculation("result"), 25));

            var copy = methodContext.Copy().AsReadOnly();

            Assert.IsTrue(copy.HasArgumentAnswer("input"));
            Assert.AreEqual(10, (int)copy.GetArgumentAnswer("input").Answer);

            Assert.IsTrue(copy.HasResult("result"));
            Assert.AreEqual(25, copy.GetResult("result").Value);
            Assert.AreEqual(25, copy.GetSubTotal("result").Value);

            Assert.AreEqual(25, copy.Total.Value);

            copy.IncrementSubTotal(new CalculationCollectionResult(new CalculationCollection("result2"), 25));
            copy.AddResult(new FormulaCalculationResult(new FormulaCalculation("result2"), 25));

            Assert.IsFalse(copy.HasResult("result2"));
            Assert.AreEqual(25, copy.Total.Value);
        }

    }
}
