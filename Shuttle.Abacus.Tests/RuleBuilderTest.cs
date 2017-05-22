using NUnit.Framework;
using Shuttle.Abacus.Domain;
using Shuttle.Abacus.Invariants.Core;

namespace Shuttle.Abacus.Tests
{
    [TestFixture]
    public class RuleBuilderTest
    {
        [Test]
        public void Should_be_able_to_build_a_simple_rule_set()
        {
            var set = Rule.With().MinimumLength(5).Create();

            Assert.IsFalse(set.BrokenBy("oops").IsEmpty);
            Assert.IsTrue(set.BrokenBy("long enough").IsEmpty);
        }
    }
}
