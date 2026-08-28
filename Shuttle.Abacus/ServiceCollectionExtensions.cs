using Microsoft.Extensions.DependencyInjection;
using Shuttle.Abacus.DataAccess;
using Shuttle.Contract;

namespace Shuttle.Abacus;

public static class ServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public AbacusBuilder AddAbacus()
        {
            Guard.AgainstNull(services);

            services
                .AddSingleton<IDataTypeFactory, DataTypeFactory>()
                .AddSingleton<IValueComparer, ValueComparer>()
                .AddScoped<IExecutionService, ExecutionService>()
                .AddScoped<IArgumentRepository, ArgumentRepository>()
                .AddScoped<IFormulaRepository, FormulaRepository>()
                .AddScoped<IMatrixRepository, MatrixRepository>()
                .AddScoped<ITestRepository, TestRepository>();

            return new(services);
        }
    }
}
