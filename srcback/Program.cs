using sharpcada;
using sharpcada.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<Worker>();

var app = builder.Build();
app.SetConfiguration();
app.RunApp();


