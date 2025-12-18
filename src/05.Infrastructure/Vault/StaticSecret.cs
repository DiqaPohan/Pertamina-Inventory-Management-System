using Microsoft.Extensions.Logging;
using Pertamina.SolutionTemplate.Infrastructure.Logging;
using VaultSharp;
using VaultSharp.V1.Commons;

namespace Pertamina.SolutionTemplate.Infrastructure.Vault;

public class StaticSecret
{
    public static Secret<SecretData> GetSecret(IVaultClient vaultClient, string path, string mountPoint)
    {
        Console.WriteLine($"Got secret = {path} {mountPoint}"); //remove after testing
        var kv2Secret = new Secret<SecretData>();
        //LoggingHelper.CreateLogger()
        // .LogInformation($"Get  DB Credential");

        try
        {
            kv2Secret = vaultClient.V1.Secrets.KeyValue.V2.ReadSecretAsync(path: path, mountPoint: mountPoint).Result;
        }
        catch (Exception err)
        {
            LoggingHelper.CreateLogger().LogError("Error Found " + err.InnerException.Message);

        }

        return kv2Secret;
    }
}
