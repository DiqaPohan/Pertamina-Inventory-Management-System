using FluentValidation;
using Pertamina.SolutionTemplate.Shared.Common.Attributes;
using Pertamina.SolutionTemplate.Shared.Common.Constants;

namespace Pertamina.SolutionTemplate.Shared.Data.Commands.CreateData;
public class EditCatalogRequest
{
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string? Tipe_Request { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string? Code_Apps { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string? Code_Apps_Update { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string Application_Name { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string Application_Area { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string Application_Type { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string Description { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string? Application_Data { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string? Diagram_Context { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string? Diagram_Physical { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string? Capability_Level_1 { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string? Capability_Level_2 { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string? Bisnis_Process { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string? Utilization { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string Application_Status { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string? Application_License { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public DateTime Application_Ats { get; set; } = new DateTime();
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string? Application_Package { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string User_Management_Integration { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string? User_Manual_Document { get; set; }

    [OpenApiContentType(ContentTypes.TextPlain)]
    public string Business_Owner_Nama { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string Business_Owner_Email { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string Business_Owner_KBO { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string Business_Owner_Jabatan { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string Business_Owner_PIC { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string Business_Owner_PIC_Email { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string Developer { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string Business_Analyst { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string? Link_Application { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string? Users { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public DateTime Start_Development { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string? Start_Development_Str { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public DateTime Start_Implementation { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string? Start_Implementation_Str { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string? Criticality { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string? Service_Owner { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string? Db_Server_Dev { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string? Db_Name_Dev { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string? App_Server_Dev { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string? Db_Server_Prod { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string? Db_Name_Prod { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string? App_Server_Prod { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string? Keterangan { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string? IsApproved { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string? Source { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string? Approved_Status { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string? Company_Code { get; set; }
    public DateTimeOffset Created_Date { get; set; }
    [OpenApiContentType(ContentTypes.TextPlain)]
    public string Created_By { get; set; }
}

public class EditCatalogRequestValidator : AbstractValidator<EditCatalogRequest>
{

    public EditCatalogRequestValidator()
    {
        _ = RuleFor(x => x.Application_Name)
            .NotEmpty();

        _ = RuleFor(x => x.Application_Area)
          .NotEmpty();

        _ = RuleFor(x => x.Application_Type)
          .NotEmpty();

        _ = RuleFor(x => x.Description)
          .NotEmpty();

        _ = RuleFor(x => x.Application_Status)
          .NotEmpty();

        _ = RuleFor(x => x.User_Management_Integration)
          .NotEmpty();

        _ = RuleFor(x => x.Business_Owner_Nama)
          .NotEmpty();

        _ = RuleFor(x => x.Business_Owner_Email)
          .NotEmpty();

        _ = RuleFor(x => x.Business_Owner_KBO)
          .NotEmpty();

        _ = RuleFor(x => x.Business_Owner_Jabatan)
          .NotEmpty();

        _ = RuleFor(x => x.Business_Owner_PIC)
          .NotEmpty();

        _ = RuleFor(x => x.Business_Owner_PIC_Email)
          .NotEmpty();

        _ = RuleFor(x => x.Developer)
          .NotEmpty();

        _ = RuleFor(x => x.Business_Analyst)
          .NotEmpty();
    }
}
