using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Pertamina.SolutionTemplate.Bsui.Models;

namespace Pertamina.SolutionTemplate.Bsui.ViewModels
{
    public class ItemConfirmationViewModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ItemConfirmationViewModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public List<InventoryItemDto> PendingItems { get; private set; } = new();
        public InventoryItemDto? SelectedItem { get; private set; }

        public Guid SelectedItemId { get; set; }
        public string TargetRackName { get; set; } = string.Empty;

        // CATATAN: Fitur ini ada di UI, tapi Controller temanmu belum menerimanya.
        // Nanti bisa minta temanmu tambahkan parameter [FromQuery] bool isFull di API.
        public bool IsRackFull { get; set; }

        public bool IsLoading { get; private set; }
        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }

        private HttpClient CreateClient()
        {
            return _httpClientFactory.CreateClient("Pertamina.SolutionTemplate.WebApi");
        }

        public async Task LoadPendingItemsAsync()
        {
            try
            {
                IsLoading = true;
                ErrorMessage = null;
                var client = CreateClient();

                // Endpoint ini SUDAH BENAR sesuai controller temanmu: [HttpGet("pending")]
                var response = await client.GetFromJsonAsync<List<InventoryItemDto>>("api/v1/items/pending");

                if (response != null)
                {
                    PendingItems = response;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Gagal memuat data: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        public void OnItemChanged(Guid itemId)
        {
            SelectedItemId = itemId;
            SelectedItem = PendingItems.FirstOrDefault(i => i.Id == itemId);

            if (SelectedItem != null)
            {
                // Mengambil info rak dari data barang pending
                TargetRackName = SelectedItem.NoRak;
            }
            else
            {
                TargetRackName = string.Empty;
            }
        }

        public async Task SubmitConfirmationAsync()
        {
            if (SelectedItemId == Guid.Empty)
            {
                ErrorMessage = "Pilih barang terlebih dahulu.";
                return;
            }

            try
            {
                IsLoading = true;
                ErrorMessage = null;
                SuccessMessage = null;

                var client = CreateClient();

                // PERBAIKAN LOGIC DISINI SESUAI CONTROLLER TEMANMU:
                // Route: [HttpPut("{id}/confirm-placement")]
                // Parameter: Guid id (dari URL), [FromQuery] string rackId

                // Kita gunakan TargetRackName sebagai nilai rackId.
                // Jika rackId mengandung spasi/karakter khusus, sebaiknya di-encode (Uri.EscapeDataString).
                var rackParam = Uri.EscapeDataString(TargetRackName ?? "");
                var url = $"api/v1/items/{SelectedItemId}/confirm-placement?rackId={rackParam}";

                // Method harus PUT, dan body kosong (null) karena data lewat URL
                var response = await client.PutAsync(url, null);

                if (response.IsSuccessStatusCode)
                {
                    SuccessMessage = "Konfirmasi berhasil! Barang sekarang aktif.";

                    // Reset
                    SelectedItemId = Guid.Empty;
                    TargetRackName = string.Empty;
                    IsRackFull = false;

                    await LoadPendingItemsAsync();
                }
                else
                {
                    var msg = await response.Content.ReadAsStringAsync();
                    ErrorMessage = $"Gagal: {response.StatusCode}. {msg}";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error koneksi: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}