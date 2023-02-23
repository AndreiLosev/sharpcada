namespace sharpcada.Api;

public static class ConfigurationWebApplicationExtensions
{
    public static void SetConfiguration(this WebApplication app)
    {
        app.MapGet("/get", (IConfiguration appConfig) => 
        {
            return "Hello World! => db1";
        });       
    }

    public static void RunApp(this WebApplication app)
    {
        var appConfig = app.Services.GetService<IConfiguration>();

        if (appConfig is null)
        {
            throw new Exception();
        }

        var appPort = appConfig["APP_PORT"];
        app.Run($"http://+:{appPort}");

    }
}
