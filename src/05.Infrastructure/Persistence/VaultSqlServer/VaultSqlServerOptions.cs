namespace Pertamina.SolutionTemplate.Infrastructure.Persistence.VaultSqlServer;
public class VaultSqlServerOptions
{
    public static readonly string SectionKey = $"{nameof(Persistence)}:{nameof(VaultSqlServer)}";

    public string URL { get; set; } = default!;
    public string Namespace { get; set; } = default!;
    public string RoleID { get; set; } = default!;
    public string SecretID { get; set; } = default!;
    public string SecretPath { get; set; } = default!;
    public string MountPoint { get; set; } = default!;

}
