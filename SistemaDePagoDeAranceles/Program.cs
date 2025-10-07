using SistemaDePagoDeAranceles.Database;
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

builder.Services.AddScoped<IRepositoryFactory<Category>, CategoryRepositoryCreator>();

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