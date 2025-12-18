using Microsoft.AspNetCore.Components;

namespace Pertamina.SolutionTemplate.Bsui.Services.Geolocation.Components;

public partial class GeolocationError
{
    [Parameter]
    public string ErrorMessage { get; set; } = default!;
}
