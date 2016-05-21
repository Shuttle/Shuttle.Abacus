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
using Shuttle.Abacus.Localisation;

namespace Abacus.Test.Unit
{
    [TestFixture]
    public class GraphNodeArgumentTest
    {
        [Test]
        public void Should_be_able_to_get_the_correct_display_string()
        {
            const string name = "Sum Insured";
            const decimal value = 12345.67m;

            var argument = new Argument
                         {
                             Name = name
                         };

            var answer = new DecimalArgumentAnswer(name, value);

            var context = new MethodContext().AddArgumentAnswer(answer);

            var display1 = new GraphNodeArgument(argument, "{0}");

            Assert.AreEqual(name, display1.DisplayString(context));

            var display2 = new GraphNodeArgument(argument, "{1}");

            Assert.AreEqual(value.ToString(Resources.FormatDecimal), display2.DisplayString(context));

            var display3 = new GraphNodeArgument(argument, "{0} = {1}");

            Assert.AreEqual(string.Format("{0} = {1}", name, value.ToString(Resources.FormatDecimal)), display3.DisplayString(context));
        }
    }
}
