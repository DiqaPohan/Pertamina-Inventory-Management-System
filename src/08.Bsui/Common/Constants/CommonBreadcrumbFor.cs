using MudBlazor;
using Pertamina.SolutionTemplate.Shared.Common.Constants;

namespace Pertamina.SolutionTemplate.Bsui.Common.Constants;

public static class CommonBreadcrumbFor
{
    public static BreadcrumbItem Active(string text)
    {
        return new(text, href: null, disabled: true);
    }

    public static readonly BreadcrumbItem Home = new(CommonDisplayTextFor.Home, href: CommonRouteFor.Index);
    public static readonly BreadcrumbItem Catalog = new(CommonDisplayTextFor.Catalog, href: Features.Catalog.Constants.RouteFor.Index);
    public static readonly BreadcrumbItem BuildingBlock = new(CommonDisplayTextFor.BuildingBlock, href: Features.Catalog.Constants.RouteFor.BuildingBlock);
    public static readonly BreadcrumbItem ListApps = new(CommonDisplayTextFor.ListApps, href: Features.Catalog.Constants.RouteFor.ListApps);
    public static readonly BreadcrumbItem RequestAndApproval = new(CommonDisplayTextFor.RequestAndApproval, href: Features.Catalog.Constants.RouteFor.Request);
}
