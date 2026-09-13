using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shuttle.Contract;

namespace Shuttle.Abacus.SqlServer;

public static class AbacusBuilderExtensions
{
    extension(AbacusBuilder builder)
    {
        public AbacusBuilder UseSqlServer(Action<SqlServerOptions> configureOptions)
        {
            Guard.AgainstNull(builder);
            Guard.AgainstNull(configureOptions);

            var options = new SqlServerOptions();

            configureOptions(options);

            builder.Services
                .AddScoped<IArgumentQuery, ArgumentQuery>()
                .AddScoped<IAlgorithmQuery, AlgorithmQuery>()
                .AddScoped<IMatrixQuery, MatrixQuery>()
                .AddScoped<ITestQuery, TestQuery>()
                .AddDbContext<AbacusDbContext>((_, dbContextOptions) =>
                {
                    dbContextOptions.UseSqlServer(options.ConnectionString, sqlServerOptions =>
                    {
                        sqlServerOptions.CommandTimeout(300);
                        sqlServerOptions.MigrationsHistoryTable("__EFMigrationsHistory", "abacus");
                    });
                });

            return builder;
        }
    }
}
