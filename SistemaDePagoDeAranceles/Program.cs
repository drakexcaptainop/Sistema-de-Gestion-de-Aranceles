using SistemaDePagoDeAranceles.Application.Services;
using SistemaDePagoDeAranceles.Application.Services.Factory;
using SistemaDePagoDeAranceles.Database;
using SistemaDePagoDeAranceles.Domain.Ports.DabasePorts;
using SistemaDePagoDeAranceles.Factory;
using SistemaDePagoDeAranceles.Respository;
using SistemaDePagoDeAranceles.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;


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

builder.Services.AddSingleton<IDbConnectionManager, SistemaDePagoDeAranceles.Infrastructure.Database.MySqlConnectionManager>();
builder.Services.AddSingleton<SistemaDePagoDeAranceles.Domain.Ports.RepositoryPorts.IDbRepository<Category>, 
    SistemaDePagoDeAranceles.Infrastructure.RespositoryAdapters.CategoryRepository>();
builder.Services.AddScoped<IRepositoryServiceFactory<Category>, CategoryRespositoryServiceCreator>();

// ==========================
//  PERSON IN CHARGE CONFIG
// ==========================

builder.Services.AddScoped<IRepositoryFactory<PersonInCharge>, PersonInChargeRepositoryCreator>();

// ==========================
//  ESTABLISHMENT CONFIG
// ==========================

builder.Services.AddScoped<IRepositoryFactory<Establishment>, EstablishmentRepositoryCreator>();

// ==========================
//  LOGIN CONFIG
// ==========================
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opt =>
    {
        opt.LoginPath = "/Login";
        opt.LogoutPath = "/Logout";
        opt.AccessDeniedPath = "/Denied";
        opt.SlidingExpiration = true;
    });

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