//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Pertamina.SolutionTemplate.Application.Services.Storage;
//using Pertamina.SolutionTemplate.Shared.Common.Constants;

//namespace Pertamina.SolutionTemplate.Infrastructure.Storage.AzureBlob;

//public static class DependencyInjection
//{
//    public static IServiceCollection AddAzureBlobStorageService(this IServiceCollection services, IConfiguration configuration, IHealthChecksBuilder healthChecksBuilder)
//    {
//        services.Configure<AzureBlobStorageOptions>(configuration.GetSection(AzureBlobStorageOptions.SectionKey));
//        services.AddSingleton<IStorageService, AzureBlobStorageService>();

//        var azureBlobStorageOptions = configuration.GetSection(AzureBlobStorageOptions.SectionKey).Get<AzureBlobStorageOptions>();

//        healthChecksBuilder.AddAzureBlobStorage(
//            connectionString: azureBlobStorageOptions.ConnectionString,
//            containerName: azureBlobStorageOptions.ContainerName,
//            name: $"{nameof(Storage)} {CommonDisplayTextFor.Service} ({nameof(AzureBlob)})");

//        return services;
//    }
//}
