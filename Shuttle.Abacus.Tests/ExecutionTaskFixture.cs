using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Shuttle.Abacus.DataAccess;
using Shuttle.Core.Data;
using Shuttle.Recall;

namespace Shuttle.Abacus.Tests
{
    [TestFixture]
    public class ExecutionEngineFixture
    {
        [Test]
        public void Should_be_able_to_perform_simple_addition()
        {
            var context = new ExecutionEngine(TODO, TODO, TODO);
        }
    }
}