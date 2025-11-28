using System.Reflection;

namespace Common.Extensions;

public static class AssemblyExtensions
{
    public static void Register(
        this IEnumerable<Assembly> assemblies,
        Action<Assembly> registerAction
    )
    {
        foreach (var assembly in assemblies)
        {
            registerAction(assembly);
        }
    }
}
