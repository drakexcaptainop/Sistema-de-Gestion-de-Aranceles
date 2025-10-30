using Common.Application.EmailServices;
using Common.Domain.ServicePorts;
using Common.Domain.SharedPorts;
using Common.Infrastructure.EmailAdapters;
using Common.Infrastructure.Persistence.Database;
using EstablishmentService.Application.ServiceFactory;
using EstablishmentService.Domain.Models;
using EstablishmentService.Infrastructure.Adapters;
using TariffingService.Application.ServiceFactory;
using TariffingService.Domain.Models;
using TariffingService.Infrastructure.Adapters;
using UIHost.Security;
using UserManagementService.Application.IdentityServices;
using UserManagementService.Application.RepositoryServices;
using UserManagementService.Application.ServiceFactory;
using UserManagementService.Domain.Models;
using UserManagementService.Domain.Ports;
using UserManagementService.Infrastructure.Adapters;
using ReportService.Application;
using QuestPDF.Infrastructure;

QuestPDF.Settings.License = LicenseType.Community;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddSingleton<MySqlConnectionManager>(new MySqlConnectionManager(
    builder.Configuration.GetConnectionString("MySqlConnection")));


builder.Services.AddDataProtection();
builder.Services.AddSingleton<IdProtector>();


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
//  FEE CONFIG
// ==========================

builder.Services.AddSingleton<IDbRepository<Fee>, FeeRepository>();
builder.Services.AddScoped<IRepositoryServiceFactory<Fee>, FeeRepositoryServiceCreator>();

builder.Services.AddSingleton<IDbRepository<User>, UserRepository>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRepositoryServiceFactory<User>, UserRepositoryServiceCreator>();
builder.Services.AddScoped<IUserRepositoryService, UserRepositoryService>();

// 🔹 Report service registration
builder.Services.AddScoped<EstablishmentReportService>(sp =>
{
    var estRepo = sp.GetRequiredService<IDbRepository<Establishment>>();
    var personRepo = sp.GetRequiredService<IDbRepository<PersonInCharge>>();
    return new EstablishmentReportService(estRepo, personRepo);
});


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