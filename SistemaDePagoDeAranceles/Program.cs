using SistemaDePagoDeAranceles.Application.Services;
using SistemaDePagoDeAranceles.Application.Services.Factory;
using SistemaDePagoDeAranceles.Factory;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using SistemaDePagoDeAranceles.Domain.Ports.RepositoryPorts;
using SistemaDePagoDeAranceles.Domain.Models;
using SistemaDePagoDeAranceles.Infrastructure.Database;
using SistemaDePagoDeAranceles.Infrastructure.RespositoryAdapters;


var builder = WebApplication.CreateBuilder(args);

// Razor Pages
builder.Services.AddRazorPages();

// Database connection
builder.Services.AddSingleton<MySqlConnectionManager>();
builder.Services.AddDataProtection();
builder.Services.AddSingleton<IdProtector>();

// ==========================
// 🔹 CATEGORY CONFIGURATION
// ==========================

//builder.Services.AddScoped<IRepositoryFactory<Category>, CategoryRepositoryCreator>();


builder.Services.AddSingleton<IDbRepository<Category>, CategoryRepository>();
builder.Services.AddScoped<IRepositoryServiceFactory<Category>, CategoryRespositoryServiceCreator>();

// ==========================
//  PERSON IN CHARGE CONFIG
// ==========================

builder.Services.AddSingleton<IDbRepository<PersonInCharge>, PersonInChargeRepository>();
builder.Services.AddScoped<IRepositoryServiceFactory<PersonInCharge>, PersonInChargeRepositoryServiceCreator>();

// ==========================
//  ESTABLISHMENT CONFIG
// ==========================

builder.Services.AddSingleton<IDbRepository<Establishment>, EstablishmentRepository>();
builder.Services.AddScoped<IRepositoryServiceFactory<Establishment>, EstablishmentRepositoryServiceCreator>();

// ==========================
// 🔹 PAYMENT CONFIG
// ==========================

builder.Services.AddSingleton<IDbRepository<Payment>, PaymentRepository>();
builder.Services.AddScoped<IRepositoryServiceFactory<Payment>, PaymentRepositoryServiceCreator>();

// ==========================
// 🔹 FEE CONFIG
// ==========================

builder.Services.AddSingleton<IDbRepository<Fee>, FeeRepository>();
builder.Services.AddScoped<IRepositoryServiceFactory<Fee>, FeeRepositoryServiceCreator>();

// ==========================
// 🔹 USER CONFIG
// ==========================

builder.Services.AddSingleton<IDbRepository<User>, UserRepository>();
builder.Services.AddScoped<IRepositoryServiceFactory<User>, UserRepositoryServiceCreator>();

// ==========================
//  APP PIPELINE
// ==========================
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();

app.Run();