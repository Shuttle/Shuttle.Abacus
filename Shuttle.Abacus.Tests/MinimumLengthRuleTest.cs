using NUnit.Framework;
using Shuttle.Abacus.Domain;

namespace Shuttle.Abacus.Tests
{
    [TestFixture]
    public class MinimumLengthRuleTest
    {
        private const string value = "blah";

        [Test]
        public void Should_fail_for_a_string_that_is_too_short()
        {
            const int length = 5;

            var rule = new MinimumLengthRule(length);

            Assert.IsTrue(rule.IsBrokenBy(value));

            Assert.AreEqual(string.Format(Resources.MinimumLengthRule, length), rule.Message.Text);
        }

        [Test]
        public void Should_pass_for_a_string_that_is_long_enough()
        {
            Assert.IsFalse(new MinimumLengthRule(4).IsBrokenBy(value));
        }
    }
}
