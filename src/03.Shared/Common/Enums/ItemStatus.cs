namespace Pertamina.SolutionTemplate.Shared.Common.Enums;

public enum ItemStatus
{
    Pending = 0, // Admin sudah set barang & RackId, tapi barang belum di rak
    Active = 1   // Pegawai sudah scan QR rak dan konfirmasi barang sudah ditaruh
}