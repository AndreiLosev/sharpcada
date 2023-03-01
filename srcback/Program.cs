using sharpcada;
using sharpcada.Api;
using sharpcada.Data;

var builder = WebApplication.CreateBuilder(args);

builder.RegistrationDAL();
builder.Services.AddSingleton<Worker>();
builder.Services.AddHostedService<BackgroundService>(p => p.GetRequiredService<Worker>());
builder.RunWebApi();
