namespace Pertamina.SolutionTemplate.Bsui.Features.Catalog.Constants;

public static class RouteFor
{
    public const string Index = nameof(Catalog);
    public static readonly string Index1 = $"{nameof(Catalog)}/{nameof(Index1)}";
    public static readonly string Index2 = $"{nameof(Catalog)}/{nameof(Index2)}";
    public static readonly string Tabular = $"{nameof(Catalog)}/{nameof(Tabular)}";
    public static readonly string BuildingBlock = $"{nameof(Catalog)}/{nameof(BuildingBlock)}";
    public static readonly string ListApps = $"{nameof(Catalog)}/{nameof(ListApps)}";
    public static readonly string Request = $"{nameof(Catalog)}/{nameof(Request)}";
    public static readonly string TabularNew = $"{nameof(Catalog)}/{nameof(TabularNew)}";
    public const string AddDraft = $"{nameof(Catalog)}/{nameof(AddDraft)}";
    public const string Edit = $"{nameof(Catalog)}/{nameof(Edit)}";
    public const string AddDraftHistoricalApplicationPhase = $"{nameof(Catalog)}/{nameof(AddDraftHistoricalApplicationPhase)}";
    //public const string ViewDraftCatalog = $"{nameof(Catalog)}/{nameof(ViewDraftCatalog)}";
    //public const string ViewDraftHistoricalApplicationPhase = $"{nameof(Catalog)}/{nameof(ViewDraftHistoricalApplicationPhase)}";
    public static string Details(string id, string id1)
    {
        return $"{nameof(Catalog)}/{nameof(Details)}/{id}/{id1}";
    }
    public static string ViewDraftCatalog(string id)
    {
        return $"{nameof(Catalog)}/{nameof(ViewDraftCatalog)}/{id}";
    }
    public static string ViewDraftHistoricalApplicationPhase(string id)
    {
        return $"{nameof(Catalog)}/{nameof(ViewDraftHistoricalApplicationPhase)}/{id}";
    }
}
