using sharpcada.Exception;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace sharpcada.Data;

public static class DatabaseContextExtensions
{
    public static void RegistrationDbServices(this WebApplicationBuilder builder)
    {
        var appConfig = builder.Configuration.CheckConfig();      
        var connectionString = appConfig.GetValue<string>("DB_CONNECTION_STRING");

        builder.Services.AddDbContext<ApplicationDbContext>(o => o.UseNpgsql(connectionString));

        var conteiner = builder.Services;
        AddRepositoriesToContainer(ref conteiner);
    }

    private static void AddRepositoriesToContainer(ref IServiceCollection conteiner)
    {
        var folderNamespace = typeof(DatabaseContextExtensions).Namespace
            ?? throw new System.Exception("This will never happen");

        var assembly = Assembly.GetExecutingAssembly();
        var iterfaceType = typeof(Repositories.Contracts.IRepository);
        var types = assembly.GetTypes()
            .Where(a => a.Namespace is string && a.Namespace.StartsWith(folderNamespace))
            .Where(a => a.IsAssignableTo(iterfaceType) && a.IsClass && !a.IsAbstract);

        foreach (var type in types)
        {
            conteiner.AddScoped(type);
        }
    }
}
