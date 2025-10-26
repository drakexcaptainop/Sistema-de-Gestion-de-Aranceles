using Common.Infrastructure.Persistence.Database;
using TariffingService.Infrastructure.Adapters;
using Common.Domain.SharedPorts;
using TariffingService.Domain.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddSingleton<MySqlConnectionManager>(new MySqlConnectionManager(
    builder.Configuration.GetConnectionString("MySqlConnection")));
builder.Services.AddSingleton<ISharedDbRepository<Category>, CategoryRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
