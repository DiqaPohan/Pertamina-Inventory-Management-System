using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using MudBlazor;
using Pertamina.SolutionTemplate.Bsui.Models;
using Pertamina.SolutionTemplate.Shared.Common.Enums;
using Pertamina.SolutionTemplate.Shared.Common.Responses;
using Shared.Common.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Pertamina.SolutionTemplate.Bsui.ViewModels
{
    public class InventoryViewModel
    {
        private readonly ISnackbar _snackbar;
        private readonly IJSRuntime _jsRuntime;
        private readonly IHttpClientFactory _httpClientFactory;
        private const string ApiClientName = "Pertamina.SolutionTemplate.WebApi";
        public Action? OnStateChange { get; set; }

        public InventoryViewModel(ISnackbar snackbar, IJSRuntime jsRuntime, IHttpClientFactory httpClientFactory)
        {
            _snackbar = snackbar;
            _jsRuntime = jsRuntime;
            _httpClientFactory = httpClientFactory;
        }

        // --- UI State ---
        public string SearchString { get; set; } = "";
        public bool IsAddDialogOpen { get; set; }
        public DialogOptions DialogOptions { get; } = new() { MaxWidth = MaxWidth.Large, FullWidth = true };
        public bool IsLoading { get; private set; } = false;

        // --- Form Properties (Full Properties untuk memicu UI Update) ---
        public Guid? EditingItemId { get; set; }

        private string _newItemName = "";
        public string NewItemName
        {
            get => _newItemName;
            set { _newItemName = value; NotifyStateChanged(); }
        }

        private int _newItemQty = 1;
        public int NewItemQty
        {
            get => _newItemQty;
            set { _newItemQty = value; NotifyStateChanged(); }
        }

        private string _selectedRackId = "";
        public string SelectedRackId
        {
            get => _selectedRackId;
            set { _selectedRackId = value; NotifyStateChanged(); }
        }

        private string _newItemUnit = "pcs";
        public string NewItemUnit
        {
            get => _newItemUnit;
            set { _newItemUnit = value; NotifyStateChanged(); }
        }

        private ItemCategory _newItemCategory = ItemCategory.Light;
        public ItemCategory NewItemCategory
        {
            get => _newItemCategory;
            set { _newItemCategory = value; NotifyStateChanged(); }
        }

        public DateTime? NewItemExpDate { get; set; }

        // --- Image Properties (Punya lu yang lama gue amanin) ---
        public string ImagePreview { get; set; } = "";
        public byte[]? UploadedImageBytes { get; private set; }
        public string UploadedImageBase64 { get; set; } = "";
        public bool IsImageLoaded { get; private set; } = false;

        // --- Logic Properties ---
        public bool IsRakAutoFilled { get; set; } = false;

        // --- Data Lists ---
        public List<InventoryItemDto> Items { get; private set; } = new();
        public List<RackDto> Racks { get; private set; } = new();

        // Validasi Form (Ini yang dipake buat Enable/Disable tombol)
        public bool IsFormValid => !string.IsNullOrWhiteSpace(NewItemName) &&
                                   !string.IsNullOrWhiteSpace(SelectedRackId) &&
                                   NewItemQty > 0;

        public IEnumerable<InventoryItemDto> FilteredItems
        {
            get
            {
                if (string.IsNullOrWhiteSpace(SearchString)) return Items;
                return Items.Where(x => x.Nama.Contains(SearchString, StringComparison.OrdinalIgnoreCase) ||
                                        (x.NoRak != null && x.NoRak.Contains(SearchString, StringComparison.OrdinalIgnoreCase)));
            }
        }

        // --- Initialization ---

        public async Task InitializeAsync()
        {
            IsLoading = true;
            NotifyStateChanged();

            await LoadRacksAsync();
            await LoadDataAsync();

            IsLoading = false;
            NotifyStateChanged();
        }

        public async Task LoadRacksAsync()
        {
            try
            {
                var client = _httpClientFactory.CreateClient(ApiClientName);
                var response = await client.GetFromJsonAsync<List<RackDto>>("api/v1/racks");
                if (response != null) Racks = response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error load racks: {ex.Message}");
            }
        }

        public async Task LoadDataAsync()
        {
            try
            {
                var client = _httpClientFactory.CreateClient(ApiClientName);
                var response = await client.GetAsync("api/v1/items?PageNumber=1&PageSize=100");

                if (response.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var result = await response.Content.ReadFromJsonAsync<PaginatedListResponse<ItemApiDto>>(options);

                    if (result?.Items != null)
                    {
                        Items = result.Items.Select(x => new InventoryItemDto
                        {
                            Id = x.Id,
                            Nama = x.Name ?? "N/A",
                            Stok = x.TotalStock,
                            ItemCategory = x.Category,
                            NoRak = x.RackId ?? "N/A",
                            Satuan = x.Unit ?? "pcs",
                            ImageUrl = x.ImageUrl,
                            ExpDate = x.ExpiryDate
                        }).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                _snackbar.Add($"Gagal memuat data: {ex.Message}", Severity.Error);
            }
        }

        // --- Auto Suggestion & AutoFill ---

        public async Task<IEnumerable<string>> SearchExistingItems(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Items.Select(x => x.Nama).Distinct().OrderBy(x => x);

            await Task.Delay(50);
            return Items
                .Where(x => x.Nama.Contains(value, StringComparison.OrdinalIgnoreCase))
                .Select(x => x.Nama)
                .Distinct()
                .OrderBy(x => x);
        }

        public void OnItemNameChanged(string value)
        {
            NewItemName = value;
            var existingItem = Items.FirstOrDefault(x => x.Nama.Equals(value, StringComparison.OrdinalIgnoreCase));

            if (existingItem != null)
            {
                SelectedRackId = existingItem.NoRak; // Otomatis milih Rak yang sama
                IsRakAutoFilled = true;
                NewItemCategory = existingItem.ItemCategory;
                NewItemUnit = existingItem.Satuan;

                if (!string.IsNullOrEmpty(existingItem.ImageUrl))
                    ImagePreview = existingItem.ImageUrl;
            }
            else
            {
                if (IsRakAutoFilled)
                {
                    IsRakAutoFilled = false;
                    SelectedRackId = "";
                }
            }
            NotifyStateChanged();
        }

        // --- Image Handling ---

        public async Task TriggerFileInput() => await _jsRuntime.InvokeVoidAsync("triggerFileInput");

        public async Task HandleFileSelected(InputFileChangeEventArgs e)
        {
            var file = e.File;
            if (file == null) return;

            if (!file.ContentType.StartsWith("image/"))
            {
                _snackbar.Add("File harus berupa gambar", Severity.Warning);
                return;
            }

            try
            {
                const long maxReadBytes = 5 * 1024 * 1024;
                using var stream = file.OpenReadStream(maxAllowedSize: maxReadBytes);
                using var ms = new MemoryStream();
                await stream.CopyToAsync(ms);
                var bytes = ms.ToArray();

                UploadedImageBytes = bytes;
                UploadedImageBase64 = Convert.ToBase64String(bytes);
                ImagePreview = $"data:{file.ContentType};base64,{UploadedImageBase64}";
                IsImageLoaded = true;

                NotifyStateChanged();
            }
            catch (Exception ex)
            {
                _snackbar.Add("Gagal memproses gambar", Severity.Error);
            }
        }

        public void ClearImage()
        {
            ImagePreview = "";
            UploadedImageBase64 = "";
            UploadedImageBytes = null;
            IsImageLoaded = false;
            NotifyStateChanged();
        }

        // --- CRUD Logic (Tetap pake JSON biar sinkron sama Command lu) ---

        public async Task HandleSubmit()
        {
            if (!IsFormValid) return;

            try
            {
                IsLoading = true;
                NotifyStateChanged();
                var client = _httpClientFactory.CreateClient(ApiClientName);

                // Kirim ImageUrl sebagai Base64 string ke Backend
                var payload = new
                {
                    Id = EditingItemId,
                    Name = NewItemName,
                    RackId = SelectedRackId,
                    Category = (int)NewItemCategory,
                    TotalStock = NewItemQty,
                    Unit = NewItemUnit,
                    ImageUrl = ImagePreview,
                    ExpiryDate = NewItemExpDate
                };

                HttpResponseMessage response;
                if (EditingItemId.HasValue)
                {
                    response = await client.PutAsJsonAsync($"api/v1/items/{EditingItemId.Value}", payload);
                }
                else
                {
                    response = await client.PostAsJsonAsync("api/v1/items", payload);
                }

                if (response.IsSuccessStatusCode)
                {
                    _snackbar.Add(EditingItemId.HasValue ? "Barang diperbarui!" : "Barang disimpan!", Severity.Success);
                    IsAddDialogOpen = false;
                    ResetForm();
                    await LoadDataAsync();
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    _snackbar.Add($"Gagal menyimpan: {error}", Severity.Error);
                }
            }
            catch (Exception ex)
            {
                _snackbar.Add($"Error: {ex.Message}", Severity.Error);
            }
            finally
            {
                IsLoading = false;
                NotifyStateChanged();
            }
        }

        public async Task DeleteItem(Guid id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient(ApiClientName);
                var response = await client.DeleteAsync($"api/v1/items/{id}");
                if (response.IsSuccessStatusCode)
                {
                    _snackbar.Add("Barang dihapus!", Severity.Success);
                    await LoadDataAsync();
                }
            }
            catch (Exception ex)
            {
                _snackbar.Add("Gagal hapus barang", Severity.Error);
            }
        }

        // --- Dialog Helpers ---

        public void OpenAddDialog()
        {
            ResetForm();
            IsAddDialogOpen = true;
            NotifyStateChanged();
        }

        public void OpenEditDialog(InventoryItemDto item)
        {
            EditingItemId = item.Id;
            _newItemName = item.Nama;
            _newItemQty = item.Stok;
            _selectedRackId = item.NoRak;
            _newItemCategory = item.ItemCategory;
            _newItemUnit = item.Satuan;
            NewItemExpDate = item.ExpDate;
            ImagePreview = item.ImageUrl ?? "";
            IsRakAutoFilled = false;
            IsAddDialogOpen = true;
            NotifyStateChanged();
        }

        public void CancelDialog()
        {
            IsAddDialogOpen = false;
            NotifyStateChanged();
        }

        private void ResetForm()
        {
            EditingItemId = null;
            _newItemName = "";
            _newItemQty = 1;
            _selectedRackId = "";
            _newItemUnit = "pcs";
            NewItemExpDate = null;
            _newItemCategory = ItemCategory.Light;
            ImagePreview = "";
            UploadedImageBase64 = "";
            UploadedImageBytes = null;
            IsRakAutoFilled = false;
            NotifyStateChanged();
        }

        public void NotifyStateChanged() => OnStateChange?.Invoke();

        private class ItemApiDto
        {
            public Guid Id { get; set; }
            public string? Name { get; set; }
            public int TotalStock { get; set; }
            public string? Unit { get; set; }
            public ItemCategory Category { get; set; }
            public string? RackId { get; set; }
            public string? ImageUrl { get; set; }
            public DateTime? ExpiryDate { get; set; }
        }

        public class RackDto
        {
            public string RackId { get; set; } = "";
            public RackStatus Status { get; set; }
        }
    }
}