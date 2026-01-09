using Microsoft.AspNetCore.Hosting.StaticWebAssets;
//using Pertamina.SolutionTemplate.Bsui.Common.Constants;
//using Pertamina.SolutionTemplate.Bsui.Services;
//using Pertamina.SolutionTemplate.Bsui.Services.Authentication;
//using Pertamina.SolutionTemplate.Bsui.Services.Authorization;
//using Pertamina.SolutionTemplate.Bsui.Services.Logging;
//using Pertamina.SolutionTemplate.Bsui.Services.Security;
// using Pertamina.SolutionTemplate.Client;
// using Pertamina.SolutionTemplate.Shared;
// using Pertamina.SolutionTemplate.Shared.Common.Constants;
using MudBlazor.Services;
using Pertamina.SolutionTemplate.Bsui.ViewModels;
using Syncfusion.Blazor;
using Syncfusion.Licensing;

Console.WriteLine($"Starting Pertamina Inventory...");

var builder = WebApplication.CreateBuilder(args);

// Registrasi ViewModel
builder.Services.AddScoped<InventoryViewModel>();
builder.Services.AddScoped<DashboardViewModel>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor()
    .AddHubOptions(options =>
    {
        // Allow bigger SignalR payloads (adjust as needed). This must be >= the largest image stream you accept.
        options.MaximumReceiveMessageSize = 10 * 1024 * 1024; // 10 MB
    });

builder.Services.Configure<Microsoft.AspNetCore.Components.Server.CircuitOptions>(options =>
{
    // Enable detailed errors while debugging to capture serialization/SignalR issues.
    options.DetailedErrors = true;
});

builder.Services.AddMudServices();
builder.Services.AddSyncfusionBlazor();

// Register named HttpClient used by InventoryViewModel
builder.Services.AddHttpClient("Pertamina.SolutionTemplate.WebApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:59908/"); // match your WebApi url
    client.Timeout = TimeSpan.FromSeconds(30);
});

StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);

var app = builder.Build();

// Ensure logger service available
var logger = app.Services.GetService<ILogger<Program>>();

AppDomain.CurrentDomain.UnhandledException += (sender, eventArgs) =>
{
    try
    {
        var ex = eventArgs.ExceptionObject as Exception;
        if (ex != null)
        {
            logger?.LogCritical(ex, "Unhandled exception (AppDomain)");
            Console.WriteLine($"Unhandled exception: {ex}");
        }
        else
        {
            logger?.LogCritical("Unhandled exception object (non-Exception) in AppDomain");
        }
    }
    catch { /* avoid throwing from handler */ }
};

TaskScheduler.UnobservedTaskException += (sender, eventArgs) =>
{
    try
    {
        logger?.LogError(eventArgs.Exception, "UnobservedTaskException");
        eventArgs.SetObserved();
    }
    catch { /* ignore */ }
};

if (!app.Environment.IsProduction())
{
    app.UseDeveloperExceptionPage();
}

// Configure middleware and routing BEFORE running the app
// (order matters)
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
//app.UseAuthenticationService(app.Configuration);
//app.UseAuthorizationService(app.Configuration);

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

// Single final Run wrapped with try/catch so startup failures are logged
try
{
    logger?.LogInformation("Running {AssemblyName}", "Pertamina Inventory");
    app.Run();
}
catch (Exception ex)
{
    logger?.LogCritical(ex, "Host terminated unexpectedly");
    Console.WriteLine($"Host terminated unexpectedly: {ex}");
    throw;
}