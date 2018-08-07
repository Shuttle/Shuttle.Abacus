using System;
using NUnit.Framework;
using Shuttle.Abacus.DataAccess;

namespace Shuttle.Abacus.Tests.DataAccess
{
    public class FormulaQueryFixture : DataAccessFixture
    {
        [Test]
        public void Should_be_able_perform_all_queries()
        {
            var query = new FormulaQuery(DatabaseGateway, new FormulaQueryFactory());

            using(TransactionScopeFactory.Create())
            using (DatabaseContextFactory.Create())
            {
                Assert.That(() => query.Get(Guid.NewGuid()), Throws.TypeOf<RecordNotFoundException>());
                Assert.That(() => query.Search(new FormulaSearchSpecification()), Throws.Nothing);
                Assert.That(() => query.Constraints(Guid.NewGuid()), Throws.Nothing);
                Assert.That(() => query.Operations(Guid.NewGuid()), Throws.Nothing);
            }
        }
    }
}