var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/get", () => "Hello World!");

app.Run("http://0.0.0.0:6542");
