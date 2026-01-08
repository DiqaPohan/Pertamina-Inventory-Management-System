using Microsoft.AspNetCore.Components;
using Pertamina.SolutionTemplate.Bsui.ViewModels;


namespace Pertamina.SolutionTemplate.Bsui.Pages
{
    public partial class Dashboard : ComponentBase, IDisposable
    {
        [Inject]
        public DashboardViewModel ViewModel { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            // Subscribe ke event perubahan state di ViewModel
            ViewModel.OnStateChange = () => InvokeAsync(StateHasChanged);

            // Load data dummy
            await ViewModel.LoadDashboardDataAsync();
        }

        public void Dispose()
        {
            ViewModel.OnStateChange = null;
        }
    }
}
