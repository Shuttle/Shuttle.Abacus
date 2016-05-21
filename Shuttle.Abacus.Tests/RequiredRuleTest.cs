using NUnit.Framework;
using Shuttle.Abacus;
using Shuttle.Abacus.Localisation;

namespace Abacus.Test.Unit
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
