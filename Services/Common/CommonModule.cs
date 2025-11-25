using System.Reflection;
using LiteBus.Commands;
using LiteBus.Extensions.Microsoft.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Common;

public static class CommonModule
{
    public static IServiceCollection AddApplication(this IServiceCollection services, params Assembly[] assemblies)
    {
        // Register LiteBus modules
        services.AddLiteBus(liteBus =>
        {
            foreach (var assembly in assemblies)
            {
                liteBus.AddCommandModule(module => module.RegisterFromAssembly(assembly));
            }
        });

        return services;
    }
}
