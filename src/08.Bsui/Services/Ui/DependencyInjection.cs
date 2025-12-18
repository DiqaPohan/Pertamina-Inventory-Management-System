using Pertamina.SolutionTemplate.Bsui.Services.UI.MudBlazorUI;

namespace Pertamina.SolutionTemplate.Bsui.Services.UI;

public static class DependencyInjection
{
    public static IServiceCollection AddUIService(this IServiceCollection services)
    {
        services.AddMudBlazorUIService();

        return services;
    }
}
