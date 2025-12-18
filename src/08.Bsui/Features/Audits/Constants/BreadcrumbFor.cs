using MudBlazor;
using Pertamina.SolutionTemplate.Shared.Audits.Constants;

namespace Pertamina.SolutionTemplate.Bsui.Features.Audits.Constants;

public static class BreadcrumbFor
{
    public static readonly BreadcrumbItem Index = new(DisplayTextFor.Audits, href: RouteFor.Index);
}
