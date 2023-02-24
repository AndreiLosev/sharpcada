using sharpcada.Exception;

namespace sharpcada.Api;

public static class ConfigurationWebApplicationExtensions
{
    public static void RunApp(this WebApplicationBuilder builder)
    {

        var appConfig = builder.Configuration.CheckConfig();
        var appPort = appConfig.GetValue<string>("APP_PORT");
        builder.Services.AddControllers();

        var app = builder.Build();
        if (appConfig.GetValue<string>("APP_ENV") == "dev")
        {
            app.UseDeveloperExceptionPage();
        }

        app.SetConfiguration();

        app.Run($"http://+:{appPort}");
    }

    private static void SetConfiguration(this WebApplication app)
    {
        app.MapGet("/get", (IConfiguration appConfig) => 
        {
            return "Hello World! => db1";
        });       
    }
}
