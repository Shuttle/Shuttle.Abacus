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
    public class LimitsTest 
    {
        [Test]
        public void Should_be_able_to_limit_to_a_maximum_value()
        {
            var calculation = new FormulaCalculation("calculation", true);

            calculation.AddFormula(new Formula().AddOperation(new AdditionOperation(new DecimalValueSource(200))));

            var limit = new MaximumLimit("max");

            limit.AddFormula(new Formula().AddOperation(new AdditionOperation(new DecimalValueSource(100))));

            calculation.AddLimit(limit);

            var methodContext = new MethodContext();

            Assert.AreEqual(100, calculation.Execute(methodContext, new FormulaCalculationContext(methodContext)).Value);
        }

        [Test]
        public void Should_be_able_to_limit_to_a_minimum_value()
        {
            var calculation = new FormulaCalculation("calculation", true);

            calculation.AddFormula(new Formula().AddOperation(new AdditionOperation(new DecimalValueSource(100))));

            var limit = new MinimumLimit("min");

            limit.AddFormula(new Formula().AddOperation(new AdditionOperation(new DecimalValueSource(50))));

            calculation.AddLimit(limit);

            var methodContext = new MethodContext();

            Assert.AreEqual(100, calculation.Execute(methodContext, new FormulaCalculationContext(methodContext)).Value);
        }
    }
}
