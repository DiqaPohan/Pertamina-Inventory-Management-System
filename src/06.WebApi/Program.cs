using System.Text.Json.Serialization;
using FluentValidation.AspNetCore;
using Pertamina.SolutionTemplate.Application;
using Pertamina.SolutionTemplate.Infrastructure;
using Pertamina.SolutionTemplate.Infrastructure.Logging;
using Pertamina.SolutionTemplate.Infrastructure.Persistence;
using Pertamina.SolutionTemplate.Shared;
using Pertamina.SolutionTemplate.Shared.Common.Constants;
using Pertamina.SolutionTemplate.WebApi.Common.Filters.ApiException;
using Pertamina.SolutionTemplate.WebApi.Common.ModelBindings;
using Pertamina.SolutionTemplate.WebApi.Services;
using Pertamina.SolutionTemplate.WebApi.Services.BackEnd;
using Pertamina.SolutionTemplate.WebApi.Services.Documentation;

Console.WriteLine($"Starting {CommonValueFor.EntryAssemblySimpleName}...");

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseLoggingService();
builder.Services.AddShared(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddWebApi(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddApiVersioning();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ApiExceptionFilterAttribute());
    options.ModelBinderProviders.Insert(0, new JsonModelBinderProvider());
})
.AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

var app = builder.Build();

using var scope = app.Services.CreateScope();
await scope.ServiceProvider.ApplyDatabaseMigrationAsync<Program>();

var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("Running {AssemblyName}", CommonValueFor.EntryAssemblySimpleName);

if (!app.Environment.IsProduction())
{
    app.UseDeveloperExceptionPage();
}

app.UseBackEndService(builder.Configuration);
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseInfrastructure(builder.Configuration);
app.UseDocumentationService(builder.Configuration);
app.MapControllers();
app.Run();
