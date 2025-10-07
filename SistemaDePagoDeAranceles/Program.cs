using SistemaDePagoDeAranceles.Database;
using SistemaDePagoDeAranceles.Factory;
using SistemaDePagoDeAranceles.Respository;
using SistemaDePagoDeAranceles.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSingleton<MySqlConnectionManager>();

//Example of repository injection

//builder.Services.AddScoped<BaseDbRepository<Test>, ExampleTestRepository>(); // NO
//builder.Services.AddScoped<RepositoryFactory<Test>, ExampleTestRepositoryCreator>(); //SI

//builder.Services.AddScoped<BaseDbRepository<Category>, CategoryRepository>(); // NO
//builder.Services.AddScoped<RepositoryFactory<Category>, CategoryRepositoryCreator>(); //SI


builder.Services.AddScoped<IDbRespository<Category>, CategoryRepository>();


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
