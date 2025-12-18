using Microsoft.Extensions.Logging;
using VaultSharp;
using VaultSharp.V1.AuthMethods.AppRole;
using VaultSharp.V1.AuthMethods;
using VaultSharp.V1.AuthMethods.Token;
using Pertamina.SolutionTemplate.Infrastructure.Logging;

namespace Pertamina.SolutionTemplate.Infrastructure.Vault;
public class ClientConnect
{
    public IVaultClient UseTokenAuth(string vaultAddress, string token)
    {
        IAuthMethodInfo authMethod = new TokenAuthMethodInfo(token);
        var vaultClientSettings = new VaultClientSettings(vaultAddress, authMethod);
        IVaultClient vaultClient = new VaultClient(vaultClientSettings);

        return vaultClient;
    }

    public static IVaultClient UseAppRoleAuth(string vaultAddress, string roleId, string secretId, string vaultNamespace, string servicesName)
    {
        LoggingHelper.CreateLogger()
            .LogInformation($"Service {servicesName} Try To Connecting using AppRole to Vault URL: {vaultAddress}");
        IAuthMethodInfo authMethod = new AppRoleAuthMethodInfo(roleId, secretId);
        var vaultClientSettings = new VaultClientSettings(vaultAddress, authMethod)
        {
            Namespace = vaultNamespace
        };
        IVaultClient vaultClient = new VaultClient(vaultClientSettings);

        return vaultClient;

    }

}
