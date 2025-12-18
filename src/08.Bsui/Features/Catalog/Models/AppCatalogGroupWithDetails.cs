using Pertamina.SolutionTemplate.Shared.Data.Queries.GetSingleData;

namespace Pertamina.SolutionTemplate.Bsui.Features.Catalog.Models;

public class AppCatalogGroup
{
    public string Level_1_lower { get; set; }
    public string Level_1 { get; set; }
    public List<AppCatalogSubgroup> Level_2 { get; set; }
}
public class AppCatalogSubgroup
{
    public string Level_1_lower { get; set; }
    public string Level_1 { get; set; }
    public string Level_2 { get; set; }
    public string Level_2_lower { get; set; }
    public List<GetSingleData> Data { get; set; }
}
public class AppCatalogGroupLevel1
{
    public string Level_1_lower_WithDetails { get; set; }
    public string Level_1_WithDetails { get; set; }
    public List<AppCatalogGroupLevel2> Level_2_WithDetails { get; set; }
}
public class AppCatalogGroupLevel2
{
    public string Level_1_lower_WithDetails { get; set; }
    public string Level_1_WithDetails { get; set; }
    public string Level_2_WithDetails { get; set; }
    public string Level_2_lower_WithDetails { get; set; }
    public List<AppCatalogGroupArea> Data_WithDetails { get; set; }
}
public class AppCatalogGroupArea
{
    public string Area { get; set; }
    public int Total { get; set; }
    public List<AppCatalogGroupStatus> Data { get; set; }
}
public class AppCatalogGroupStatus
{
    public string Status { get; set; }
    public int Total { get; set; }
    public List<GetSingleData> Data { get; set; }
}
