using SistemaDePagoDeAranceles.Application.Services;
using SistemaDePagoDeAranceles.Application.Services.Factory;
using SistemaDePagoDeAranceles.Database;
using SistemaDePagoDeAranceles.Domain.Ports.DabasePorts;
using SistemaDePagoDeAranceles.Factory;
using SistemaDePagoDeAranceles.Respository;
using SistemaDePagoDeAranceles.Models;

var builder = WebApplication.CreateBuilder(args);

// Razor Pages
builder.Services.AddRazorPages();

// Database connection
builder.Services.AddSingleton<MySqlConnectionManager>();

// ==========================
// 🔹 CATEGORY CONFIGURATION
// ==========================

//builder.Services.AddScoped<IRepositoryFactory<Category>, CategoryRepositoryCreator>();

builder.Services.AddSingleton<IDbConnectionManager, SistemaDePagoDeAranceles.Infrastructure.Database.MySqlConnectionManager>();
builder.Services.AddSingleton<SistemaDePagoDeAranceles.Domain.Ports.RepositoryPorts.IDbRepository<Category>, SistemaDePagoDeAranceles.Infrastructure.RespositoryAdapters.CategoryRepository>();
builder.Services.AddScoped<IRepositoryServiceFactory<Category>, CategoryRespositoryServiceCreator>();

// ==========================
// 🔹 PERSON IN CHARGE CONFIG
// ==========================

builder.Services.AddScoped<IRepositoryFactory<PersonInCharge>, PersonInChargeRepositoryCreator>();

// ==========================
// 🔹 ESTABLISHMENT CONFIG
// ==========================

builder.Services.AddScoped<IRepositoryFactory<Establishment>, EstablishmentRepositoryCreator>();

// ==========================
// 🔹 APP PIPELINE
// ==========================
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();

app.Run();