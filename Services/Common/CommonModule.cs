using System.Reflection;
using Common.Extensions;
using LiteBus.Commands;
using LiteBus.Events;
using LiteBus.Extensions.Microsoft.DependencyInjection;
using LiteBus.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Common;

public static class CommonModule
{
    public static IServiceCollection RegisterFeatureHandlers(
        this IServiceCollection services,
        params Assembly[] assemblies
    )
    {
        services.AddLiteBus(liteBus =>
        {
            liteBus.AddCommandModule(m => assemblies.Register(a => m.RegisterFromAssembly(a)));
            liteBus.AddEventModule(m => assemblies.Register(a => m.RegisterFromAssembly(a)));
            liteBus.AddQueryModule(m => assemblies.Register(a => m.RegisterFromAssembly(a)));
        });

        return services;
    }
}
