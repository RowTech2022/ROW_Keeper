using Keeper.Api;
using Keeper.Core;
using Microsoft.Extensions.FileProviders;
using Row.Common1;

var builder = WebApplication.CreateBuilder(args); 

builder.Services.AddSignalR();

var configs = builder.Configuration.GetSection(Globul.ProjectKey).GetChildren().ToList();
var settings = new StartupSettings().Load(configs);

var access = new Access();
builder.Services.UseAllCommonSettings(settings.Common, access);

builder.Services.AddSingleton(settings.Sql);

var fileEngine = new FileEngine(settings.FileStoragePath);
builder.Services.AddSingleton(fileEngine);

var dtoComplex = settings.Common.Injections.Dto;
if (dtoComplex == null)
    throw new Exception("Dto complex cannot be null.");

var userEngine2 = new UserEngine2(settings.Sql, dtoComplex, fileEngine, settings.IgnorePassword, settings.IgnoreCode);
builder.Services.AddSingleton(userEngine2);

var userEngine = new UserEngine(settings.Sql, dtoComplex, settings.IgnorePassword,
    settings.SMSSend);
builder.Services.AddSingleton(userEngine);

settings.AuthServerSettings.TokenProtector = settings.Common.Injections.DataProtector;
settings.AuthServerSettings.AuthProtector = settings.Common.AuthDataProtector;

var authEngine2 = new AuthEngine(userEngine, settings.AuthServerSettings, settings.Sql, settings.SMSSend);
builder.Services.AddSingleton(authEngine2);

builder.Services.AddSingleton<OrganizationEngine>();

builder.Services.AddSingleton<CategoryEngine>();

builder.Services.AddSingleton<ProductEngine>();

builder.Services.AddSingleton<ProductDiscountEngine>();

builder.Services.AddSingleton<SupplierEngine>();

builder.Services.AddSingleton<LanguageService>();

builder.Services.AddSingleton<PlanEngine>();

var app = builder.Build();

app.InitBaseStartup(settings.Common);

if (!Directory.Exists(settings.FileStoragePath))
    Directory.CreateDirectory(settings.FileStoragePath);

StaticMiddleware.AddFiles(settings.FileStoragePath);

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(settings.FileStoragePath),
    RequestPath = Helper.FilePublicPath
});

app.Run();
