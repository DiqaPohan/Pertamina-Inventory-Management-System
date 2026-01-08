using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using MudBlazor;
using Pertamina.SolutionTemplate.Bsui.Models; // Pastikan namespace DTO benar
using Shared.Common.Enums;
using Pertamina.SolutionTemplate.Shared.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pertamina.SolutionTemplate.Bsui.ViewModels
{
    public class InventoryViewModel
    {
        private readonly ISnackbar _snackbar;
        private readonly IJSRuntime _jsRuntime;


        // Ini adalah "Trigger" pengganti StateHasChanged
        public Action? OnStateChange { get; set; }

        public InventoryViewModel(ISnackbar snackbar, IJSRuntime jsRuntime)
        {
            _snackbar = snackbar;
            _jsRuntime = jsRuntime;
        }

        // --- STATE UI (Properties) ---
        public string SearchString { get; set; } = "";
        public bool IsAddDialogOpen { get; set; }
        public DialogOptions DialogOptions { get; } = new() { MaxWidth = MaxWidth.Medium, FullWidth = true };

        // --- FORM FIELDS ---
        public string NewItemName { get; set; } = "";
        public int NewItemQty { get; set; } = 1;
        public string NewItemRak { get; set; } = "";
        public DateTime? NewItemExpDate { get; set; }
        public string ImagePreview { get; set; } = "";
        public string UploadedImageBase64 { get; set; } = "";
        public bool IsRakAutoFilled { get; set; } = false;
        public ItemCategory NewItemCategory { get; set; } = ItemCategory.Light;

        // --- DATA ---
        public List<InventoryItemDto> Items { get; private set; } = new();

        public bool IsFormValid => !string.IsNullOrWhiteSpace(NewItemName) && NewItemQty > 0 && !string.IsNullOrWhiteSpace(NewItemRak);

        public IEnumerable<InventoryItemDto> FilteredItems
        {
            get
            {
                if (string.IsNullOrWhiteSpace(SearchString)) return Items;
                return Items.Where(x => x.Nama.Contains(SearchString, StringComparison.OrdinalIgnoreCase) ||
                                        x.NoRak.Contains(SearchString, StringComparison.OrdinalIgnoreCase));
            }
        }

        public async Task<IEnumerable<string>> SearchExistingItems(string value)
        {
            await Task.Delay(50);

            if (string.IsNullOrWhiteSpace(value))
                return Items.Select(x => x.Nama).Distinct().OrderBy(x => x);

            var valueLower = value.ToLower();
            return Items
                .Where(x => x.Nama.ToLower().Contains(valueLower))
                .Select(x => x.Nama)
                .Distinct()
                .OrderBy(x => x)
                .ToList();
        }

        // --- LOGIC METHODS ---

        public async Task LoadDataAsync()
        {
            await Task.Delay(100);

            // [UPDATE] Tambahkan data dummy Category
            Items = new List<InventoryItemDto>
            {
                new InventoryItemDto
                {
                    Nama = "Helm Safety Red",
                    NoRak = "A-01",
                    Stok = 25,
                    ItemCategory = ItemCategory.Light, // [BARU]
                    ImageUrl = "https://images.unsplash.com/photo-1590650153855-d9e808231d41?q=80&w=400",
                    ExpDate = DateTime.Now.AddDays(15)
                },
                new InventoryItemDto
                {
                    Nama = "Genset Portable 5000W",
                    NoRak = "Z-99",
                    Stok = 2,
                    ItemCategory = ItemCategory.Heavy, // [BARU]
                    ImageUrl = "https://images.unsplash.com/photo-1581094288338-2314dddb79a7?q=80&w=400",
                    ExpDate = null
                },
                new InventoryItemDto
                {
                    Nama = "Rompi Proyek",
                    NoRak = "B-12",
                    Stok = 50,
                    ItemCategory = ItemCategory.Medium, // [BARU]
                    ImageUrl = "https://images.unsplash.com/photo-1581094288338-2314dddb79a7?q=80&w=400",
                    ExpDate = DateTime.Now.AddYears(1)
                }
            };
            NotifyStateChanged();
        }

        public void HandleSubmit()
        {
            if (!IsFormValid)
            {
                _snackbar.Add("Lengkapi data!", Severity.Warning);
                return;
            }

            var existing = Items.FirstOrDefault(x => x.Nama.Equals(NewItemName, StringComparison.OrdinalIgnoreCase));
            if (existing != null)
            {
                existing.Stok += NewItemQty;
                existing.ItemCategory = NewItemCategory;
                _snackbar.Add("Stok bertambah!", Severity.Success);
            }
            else
            {
                Items.Add(new InventoryItemDto
                {
                    Nama = NewItemName,
                    Stok = NewItemQty,
                    NoRak = NewItemRak,
                    ExpDate = NewItemExpDate,
                    ImageUrl = ImagePreview,
                    ItemCategory = NewItemCategory
                });
                _snackbar.Add("Barang baru!", Severity.Success);
            }

            IsAddDialogOpen = false;
            ResetForm();
            NotifyStateChanged(); // <--- Update UI
        }

        public void OnItemNameChanged(string value)
        {
            NewItemName = value;

            var existingItem = Items.FirstOrDefault(x =>
                x.Nama.Equals(value, StringComparison.OrdinalIgnoreCase));

            if (existingItem != null)
            {
                // Auto-fill nomor rak dari data existing
                NewItemRak = existingItem.NoRak;
                IsRakAutoFilled = true;
                NewItemCategory = existingItem.ItemCategory;

                // Auto-fill image preview jika ada
                if (!string.IsNullOrEmpty(existingItem.ImageUrl))
                {
                    ImagePreview = existingItem.ImageUrl;
                }
            }
            else
            {
                // Barang baru - reset rak & enable input
                NewItemRak = "";
                IsRakAutoFilled = false;
                NewItemCategory = ItemCategory.Light;
            }

            NotifyStateChanged();
        }
        public async Task TriggerFileInput()
        {
            await _jsRuntime.InvokeVoidAsync("eval", "document.getElementById('fileInput').click()");
        }

        // Handle file upload
        public async Task HandleFileSelected(InputFileChangeEventArgs e)
        {
            var file = e.File;

            if (file != null)
            {
                // Validasi ukuran file (max 5MB)
                if (file.Size > 5 * 1024 * 1024)
                {
                    _snackbar.Add("Ukuran file maksimal 5MB", Severity.Warning);
                    return;
                }

                // Validasi tipe file
                if (!file.ContentType.StartsWith("image/"))
                {
                    _snackbar.Add("File harus berupa gambar (JPG, PNG)", Severity.Warning);
                    return;
                }

                try
                {
                    // Convert to base64 for preview
                    var buffer = new byte[file.Size];
                    await file.OpenReadStream(maxAllowedSize: 5 * 1024 * 1024).ReadAsync(buffer);
                    UploadedImageBase64 = Convert.ToBase64String(buffer);
                    ImagePreview = $"data:{file.ContentType};base64,{UploadedImageBase64}";

                    NotifyStateChanged();
                    _snackbar.Add("Gambar berhasil diunggah", Severity.Success);
                }
                catch (Exception ex)
                {
                    _snackbar.Add($"Error upload gambar: {ex.Message}", Severity.Error);
                }
            }
        }

        // Clear uploaded image
        public void ClearImage()
        {
            ImagePreview = "";
            UploadedImageBase64 = "";
            NotifyStateChanged();
        }
        
        public void OpenAddDialog()
        {
            ResetForm();
            IsAddDialogOpen = true;
            NotifyStateChanged();
        }

        public void CancelDialog()
        {
            IsAddDialogOpen = false;
            NotifyStateChanged();
        }

        // Helper private untuk memicu update UI
        private void NotifyStateChanged() => OnStateChange?.Invoke();

        private void ResetForm()
        {
            NewItemName = "";
            NewItemQty = 1;
            NewItemRak = "";
            NewItemExpDate = null;
            ImagePreview = "";
            UploadedImageBase64 = "";
            IsRakAutoFilled = false;
            NewItemCategory = ItemCategory.Light;
        }
    }
}