using SistemaDePagoDeAranceles.Database;
using SistemaDePagoDeAranceles.Respository;
using SistemaDePagoDeAranceles.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSingleton<MySqlConnectionManager>();

//Example of repository injection
builder.Services.AddScoped<BaseDbRepository<Test>, ExampleTestRepository>();

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
