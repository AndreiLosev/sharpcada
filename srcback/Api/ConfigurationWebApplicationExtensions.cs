using sharpcada.Exception;

namespace sharpcada.Api;

public static class ConfigurationWebApplicationExtensions
{
    public static void RunWebApi(this WebApplicationBuilder builder)
    {

        var appConfig = builder.Configuration.CheckConfig();
        var appPort = appConfig.GetValue<string>("APP_PORT");
        builder.Services.AddControllers();

        var app = builder.Build();
        if (appConfig.GetValue<string>("APP_ENV") == "dev")
        {
            app.UseDeveloperExceptionPage();
        }

        app.MapControllers();
        app.Run($"http://+:{appPort}");
    }
}
