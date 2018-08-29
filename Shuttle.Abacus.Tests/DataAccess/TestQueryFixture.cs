using System;
using NUnit.Framework;
using Shuttle.Abacus.DataAccess;

namespace Shuttle.Abacus.Tests.DataAccess
{
    public class TestQueryFixture : DataAccessFixture
    {
        [Test]
        public void Should_be_able_perform_all_queries()
        {
            var query = new TestQuery(DatabaseGateway, new TestQueryFactory());

            using(TransactionScopeFactory.Create())
            using (DatabaseContextFactory.Create())
            {
                Assert.That(() => query.Get(Guid.NewGuid()), Throws.TypeOf<RecordNotFoundException>());
                Assert.That(() => query.Search(new TestSearchSpecification()), Throws.Nothing);
                Assert.That(() => query.All(), Throws.Nothing);
                Assert.That(() => query.Arguments(Guid.NewGuid()), Throws.Nothing);
            }
        }
    }
}