using VaultSharp;
using VaultSharp.V1.Commons;
using VaultSharp.V1.SecretsEngines;
using Microsoft.Extensions.Logging;
using Pertamina.SolutionTemplate.Infrastructure.Logging;
using Pertamina.SolutionTemplate.Application.Services.BackgroundJob;

namespace Pertamina.SolutionTemplate.Infrastructure.Vault;

public class DynamicSecret
{
    private readonly IBackgroundJobService _backgroundJobService;
    public static Secret<UsernamePasswordCredentials> GenerateCredential(IVaultClient vaultClient, string role, string mountPoint)
    {
        var dbCreds = new Secret<UsernamePasswordCredentials>();
        LoggingHelper.CreateLogger()
            .LogInformation($"Generating DB Credential");

        try
        {
            dbCreds = vaultClient.V1.Secrets.Database
               .GetCredentialsAsync(roleName: role, mountPoint: mountPoint).Result;
        }
        catch (Exception err)
        {
            LoggingHelper.CreateLogger().LogError("Error Found " + err.InnerException.Message);
        }

        return dbCreds;
    }

    // public async Task RefreshCredential(string requestId, string cronExpression)
    // {
    //     var jobId = string.Empty;
    //
    //     try
    //     {
    //         await _backgroundJobService.AddRecurringJob()
    //     }
    // }
}

