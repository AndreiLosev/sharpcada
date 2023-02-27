using System.Reflection;
using srcback.Services.Contracts;

namespace sharpcada.Services;

public static class ServicesExtensions
{
    public static void RegistrationServices(this WebApplicationBuilder builder)
    {
        var folderNamespace = typeof(ServicesExtensions).Namespace
            ?? throw new System.Exception("This will never happen");

        var assembly = Assembly.GetExecutingAssembly();
        var iterfacetype = typeof(IServices);
        var types = assembly.GetTypes()
            .Where(a => a.Namespace is string && a.Namespace.StartsWith(folderNamespace))
            .Where(a => a.IsAssignableTo(iterfacetype) && a.IsClass && !a.IsAbstract);

        foreach (var type in types)
        {
            builder.Services.AddTransient(type);
        }
    }
}
