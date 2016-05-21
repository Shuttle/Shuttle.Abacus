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
