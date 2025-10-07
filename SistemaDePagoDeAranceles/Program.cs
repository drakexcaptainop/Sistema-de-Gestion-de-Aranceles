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
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<IDbRespository<Category>, CategoryRepository>();
builder.Services.AddScoped<RepositoryFactory<Category>, CategoryRepositoryCreator>();
builder.Services.AddScoped<CategoryRepositoryCreator>();

// ==========================
// 🔹 PERSON IN CHARGE CONFIG
// ==========================
builder.Services.AddScoped<PersonInChargeRepository>();
builder.Services.AddScoped<IDbRespository<PersonInCharge>, PersonInChargeRepository>();
builder.Services.AddScoped<RepositoryFactory<PersonInCharge>, PersonInChargeRepositoryCreator>();
builder.Services.AddScoped<PersonInChargeRepositoryCreator>();

// ==========================
// 🔹 ESTABLISHMENT CONFIG
// ==========================
builder.Services.AddScoped<EstablishmentRepository>();
builder.Services.AddScoped<IDbRespository<Establishment>, EstablishmentRepository>();
builder.Services.AddScoped<RepositoryFactory<Establishment>, EstablishmentRepositoryCreator>();
builder.Services.AddScoped<EstablishmentRepositoryCreator>();

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