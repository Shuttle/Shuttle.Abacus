using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Shuttle.Abacus.SqlServer;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AbacusDbContext>
{
    public AbacusDbContext CreateDbContext(string[] args)
    {
        /*
            Right-click on `Shuttle.Abacus.SqlServer` and select `Manage User Secrets`
            {
              "ConnectionStrings": {
                "Abacus": "Data Source=.;Initial Catalog=Abacus;user id=<user>;password=<password>;TrustServerCertificate=true"
              }
            }
        */

        var configuration = new ConfigurationBuilder()
            .AddUserSecrets<DesignTimeDbContextFactory>()
            .Build();

        var connectionString = configuration.GetConnectionString("Abacus")
                                ?? "Data Source=.;Initial Catalog=Abacus;Integrated Security=True;TrustServerCertificate=True";

        var optionsBuilder = new DbContextOptionsBuilder<AbacusDbContext>();

        optionsBuilder.UseSqlServer(connectionString, sqlServerOptions =>
        {
            sqlServerOptions.CommandTimeout(300);
            sqlServerOptions.MigrationsHistoryTable("__EFMigrationsHistory", "abacus");
        });

        return new(optionsBuilder.Options);
    }
}
