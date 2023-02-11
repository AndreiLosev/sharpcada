var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/get", () => "Hello World!");

app.Run();
