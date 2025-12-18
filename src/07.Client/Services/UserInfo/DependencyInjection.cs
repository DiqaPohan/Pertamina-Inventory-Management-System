using Microsoft.Extensions.DependencyInjection;

namespace Pertamina.SolutionTemplate.Client.Services.UserInfo;

public static class DependencyInjection
{
    public static IServiceCollection AddUserInfoService(this IServiceCollection services)
    {
        services.AddScoped<UserInfoService>();

        return services;
    }
}
