using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Shuttle.Abacus.DataAccess;
using Shuttle.Core.Data;
using Shuttle.Recall;

namespace Shuttle.Abacus.Tests
{
    [TestFixture]
    public class ExecutionTaskFixture
    {
        [Test]
        public void Should_be_able_to_perform_simple_addition()
        {
            var task = new ExecutionTask(new Mock<IDatabaseContextFactory>().Object, new Mock<IEventStore>().Object, new Mock<IFormulaQuery>().Object, new Mock<IArgumentQuery>().Object);

            task.Execute("simple", new List<ArgumentValue>()
            {
                new ArgumentValue
                {
                    Name = "argument-1",
                    Value = "2"
                },
                new ArgumentValue
                {
                    Name = "argument-2",
                    Value = "3"
                }
            });
        }
    }
}