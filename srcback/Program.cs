var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var db = 0;

app.MapGet("/get", (IConfiguration appConfig) => 
{
    db += 1;
    return $"Hello World! => {db}";
});


RunApp(app);

static void RunApp(WebApplication app)
{
    var appConfig = app.Services.GetService<IConfiguration>();

    if (appConfig is null)
    {
        throw new Exception();
    }

    var appPort = appConfig["APP_PORT"];
    app.Run($"http://0.0.0.0:{appPort}");
}

