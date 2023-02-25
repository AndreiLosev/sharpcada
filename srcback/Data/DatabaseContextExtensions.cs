using sharpcada.Exception;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace sharpcada.Data;

public static class DatabaseContextExtensions
{
    public static void AddDataBaseContext(this WebApplicationBuilder builder)
    {
        var appConfig = builder.Configuration.CheckConfig();      
        var connectionString = appConfig.GetValue<string>("DB_CONNECTION_STRING");

        builder.Services.AddDbContext<ApplicationDbContext>(o => o.UseNpgsql(connectionString));

        builder.Services.AddScoped<Repositories.DeviceRepositories>();
        // AddRepositoriesToContainer(builder);
    }

    private static void AddRepositoriesToContainer(WebApplicationBuilder builder)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var iterfaceType = typeof(Repositories.Contracts.IBaseRepository);
        var types = assembly.GetTypes()
            .Where(a => a.Namespace is string && a.Namespace.StartsWith("sharpcada.Data"))
            .Where(a => a.IsAssignableTo(iterfaceType) && a.IsClass && !a.IsAbstract)
        ;

        foreach (var type in types)
        {
            builder.Services.AddScoped(type);
        }
    }
}
