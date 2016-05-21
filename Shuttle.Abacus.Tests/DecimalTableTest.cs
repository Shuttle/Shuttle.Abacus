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
