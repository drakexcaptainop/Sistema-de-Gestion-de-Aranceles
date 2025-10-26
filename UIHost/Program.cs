using Common.Application.EmailServices;
using Common.Domain.ServicePorts;
using Common.Domain.SharedPorts;
using Common.Infrastructure.EmailAdapters;
using Common.Infrastructure.Persistence.Database;
using EstablishmentService.Domain.Models;
using EstablishmentService.Domain.RepositoryPorts;
using EstablishmentService.Infrastructure.Adapters;
using Microsoft.Extensions.DependencyInjection;
using TariffingService.Domain.Models;
using TariffingService.Domain.RepositoryPorts;
using TariffingService.Infrastructure.Adapters;
using UIHost.Security;
using UserManagementService.Application.IdentityServices;
using UserManagementService.Application.RepositoryServices;
using UserManagementService.Domain.Models;
using UserManagementService.Domain.Ports;
using UserManagementService.Infrastructure.Adapters;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddSingleton<MySqlConnectionManager>(new MySqlConnectionManager(
    builder.Configuration.GetConnectionString("MySqlConnection")));


builder.Services.AddDataProtection();
builder.Services.AddSingleton<IdProtector>();


builder.Services.AddSingleton<ISharedDbRepository<Category>, CategoryRepository>();
builder.Services.AddSingleton<ISharedDbRepository<Establishment>, EstablishmentRepository>();
builder.Services.AddSingleton<ISharedDbRepository<User>, UserRepository>();
builder.Services.AddSingleton< IUserRepository, UserRepository>();
builder.Services.AddSingleton<IUserRepositoryService, UserRepositoryService>();

var _configuration = builder.Configuration;
var smtpHost = _configuration["Email:SmtpHost"];
var smtpPort = int.Parse(_configuration["Email:SmtpPort"] ?? "587");
var smtpUser = _configuration["Email:SmtpUser"];
var smtpPass = _configuration["Email:SmtpPassword"];
var fromEmail = _configuration["Email:FromEmail"];
var fromName = _configuration["Email:FromName"] ?? "Sistema de Pagos";

ILogger<SmtpEmailAdapter> logger = LoggerFactory.Create(config =>
{
    config.AddConsole();
}).CreateLogger<SmtpEmailAdapter>();

var adapter = new SmtpEmailAdapter(
    new SmtpSettings
    {
        Host = smtpHost,
        Port = smtpPort,
        User = smtpUser,
        Password = smtpPass,
        FromEmail = fromEmail,
        FromName = fromName
    }, logger
    );

builder.Services.AddScoped<IEmailService, SmtpEmailAdapter>( sp=>adapter );
builder.Services.AddScoped<EmailService>();


// Register AuthService
builder.Services.AddScoped<IAuthService, AuthService>();
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
