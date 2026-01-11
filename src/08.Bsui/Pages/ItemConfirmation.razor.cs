using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Pertamina.SolutionTemplate.Bsui.ViewModels;

namespace Pertamina.SolutionTemplate.Bsui.Pages
{
    public partial class ItemConfirmation : ComponentBase
    {
        // --- BAGIAN INI SANGAT PENTING ---
        // Tanpa properti ini, method di bawah tidak akan mengenali 'ViewModel'
        // dan akan menyebabkan error "does not exist in current context".
        [Inject]
        public ItemConfirmationViewModel ViewModel { get; set; } = default!;

        [Inject]
        public ISnackbar Snackbar { get; set; } = default!;
        // ---------------------------------

        protected override async Task OnInitializedAsync()
        {
            // Memuat data saat halaman dibuka
            await ViewModel.LoadPendingItemsAsync();
        }

        private void OnItemSelected(Guid itemId)
        {
            ViewModel.OnItemChanged(itemId);
        }

        private async Task HandleSubmit()
        {
            await ViewModel.SubmitConfirmationAsync();

            if (!string.IsNullOrEmpty(ViewModel.SuccessMessage))
            {
                Snackbar.Add(ViewModel.SuccessMessage, Severity.Success);
            }
            else if (!string.IsNullOrEmpty(ViewModel.ErrorMessage))
            {
                Snackbar.Add(ViewModel.ErrorMessage, Severity.Error);
            }
        }
    }
}