using NUnit.Framework;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Invariants.Core;
using Shuttle.Abacus.Localisation;

namespace Shuttle.Abacus.Tests
{
    [TestFixture]
    public class RequiredRuleTest
    {
        [Test]
        public void Should_fail_when_no_value_is_specified()
        {
            var rule = new RequiredRule();

            Assert.IsTrue(rule.IsBrokenBy(string.Empty));

            Assert.AreEqual(Resources.RequiredRule, rule.Message.Text);
        }

        [Test]
        public void Should_pass_when_a_value_is_specified()
        {
            Assert.IsFalse(new RequiredRule().IsBrokenBy("blah"));
        }
    }
}
