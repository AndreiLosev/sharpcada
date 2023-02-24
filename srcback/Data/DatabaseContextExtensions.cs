using sharpcada.Exception;
using Microsoft.EntityFrameworkCore;

namespace sharpcada.Data;


public static class DatabaseContextExtensions
{
    public static void AddDataBaseContext(this WebApplicationBuilder builder)
    {
        var appConfig = builder.Configuration.CheckConfig();      
        var connectionString = appConfig.GetValue<string>("DB_CONNECTION_STRING");

        builder.Services.AddDbContext<ApplicationDbContext>(o => o.UseNpgsql(connectionString));
    }
}
