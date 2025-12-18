using System.ComponentModel.DataAnnotations;

namespace Pertamina.SolutionTemplate.Shared.Public.Queries.InsertDataWithToken;
public class InsertDataWithToken
{
    [Required]
    public string Token { get; set; }
    [Required]
    public string Application_Name { get; set; }
    [Required]
    public string Application_Area { get; set; }
    [Required]
    public string Application_Type { get; set; }
    [Required]
    public string Description { get; set; }
    public string? Bisnis_Process { get; set; }
    public string? Utilization { get; set; }
    [Required]
    public string Application_Status { get; set; }
    public string? Application_License { get; set; }
    public DateTime? Application_Ats { get; set; }
    public string? Application_Package { get; set; }
    [Required]
    public string User_Management_Integration { get; set; }
    public string? User_Manual_Document { get; set; }
    [Required]
    public string Business_Owner_Nama { get; set; }
    [Required]
    public string Business_Owner_Email { get; set; }
    [Required]
    public string Business_Owner_KBO { get; set; }
    [Required]
    public string Business_Owner_Jabatan { get; set; }
    [Required]
    public string Business_Owner_PIC { get; set; }
    [Required]
    public string Business_Owner_PIC_Email { get; set; }
    [Required]
    public string Developer { get; set; }
    [Required]
    public string Business_Analyst { get; set; }
    public string? Link_Application { get; set; }
    public string? Users { get; set; }
    [Required]
    public DateTime Start_Development { get; set; }
    [Required]
    public DateTime Start_Implementation { get; set; }
    public string? Criticality { get; set; }
    public string? Service_Owner { get; set; }
    public string? Db_Server_Dev { get; set; }
    public string? Db_Name_Dev { get; set; }
    public string? App_Server_Dev { get; set; }
    public string? Db_Server_Prod { get; set; }
    public string? Db_Name_Prod { get; set; }
    public string? App_Server_Prod { get; set; }
    public string? Keterangan { get; set; }
    public string? Company_Code { get; set; }
    [Required]
    public DateTimeOffset Created_Date { get; set; }
    [Required]
    public string Created_By { get; set; }
}
