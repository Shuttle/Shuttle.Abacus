using NUnit.Framework;
using Shuttle.Abacus;

namespace Abacus.Test.Unit
{
    [TestFixture]
    public class OperationFactoryTest 
    {
        [Test]
        public void Should_be_able_to_create_a_new_addition_operation()
        {
            IOperationFactory factory = new AdditionOperationFactory();

            var operation = factory.Create(new ArgumentAnswerValueSource(new Argument
                                                                       {
                                                                           Name = "someinput"
                                                                       }));

            Assert.AreEqual("Addition", operation.Name);
        }
    }
}
