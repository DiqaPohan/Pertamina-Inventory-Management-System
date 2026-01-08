using MudBlazor;

namespace Pertamina.SolutionTemplate.Bsui.Models
{
    public class ActivityLogDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime Timestamp { get; set; }
        public string UserName { get; set; } = "";
        public string ActionType { get; set; } = ""; // Masuk, Keluar, Pinjam
        public string ItemName { get; set; } = "";
        public string Details { get; set; } = ""; // Contoh: "Menambahkan 50 Pcs"

        // Helper untuk warna badge di UI
        public Color BadgeColor => ActionType switch
        {
            "Barang Masuk" => Color.Success,
            "Barang Keluar" => Color.Error,
            "Peminjaman" => Color.Warning,
            "Pengembalian" => Color.Info,
            _ => Color.Default
        };

        public string Icon => ActionType switch
        {
            "Barang Masuk" => Icons.Material.Filled.ArrowDownward, // Masuk ke gudang
            "Barang Keluar" => Icons.Material.Filled.ArrowUpward,  // Keluar dari gudang
            "Peminjaman" => Icons.Material.Filled.AssignmentInd,
            "Pengembalian" => Icons.Material.Filled.AssignmentReturn,
            _ => Icons.Material.Filled.Info
        };
    }
}
