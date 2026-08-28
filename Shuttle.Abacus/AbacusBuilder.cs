using Microsoft.Extensions.DependencyInjection;
using Shuttle.Contract;

namespace Shuttle.Abacus;

public class AbacusBuilder(IServiceCollection services)
{
    public IServiceCollection Services { get; } = Guard.AgainstNull(services);
}
