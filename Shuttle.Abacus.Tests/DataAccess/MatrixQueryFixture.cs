using System;
using NUnit.Framework;
using Shuttle.Abacus.DataAccess;

namespace Shuttle.Abacus.Tests.DataAccess
{
    public class MatrixQueryFixture : DataAccessFixture
    {
        [Test]
        public void Should_be_able_perform_all_queries()
        {
            var query = new MatrixQuery(DatabaseGateway, new MatrixQueryFactory());

            using(TransactionScopeFactory.Create())
            using (DatabaseContextFactory.Create())
            {
                Assert.That(() => query.Get(Guid.NewGuid()), Throws.Nothing);
                Assert.That(() => query.All(), Throws.Nothing);
                Assert.That(() => query.Constraints(Guid.NewGuid()), Throws.Nothing);
                Assert.That(() => query.Search(new MatrixSearchSpecification()), Throws.Nothing);
            }
        }
    }
}