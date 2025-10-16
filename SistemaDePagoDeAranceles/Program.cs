using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using SistemaDePagoDeAranceles.Application.Services;
using SistemaDePagoDeAranceles.Application.Services.Factory;
using SistemaDePagoDeAranceles.Domain.Models;
using SistemaDePagoDeAranceles.Domain.Ports.RepositoryPorts;
using SistemaDePagoDeAranceles.Domain.Ports.ServicePorts;
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
// 🔹 Authentication CONFIG
// ==========================
// Session + HttpContextAccessor

// Register IUserRepository and IAuthService so page models can resolve IAuthService
builder.Services.AddSingleton<SistemaDePagoDeAranceles.Domain.Ports.RepositoryPorts.IUserRepository, SistemaDePagoDeAranceles.Infrastructure.RespositoryAdapters.UserRepository>();
builder.Services.AddScoped<SistemaDePagoDeAranceles.Domain.Ports.ServicePorts.IAuthService, SistemaDePagoDeAranceles.Application.Services.AuthService>();
// Add authentication (cookie) and authorization
builder.Services.AddAuthentication(Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
        options.AccessDeniedPath = "/Login";
        options.ExpireTimeSpan = System.TimeSpan.FromHours(8);
    });
builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();
// Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = System.TimeSpan.FromHours(8);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
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

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseSession();
app.UseAuthorization();

app.MapRazorPages();

app.Run();