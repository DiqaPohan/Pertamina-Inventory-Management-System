namespace Pertamina.SolutionTemplate.Shared.Services.Authorization.Constants;

public static class Permissions
{
    #region Essential Permissions
    public const string SolutionTemplate_Audit_Index = "kpienterprisecatalog.audit.index";
    public const string SolutionTemplate_Audit_View = "kpienterprisecatalog.audit.view";
    public const string SolutionTemplate_HealthCheck_View = "kpienterprisecatalog.healthcheck.view";
    #endregion Essential Permissions

    #region Business Permissions
    public const string KpiEnterprise_Catalog_Access = "api://50327805-bc3a-4fe1-aa00-ff6633daa5cf/kpiapplicationcatalog.access";
    public const string KpiEnterprise_Catalog_Apitemp = "kpiencatalog.api.audience";
    public const string KpiEnterprise_Catalog_Api = "kpienterprisecatalog.api.audience";
    public const string KpiEnterprise_Catalog_View = "kpienterprisecatalog.catalog.view";
    public const string KpiEnterprise_Catalog_Approval = "kpienterprisecatalog.catalog.approval";
    #endregion Business Permissions

    public static readonly string[] All = new string[]
    {
        #region Essential Permissions
        SolutionTemplate_Audit_Index,
        SolutionTemplate_Audit_View,
        SolutionTemplate_HealthCheck_View,
        #endregion Essential Permissions

        #region Business Permissions
        KpiEnterprise_Catalog_Access,
        KpiEnterprise_Catalog_Apitemp,
        KpiEnterprise_Catalog_Api,
        KpiEnterprise_Catalog_View,
        KpiEnterprise_Catalog_Approval,
        #endregion Business Permissions
    };
}
