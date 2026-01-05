using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using MudBlazor;

namespace Pertamina.SolutionTemplate.Bsui.Pages
{
    public partial class Inventory
    {
        [Inject] private ISnackbar Snackbar { get; set; } = default!;
        [Inject] private IJSRuntime JSRuntime { get; set; } = default!;

        private string _searchString = "";
        private bool _isAddDialogOpen;
        private DialogOptions _dialogOptions = new()
        {
            MaxWidth = MaxWidth.Medium,
            FullWidth = true,
            CloseButton = true,
            DisableBackdropClick = true,
            CloseOnEscapeKey = true
        };

        // Form Fields
        private string _newItemName = "";
        private int _newItemQty = 1;
        private string _newItemRak = "";
        private DateTime? _newItemExpDate;
        private string _imagePreview = "";
        private string _uploadedImageBase64 = "";
        private bool _isRakAutoFilled = false;

        // Validation
        private bool IsFormValid =>
            !string.IsNullOrWhiteSpace(_newItemName) &&
            _newItemQty > 0 &&
            !string.IsNullOrWhiteSpace(_newItemRak);

        // Data Structure
        public class InventoryItem
        {
            public string Nama { get; set; } = "";
            public string NoRak { get; set; } = "";
            public int Stok { get; set; }
            public string Satuan { get; set; } = "Pcs";
            public string ImageUrl { get; set; } = "";
            public DateTime? ExpDate { get; set; }
            public bool IsExpiredSoon => ExpDate.HasValue && (ExpDate.Value - DateTime.Now).TotalDays < 30;
        }

        private List<InventoryItem> _items = new()
        {
            new InventoryItem
            {
                Nama = "Helm Safety Red",
                NoRak = "A-01",
                Stok = 25,
                ImageUrl = "https://images.unsplash.com/photo-1590650153855-d9e808231d41?q=80&w=400",
                ExpDate = DateTime.Now.AddDays(15)
            },
            new InventoryItem
            {
                Nama = "Rompi Proyek",
                NoRak = "B-12",
                Stok = 50,
                ImageUrl = "https://images.unsplash.com/photo-1581094288338-2314dddb79a7?q=80&w=400",
                ExpDate = DateTime.Now.AddYears(1)
            },
            new InventoryItem
            {
                Nama = "Kabel Roll 50m",
                NoRak = "C-05",
                Stok = 10,
                ImageUrl = "https://images.unsplash.com/photo-1558522195-e1201b090344?q=80&w=400",
                ExpDate = null
            },
            new InventoryItem
            {
                Nama = "Sepatu Safety",
                NoRak = "A-05",
                Stok = 12,
                ImageUrl = "https://images.unsplash.com/photo-1542291026-7eec264c27ff?q=80&w=400",
                ExpDate = DateTime.Now.AddDays(10)
            },
            new InventoryItem
            {
                Nama = "Sarung Tangan Karet",
                NoRak = "B-03",
                Stok = 100,
                ImageUrl = "https://images.unsplash.com/photo-1585741142235-b5ccf6d0803e?q=80&w=400",
                ExpDate = DateTime.Now.AddMonths(6)
            }
        };

        // Real-time filtering
        private IEnumerable<InventoryItem> FilteredItems
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_searchString))
                    return _items.OrderBy(x => x.Nama);

                var searchLower = _searchString.ToLower();
                return _items
                    .Where(x => x.Nama.ToLower().Contains(searchLower) ||
                                x.NoRak.ToLower().Contains(searchLower))
                    .OrderBy(x => x.Nama);
            }
        }

        // Autocomplete search
        private async Task<IEnumerable<string>> SearchExistingItems(string value)
        {
            await Task.Delay(50);

            if (string.IsNullOrWhiteSpace(value))
                return _items.Select(x => x.Nama).Distinct().OrderBy(x => x);

            var valueLower = value.ToLower();
            return _items
                .Where(x => x.Nama.ToLower().Contains(valueLower))
                .Select(x => x.Nama)
                .Distinct()
                .OrderBy(x => x)
                .ToList();
        }

        // Handle item name selection - AUTO FILL RAK
        private void OnItemNameChanged(string value)
        {
            _newItemName = value;

            var existingItem = _items.FirstOrDefault(x =>
                x.Nama.Equals(value, StringComparison.OrdinalIgnoreCase));

            if (existingItem != null)
            {
                // Auto-fill nomor rak dari data existing
                _newItemRak = existingItem.NoRak;
                _isRakAutoFilled = true;

                // Auto-fill image preview jika ada
                if (!string.IsNullOrEmpty(existingItem.ImageUrl))
                {
                    _imagePreview = existingItem.ImageUrl;
                }
            }
            else
            {
                // Barang baru - reset rak & enable input
                _newItemRak = "";
                _isRakAutoFilled = false;
            }

            StateHasChanged();
        }

        // Trigger file input click
        private async Task TriggerFileInput()
        {
            await JSRuntime.InvokeVoidAsync("eval", "document.getElementById('fileInput').click()");
        }

        // Handle file upload
        private async Task HandleFileSelected(InputFileChangeEventArgs e)
        {
            var file = e.File;

            if (file != null)
            {
                // Validasi ukuran file (max 5MB)
                if (file.Size > 5 * 1024 * 1024)
                {
                    Snackbar.Add("Ukuran file maksimal 5MB", Severity.Warning);
                    return;
                }

                // Validasi tipe file
                if (!file.ContentType.StartsWith("image/"))
                {
                    Snackbar.Add("File harus berupa gambar (JPG, PNG)", Severity.Warning);
                    return;
                }

                try
                {
                    // Convert to base64 for preview
                    var buffer = new byte[file.Size];
                    await file.OpenReadStream(maxAllowedSize: 5 * 1024 * 1024).ReadAsync(buffer);
                    _uploadedImageBase64 = Convert.ToBase64String(buffer);
                    _imagePreview = $"data:{file.ContentType};base64,{_uploadedImageBase64}";

                    StateHasChanged();
                    Snackbar.Add("Gambar berhasil diunggah", Severity.Success);
                }
                catch (Exception ex)
                {
                    Snackbar.Add($"Error upload gambar: {ex.Message}", Severity.Error);
                }
            }
        }

        // Clear uploaded image
        private void ClearImage()
        {
            _imagePreview = "";
            _uploadedImageBase64 = "";
            StateHasChanged();
        }

        // Open add dialog
        private void OpenAddDialog()
        {
            ResetForm();
            _isAddDialogOpen = true;
        }

        // Cancel dialog
        private void CancelDialog()
        {
            _isAddDialogOpen = false;
            ResetForm();
        }

        // Reset form
        private void ResetForm()
        {
            _newItemName = "";
            _newItemQty = 1;
            _newItemRak = "";
            _newItemExpDate = null;
            _imagePreview = "";
            _uploadedImageBase64 = "";
            _isRakAutoFilled = false;
        }

        // Handle form submission
        private void HandleSubmit()
        {
            if (!IsFormValid)
            {
                Snackbar.Add("Mohon lengkapi semua field yang wajib diisi", Severity.Warning);
                return;
            }

            var existing = _items.FirstOrDefault(x =>
                x.Nama.Equals(_newItemName.Trim(), StringComparison.OrdinalIgnoreCase));

            if (existing != null)
            {
                // Update existing item
                existing.Stok += _newItemQty;

                // Update image jika ada upload baru
                if (!string.IsNullOrEmpty(_imagePreview))
                {
                    existing.ImageUrl = _imagePreview;
                }

                // Update exp date jika diisi
                if (_newItemExpDate.HasValue)
                {
                    existing.ExpDate = _newItemExpDate;
                }

                Snackbar.Add($"Stok {existing.Nama} berhasil ditambahkan (+{_newItemQty}). Total: {existing.Stok} {existing.Satuan}", Severity.Success);
            }
            else
            {
                // Add new item
                string finalImage = string.IsNullOrEmpty(_imagePreview)
                    ? "https://via.placeholder.com/400x300?text=No+Image"
                    : _imagePreview;

                var newItem = new InventoryItem
                {
                    Nama = _newItemName.Trim(),
                    Stok = _newItemQty,
                    NoRak = _newItemRak.Trim(),
                    ExpDate = _newItemExpDate,
                    ImageUrl = finalImage
                };

                _items.Add(newItem);
                Snackbar.Add($"Barang baru '{newItem.Nama}' berhasil ditambahkan", Severity.Success);
            }

            _isAddDialogOpen = false;
            ResetForm();
            StateHasChanged();
        }
    }
}