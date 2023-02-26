using sharpcada.Exception;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
// using sharpcada.Data.Repositories;

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
        var assembly = Assembly.GetExecutingAssembly();
        var iterfaceType = typeof(Repositories.Contracts.IBaseRepository);
        var types = assembly.GetTypes()
            .Where(a => a.Namespace is string && a.Namespace.StartsWith("sharpcada.Data"))
            .Where(a => a.IsAssignableTo(iterfaceType) && a.IsClass && !a.IsAbstract)
        ;

        foreach (var type in types)
        {
            conteiner.AddTransient(type);
        }
    }
}
