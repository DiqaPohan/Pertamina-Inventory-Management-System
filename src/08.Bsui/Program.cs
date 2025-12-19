using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Pertamina.SolutionTemplate.Bsui.Common.Constants;
using Pertamina.SolutionTemplate.Bsui.Services;
using Pertamina.SolutionTemplate.Bsui.Services.Authentication;
using Pertamina.SolutionTemplate.Bsui.Services.Authorization;
using Pertamina.SolutionTemplate.Bsui.Services.Logging;
using Pertamina.SolutionTemplate.Bsui.Services.Security;
using Pertamina.SolutionTemplate.Client;
using Pertamina.SolutionTemplate.Shared;
using Pertamina.SolutionTemplate.Shared.Common.Constants;
using Syncfusion.Blazor;
using Syncfusion.Licensing;

Console.WriteLine($"Starting {CommonValueFor.EntryAssemblySimpleName}...");

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseLoggingService();
builder.Services.AddHttpContextAccessor();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSyncfusionBlazor();

builder.Services.AddShared(builder.Configuration);
builder.Services.AddClient(builder.Configuration);
builder.Services.AddBsui(builder.Configuration);

//SyncfusionLicenseProvider.RegisterLicense(builder.Configuration["Syncfusion:LicenseKey"]); HEHEHE
StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("Running {AssemblyName}", CommonValueFor.EntryAssemblySimpleName);

if (!app.Environment.IsProduction())
{
    app.UseDeveloperExceptionPage();
}

app.UseBsui(app.Configuration);
app.UseSecurityService(app.Environment);
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthenticationService(app.Configuration);
app.UseAuthorizationService(app.Configuration);
app.MapBlazorHub();
app.MapFallbackToPage(CommonRouteFor.Host);
app.Run();
