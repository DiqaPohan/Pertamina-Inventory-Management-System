using Microsoft.AspNetCore.Components;
using Pertamina.SolutionTemplate.Bsui.ViewModels;
using System;
using System.Threading.Tasks;

namespace Pertamina.SolutionTemplate.Bsui.Pages
{
    public partial class Inventory : IDisposable
    {
        [Inject]
        public InventoryViewModel ViewModel { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            ViewModel.OnStateChange = () => InvokeAsync(StateHasChanged);

            // Call InitializeAsync to load Racks AND Data
            await ViewModel.InitializeAsync();
        }

        public void Dispose()
        {
            if (ViewModel != null)
            {
                ViewModel.OnStateChange = null;
            }
        }
    }
}