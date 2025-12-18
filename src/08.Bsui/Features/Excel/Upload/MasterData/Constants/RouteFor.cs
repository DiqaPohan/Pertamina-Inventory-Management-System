namespace Pertamina.SolutionTemplate.Bsui.Features.Excel.MasterData.Constants;

public static class RouteFor
{
    public const string Index = nameof(Catalog);
    public static readonly string MasterData = nameof(MasterData);
    public static readonly string Raw = nameof(Raw);
    public static readonly string Import = nameof(Import);
    public static readonly string Save = nameof(Save);
    public static readonly string Group = $"{MasterData}/{Raw}/Group/";
    public static readonly string LembarKerja = $"{MasterData}/{Raw}/LembarKerja/";
    public static readonly string Validasi = $"{MasterData}/{Raw}/Validasi/";
    public static readonly string ShareFolder = $"{MasterData}/{Raw}/ShareFolder/";
    public static readonly string MasterPowerBi = $"{nameof(MasterData)}/PowerBi/";
}

