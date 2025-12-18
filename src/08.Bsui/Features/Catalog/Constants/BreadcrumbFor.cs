using MudBlazor;
namespace Pertamina.SolutionTemplate.Bsui.Features.Catalog.Constants;

public static class BreadcrumbFor
{
    public static readonly BreadcrumbItem Index = new("Catalog", href: RouteFor.Index);
    public static readonly BreadcrumbItem ByBuildingBlock = new("By Building Block", href: RouteFor.BuildingBlock);
    public static readonly BreadcrumbItem ByListApps = new("By List Of Applications", href: RouteFor.Tabular);
    public static readonly BreadcrumbItem Request = new("Request And Approval", href: RouteFor.Request);
}
