using System;
using System.Transactions;
#if (NETCOREAPP2_0 || NETSTANDARD2_0)
using Moq;
#endif
using NUnit.Framework;
using Shuttle.Core.Data;
using Shuttle.Core.Transactions;

namespace Shuttle.Abacus.Tests.DataAccess
{
    [TestFixture]
    public class DataAccessFixture
    {
        [SetUp]
        public void DataAccessSetUp()
        {
            TransactionScopeFactory =
                new DefaultTransactionScopeFactory(true, IsolationLevel.ReadCommitted, TimeSpan.FromSeconds(120));
            DatabaseGateway = new DatabaseGateway();
            DataRowMapper = new DataRowMapper();
            DatabaseContextCache = new ThreadStaticDatabaseContextCache();
#if (!NETCOREAPP2_0 && !NETSTANDARD2_0)
            DatabaseContextFactory = new DatabaseContextFactory(
                new ConnectionConfigurationProvider(),
                new DbConnectionFactory(),
                new DbCommandFactory(),
                new ThreadStaticDatabaseContextCache());
#else
            var connectionConfigurationProvider = new Mock<IConnectionConfigurationProvider>();

            connectionConfigurationProvider.Setup(m => m.Get(It.IsAny<string>())).Returns(
                new ConnectionConfiguration(
                    "Ems",
                    "System.Data.SqlClient",
                    "Data Source=.;Initial Catalog=EMS_DEV;Integrated Security=SSPI;"));

            DatabaseContextFactory = new DatabaseContextFactory(
                connectionConfigurationProvider.Object,
                new DbConnectionFactory(new DbProviderFactories()),
                new DbCommandFactory(),
                new ThreadStaticDatabaseContextCache());
#endif
            DatabaseContextFactory.ConfigureWith("Abacus");
            QueryMapper = new QueryMapper(DatabaseGateway, DataRowMapper);
        }

        protected ITransactionScopeFactory TransactionScopeFactory { get; private set; }
        protected IDatabaseContextCache DatabaseContextCache { get; private set; }
        protected IDatabaseGateway DatabaseGateway { get; private set; }
        protected IDatabaseContextFactory DatabaseContextFactory { get; private set; }
        protected IDataRowMapper DataRowMapper { get; private set; }
        protected IQueryMapper QueryMapper { get; private set; }
    }
}