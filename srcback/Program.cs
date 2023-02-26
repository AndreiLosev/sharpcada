using sharpcada;
using sharpcada.Api;
using sharpcada.Data;

var builder = WebApplication.CreateBuilder(args);

builder.RegistrationDbServices();
builder.Services.AddHostedService<Worker>();
builder.RunWebApi();
