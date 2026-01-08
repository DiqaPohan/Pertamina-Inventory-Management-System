using MudBlazor;
using Pertamina.SolutionTemplate.Bsui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pertamina.SolutionTemplate.Bsui.ViewModels
{
    public class DashboardViewModel
    {
        public Action? OnStateChange { get; set; }

        // --- STATISTIK (Cards) ---
        public int TotalItemsStored { get; private set; }
        public int ItemsInThisMonth { get; private set; }
        public int ItemsOutThisMonth { get; private set; }
        public int ItemsOnLoan { get; private set; }

        // --- DATA LOGS ---
        public List<ActivityLogDto> RecentLogs { get; private set; } = new();
        public bool IsLoading { get; private set; } = true;

        // --- METHODS ---

        public async Task LoadDashboardDataAsync()
        {
            IsLoading = true;
            NotifyStateChanged();

            // Simulasi delay API backend
            await Task.Delay(500);

            // 1. Generate Statistik Dummy
            TotalItemsStored = 1250;     // Total barang di gudang
            ItemsInThisMonth = 345;      // Barang masuk bulan ini
            ItemsOutThisMonth = 120;     // Barang keluar bulan ini
            ItemsOnLoan = 45;            // Sedang dipinjam

            // 2. Generate Dummy Logs (Max 20 sesuai request)
            GenerateDummyLogs();

            IsLoading = false;
            NotifyStateChanged();
        }

        private void GenerateDummyLogs()
        {
            var users = new[] { "Budi Santoso", "Siti Aminah", "Joko Anwar", "Rina Marlina" };
            var items = new[] { "Helm Safety Red", "Rompi Proyek", "Sepatu Boots", "Sarung Tangan", "Genset 5000W" };
            var actions = new[] { "Barang Masuk", "Barang Keluar", "Peminjaman", "Pengembalian" };

            var random = new Random();
            RecentLogs = new List<ActivityLogDto>();

            for (int i = 0; i < 20; i++)
            {
                var action = actions[random.Next(actions.Length)];
                var qty = random.Next(1, 50);

                RecentLogs.Add(new ActivityLogDto
                {
                    Timestamp = DateTime.Now.AddHours(-i * 2).AddMinutes(-random.Next(0, 60)),
                    UserName = users[random.Next(users.Length)],
                    ActionType = action,
                    ItemName = items[random.Next(items.Length)],
                    Details = FormatDetails(action, qty)
                });
            }
        }

        private string FormatDetails(string action, int qty)
        {
            return action switch
            {
                "Barang Masuk" => $"Menambahkan stok +{qty}",
                "Barang Keluar" => $"Mengurangi stok -{qty} (Rusak/Musnah)",
                "Peminjaman" => $"Meminjam {qty} unit untuk Proyek A",
                "Pengembalian" => $"Mengembalikan {qty} unit",
                _ => "-"
            };
        }

        private void NotifyStateChanged() => OnStateChange?.Invoke();

    }
}
