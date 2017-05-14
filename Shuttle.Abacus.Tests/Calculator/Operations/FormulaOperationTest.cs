using NUnit.Framework;
using Shuttle.Abacus.Domain;

namespace Shuttle.Abacus.Tests
{
    [TestFixture]
    public class FormulaOperationTest
    {
        [Test]
        public void Should_be_able_to_extract_the_flattened_values_for_an_operation()
        {
            var operation = new AdditionOperation(new ConstantValueSource(100));

            Assert.AreEqual("Addition", operation.Name);
        }
    }
}
