using NUnit.Framework;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Invariants.Core.Rules;
using Shuttle.Abacus.Localisation;

namespace Shuttle.Abacus.Tests
{
    [TestFixture]
    public class MaximumLengthRuleTest 
    {
        private const string value = "long-long";

        [Test]
        public void Should_fail_for_a_string_that_is_too_long()
        {
            const int length = 5;

            var rule = new MaximumLengthRule(length);

            Assert.IsTrue(rule.IsBrokenBy(value));

            Assert.AreEqual(string.Format(Resources.MaximumLengthRule, length), rule.Message.Text);
        }

        [Test]
        public void Should_pass_for_a_string_that_is_short_enough()
        {
            Assert.IsFalse(new MaximumLengthRule(10).IsBrokenBy(value));
        }
    }
}
