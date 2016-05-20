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
    public class DecimalValueTest
    {
        [Test]
        public void Should_be_able_to_create_a_new_value_and_constrain_it()
        {
            var rate = new DecimalValue(10);

            Assert.IsTrue(rate.IsSatisfiedBy(new MethodContext()));

            var answer = new TextArgumentAnswer("argument", "one");

            rate.AddConstraint(new EqualsConstraint(Guid.NewGuid(), answer));

            var contextTrue = new MethodContext();

            contextTrue.AddArgumentAnswer(answer);

            Assert.IsTrue(rate.IsSatisfiedBy(contextTrue));

            var contextFalse = new MethodContext();

            contextFalse.AddArgumentAnswer(new TextArgumentAnswer("argument", "two"));

            Assert.IsFalse(rate.IsSatisfiedBy(contextFalse));
        }
    }
}
