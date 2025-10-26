using Common.Infrastructure.Persistence.Database;
using Common.Domain.SharedPorts;

using TariffingService.Infrastructure.Adapters;
using TariffingService.Domain.Models;
using TariffingService.Domain.RepositoryPorts;


using EstablishmentService.Infrastructure.Adapters;
using EstablishmentService.Domain.Models;
using EstablishmentService.Domain.RepositoryPorts;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddSingleton<MySqlConnectionManager>(new MySqlConnectionManager(
    builder.Configuration.GetConnectionString("MySqlConnection")));



builder.Services.AddSingleton<ISharedDbRepository<Category>, CategoryRepository>();
builder.Services.AddSingleton<ISharedDbRepository<Establishment>, EstablishmentRepository>();

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
