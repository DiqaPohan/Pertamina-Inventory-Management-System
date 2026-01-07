using Microsoft.AspNetCore.Components;
using Pertamina.SolutionTemplate.Bsui.ViewModels;
using System;
using System.Threading.Tasks;

namespace Pertamina.SolutionTemplate.Bsui.Pages
{
    public partial class Inventory : IDisposable
    {
        // 1. Inject ViewModel
        [Inject]
        public InventoryViewModel ViewModel { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            // 2. JEMBATAN UTAMA (Solusi Error StateHasChanged)
            // Kita kasih tahu ViewModel: "Hei, kalau ada perubahan data, tolong panggil method ini"
            ViewModel.OnStateChange = () => InvokeAsync(StateHasChanged);

            // 3. Load Data
            await ViewModel.LoadDataAsync();
        }

        public void Dispose()
        {
            // 4. Bersihkan jembatan saat pindah halaman (mencegah memory leak)
            ViewModel.OnStateChange = null;
        }
    }
}