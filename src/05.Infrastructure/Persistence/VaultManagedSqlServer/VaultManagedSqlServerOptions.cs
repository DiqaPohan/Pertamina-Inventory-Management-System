namespace Pertamina.SolutionTemplate.Infrastructure.Persistence.VaultManagedSqlServer;
public class VaultManagedSqlServerOptions
{
    public static readonly string SectionKey = $"{nameof(Persistence)}:{nameof(VaultManagedSqlServer)}";

    public string URL { get; set; } = default!;
    public string Namespace { get; set; } = default!;
    public string RoleID { get; set; } = default!;
    public string SecretID { get; set; } = default!;
    public string DBRoleName { get; set; } = default!;
    public string MountPoint { get; set; } = default!;
    public string DBName { get; set; } = default!;
    public string DBServer { get; set; } = default!;

}
