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
using System.Linq;
using NUnit.Framework;
using Shuttle.Abacus;

namespace Abacus.Test.Unit
{
    [TestFixture]
    public class CalculationCollectionTest
    {
        [Test]
        public void Should_be_able_to_check_for_duplicate_names_in_the_hierarchy()
        {
            var level1 = new CalculationCollection("level1");
            var level2 = new CalculationCollection("level2");
            var level3 = new CalculationCollection("level3");

            level1.AddCalculation(level2);
            level2.AddCalculation(level3);

            Assert.True(level1.ContainsName("level3", Guid.Empty));
        }

        [Test]
        public void Should_be_able_to_delete_a_calculation_in_the_hierarchy()
        {
            var level1 = new CalculationCollection("level1");
            var level2 = new CalculationCollection("level2");
            var level3 = new CalculationCollection("level3");

            level1.AddCalculation(level2);
            level2.AddCalculation(level3);

            var formula1 = new FormulaCalculation("formula1", false);
            var formula2 = new FormulaCalculation("formula2", false);

            level2.AddCalculation(formula1);
            level3.AddCalculation(formula2);

            Assert.IsTrue(level1.HierarchyContains(formula1.Id));
            Assert.IsTrue(level1.HierarchyContains(formula2.Id));

            level1.Remove(formula1.Id);
            level1.Remove(formula2.Id);

            Assert.IsFalse(level1.HierarchyContains(formula1.Id));
            Assert.IsFalse(level1.HierarchyContains(formula2.Id));
        }

        [Test]
        public void Should_be_able_to_grab_calculations()
        {
            var level1 = new CalculationCollection("level1");
            var level2 = new CalculationCollection("level2");
            var level3 = new CalculationCollection("level3");

            level1.AddCalculation(level2);
            level2.AddCalculation(level3);

            var formula1 = new FormulaCalculation("formula1", false);
            var formula2 = new FormulaCalculation("formula2", false);

            level2.AddCalculation(formula1);
            level3.AddCalculation(formula2);

            Assert.AreEqual(1, level1.Calculations.Count());
            Assert.AreEqual(2, level2.Calculations.Count());
            Assert.AreEqual(1, level3.Calculations.Count());

            level1.Grab(formula1, level1);

            Assert.AreEqual(2, level1.Calculations.Count());
            Assert.AreEqual(1, level2.Calculations.Count());
            Assert.AreEqual(1, level3.Calculations.Count());

            level1.Grab(formula2, level1);

            Assert.AreEqual(3, level1.Calculations.Count());
            Assert.AreEqual(1, level2.Calculations.Count());
            Assert.AreEqual(0, level3.Calculations.Count());
        }

        [Test]
        public void Should_not_be_able_to_add_a_calculation_that_uses_a_calculation_that_is_not_yet_defined()
        {
            var method = new Method();

            var level1 = new CalculationCollection("level1");
            var level2 = new CalculationCollection("level2");
            var level3 = new CalculationCollection("level3");

            level1.AddCalculation(level2);
            level2.AddCalculation(level3);

            var formula1 = new FormulaCalculation("formula1", false);
            var formula2 = new FormulaCalculation("formula2", false);

            level2.AddCalculation(formula1);
            level3.AddCalculation(formula2);

            var calculation1 = new FormulaCalculation("calculation1", false);

            var formula3 = new Formula(new CalculationResultValueSource(calculation1));

            formula2.AddFormula(formula3);

            method.AddCalculation(level1);

            Assert.Throws<InvalidCalculationOrderException>(method.EnforceInvariants);

            level2.AddCalculation(calculation1);

            level2.Remove(level3.Id);
            level2.AddCalculation(level3);

            method.EnforceInvariants();
        }
    }
}