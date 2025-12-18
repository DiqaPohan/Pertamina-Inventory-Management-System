
using Pertamina.SolutionTemplate.Shared.Common.Responses;

namespace Pertamina.SolutionTemplate.Shared.Data.Queries.GetData;
public class GetDataResponse : Response
{
    public string? Code_Apps { get; set; }
    public string? Application_Area { get; set; }
    public string? Application_Name { get; set; }
    public string? Application_Type { get; set; }
    public string? Description { get; set; }
    public string? Capability { get; set; }
    public string? Application_Data { get; set; }
    public string? Diagram_Context { get; set; }
    public string? Diagram_Physical { get; set; }
    public string? Creator { get; set; }
    public string? Level_1 { get; set; }
    public string? Level_2 { get; set; }
    public string? Utilization { get; set; }
    public string? Application_Status { get; set; }
    public string? Application_License { get; set; }
    public string? Application_Ats { get; set; }
    public string? Application_Package { get; set; }
    public string? User_Management_Integration { get; set; }
    public string? User_Manual_Document { get; set; }
    public string? Business_Owner_PIC { get; set; }
    public string? Business_Owner { get; set; }
    public string? Developer { get; set; }
    public string? Link_Application { get; set; }
    public string? Users { get; set; }
    public string? Start_Development { get; set; }
    public string? Start_Implementation { get; set; }
    public string? Criticality { get; set; }
    public string? Holding_Area { get; set; }
    public DateTimeOffset? Created { get; set; }
    public string? CreatedBy { get; set; }
}
