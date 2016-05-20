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
using NUnit.Framework;
using Shuttle.Abacus;

namespace Abacus.Test.Unit
{
    [TestFixture]
    public class DecimalTableTest
    {
        [Test]
        public void Should_be_able_to_create_a_table_and_add_an_argument_constraint()
        {
            var table = new DecimalTable(Guid.NewGuid(), "Some DecimalValue Table", Guid.Empty, Guid.Empty);

            var value = new DecimalValue(10);

            var answer = new TextArgumentAnswer("argument", "one");

            value.AddConstraint(new EqualsConstraint(Guid.NewGuid(), answer));

            table.AddDecimalValue(value);

            var contextTrue = new MethodContext();

            contextTrue.AddArgumentAnswer(answer);

            Assert.IsTrue(table.IsSatisfiedBy(contextTrue));

            var contextFalse = new MethodContext();

            contextFalse.AddArgumentAnswer(new TextArgumentAnswer("argument", "two"));

            Assert.IsFalse(table.IsSatisfiedBy(contextFalse));
        }
    }
}
