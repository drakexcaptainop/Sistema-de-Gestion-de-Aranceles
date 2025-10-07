using SistemaDePagoDeAranceles.Database;
using SistemaDePagoDeAranceles.Factory;
using SistemaDePagoDeAranceles.Respository;
using SistemaDePagoDeAranceles.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddSingleton<MySqlConnectionManager>();

builder.Services.AddScoped<IDbRespository<Category>, CategoryRepository>();
builder.Services.AddScoped<RepositoryFactory<Category>, CategoryRepositoryCreator>();

builder.Services.AddScoped<IDbRespository<PersonInCharge>, PersonInChargeRepository>();
builder.Services.AddScoped<RepositoryFactory<PersonInCharge>, PersonInChargeRepositoryCreator>();

builder.Services.AddScoped<IDbRespository<Establishment>, EstablishmentRepository>();
builder.Services.AddScoped<RepositoryFactory<Establishment>, EstablishmentRepositoryCreator>();

builder.Services.AddScoped<EstablishmentRepository>();

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