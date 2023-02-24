using sharpcada;
using sharpcada.Api;
using sharpcada.Data;

var builder = WebApplication.CreateBuilder(args);
builder.AddDataBaseContext();

builder.Services.AddHostedService<Worker>();

builder.RunApp();
