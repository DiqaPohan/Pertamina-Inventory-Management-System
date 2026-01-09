using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using MudBlazor;
using Pertamina.SolutionTemplate.Bsui.Models;
using Pertamina.SolutionTemplate.Shared.Common.Enums;
using Pertamina.SolutionTemplate.Shared.Common.Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization; // PENTING: Untuk ReferenceHandler
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

        public string SearchString { get; set; } = "";
        public bool IsAddDialogOpen { get; set; }
        public DialogOptions DialogOptions { get; } = new() { MaxWidth = MaxWidth.Medium, FullWidth = true };
        public bool IsLoading { get; private set; } = false;

        public Guid? EditingItemId { get; set; }
        public string NewItemName { get; set; } = "";
        public int NewItemQty { get; set; } = 1;
        public string NewItemRak { get; set; } = "";
        public string NewItemUnit { get; set; } = "pcs";
        public DateTime? NewItemExpDate { get; set; }
        public string ImagePreview { get; set; } = "";
        public string UploadedImageBase64 { get; set; } = "";
        public bool IsRakAutoFilled { get; set; } = false;
        public ItemCategory NewItemCategory { get; set; } = ItemCategory.Light;
        // add near other properties
        public bool IsImageLoaded { get; private set; } = false;

        public List<InventoryItemDto> Items { get; private set; } = new();
        public bool IsFormValid => !string.IsNullOrWhiteSpace(NewItemName) && NewItemQty > 0;

        public IEnumerable<InventoryItemDto> FilteredItems
        {
            get
            {
                if (string.IsNullOrWhiteSpace(SearchString)) return Items;
                return Items.Where(x => x.Nama.Contains(SearchString, StringComparison.OrdinalIgnoreCase) ||
                                        x.NoRak.Contains(SearchString, StringComparison.OrdinalIgnoreCase));
            }
        }

        public async Task LoadDataAsync()
        {
            try
            {
                IsLoading = true;
                NotifyStateChanged();

                var client = _httpClientFactory.CreateClient(ApiClientName);
                client.Timeout = TimeSpan.FromSeconds(30);

                var response = await client.GetAsync("api/v1/items?PageNumber=1&PageSize=100");
                if (!response.IsSuccessStatusCode)
                {
                    _snackbar.Add($"Gagal memuat data. Status: {response.StatusCode}", Severity.Error);
                    return;
                }

                var jsonString = await response.Content.ReadAsStringAsync();

                // Diagnostic logging - always print so we can inspect payload
                Console.WriteLine($"DEBUG: /api/v1/items response length: {(jsonString?.Length ?? 0)}");
                if (!string.IsNullOrEmpty(jsonString))
                    Console.WriteLine("DEBUG: /api/v1/items response snippet: " +
                                      (jsonString.Length > 2000 ? jsonString.Substring(0, 2000) + " ...[truncated]" : jsonString));

                // Json options: allow string enums and case-insensitive property matching
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    PropertyNamingPolicy = null,
                    NumberHandling = JsonNumberHandling.AllowReadingFromString,
                    ReferenceHandler = ReferenceHandler.IgnoreCycles
                };
                options.Converters.Add(new JsonStringEnumConverter());

                PaginatedListResponse<ItemApiDto>? result = null;

                try
                {
                    result = JsonSerializer.Deserialize<PaginatedListResponse<ItemApiDto>>(jsonString, options);

                    if (result == null)
                    {
                        // Try root-array fallback
                        var listResult = JsonSerializer.Deserialize<List<ItemApiDto>>(jsonString, options);
                        if (listResult != null)
                        {
                            result = new PaginatedListResponse<ItemApiDto>
                            {
                                Items = listResult,
                                TotalCount = listResult.Count
                            };
                        }
                    }
                }
                catch (JsonException jex)
                {
                    // Detailed diagnostics to inspect JSON shape
                    Console.WriteLine("JsonException while parsing /api/v1/items: " + jex.Message);
                    Console.WriteLine("JsonException StackTrace: " + jex.StackTrace);

                    try
                    {
                        using var doc = JsonDocument.Parse(jsonString);
                        var root = doc.RootElement;
                        Console.WriteLine($"DEBUG: JSON root kind: {root.ValueKind}");

                        if (root.ValueKind == JsonValueKind.Object)
                        {
                            Console.WriteLine("DEBUG: Root object properties:");
                            foreach (var prop in root.EnumerateObject().Take(20))
                                Console.WriteLine($" - {prop.Name} (kind={prop.Value.ValueKind})");
                        }
                        else if (root.ValueKind == JsonValueKind.Array)
                        {
                            Console.WriteLine("DEBUG: Root is array; first element:");
                            if (root.GetArrayLength() > 0)
                            {
                                var first = root[0];
                                if (first.ValueKind == JsonValueKind.Object)
                                {
                                    foreach (var prop in first.EnumerateObject().Take(20))
                                        Console.WriteLine($" - {prop.Name} (kind={prop.Value.ValueKind})");
                                }
                                else
                                {
                                    Console.WriteLine($" - first element kind: {first.ValueKind}");
                                }
                            }
                        }
                    }
                    catch (Exception exDoc)
                    {
                        Console.WriteLine("Failed to parse JSON with JsonDocument: " + exDoc.Message);
                    }
                }
                catch (Exception exInner)
                {
                    Console.WriteLine("Unexpected parse error: " + exInner.Message);
                }

                if (result?.Items != null)
                {
                    Items = result.Items.Select(x => new InventoryItemDto
                    {
                        Id = x.Id,
                        Nama = x.Name ?? "Tanpa Nama",
                        Stok = x.TotalStock,
                        ItemCategory = x.Category,
                        ImageUrl = !string.IsNullOrEmpty(x.ImageUrl) ? x.ImageUrl : "https://via.placeholder.com/400?text=No+Image",
                        ExpDate = x.ExpiryDate,
                        NoRak = !string.IsNullOrEmpty(x.Description) ? x.Description : "N/A",
                        Satuan = x.Unit ?? "pcs"
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"FATAL ERROR LoadData: {ex}");
                _snackbar.Add($"Error sistem: {ex.Message}", Severity.Error);
            }
            finally
            {
                IsLoading = false;
                NotifyStateChanged();
            }
        }

        public async Task HandleSubmit()
        {
            if (!IsFormValid)
            {
                _snackbar.Add("Lengkapi data!", Severity.Warning);
                return;
            }

            try
            {
                IsLoading = true;
                NotifyStateChanged();

                var client = _httpClientFactory.CreateClient(ApiClientName);

                if (UploadedImageBytes != null && UploadedImageBytes.Length > 0)
                {
                    // Upload multipart: form fields + image file
                    using var content = new MultipartFormDataContent();

                    // Add JSON fields as string content (or add a JSON part depending on backend)
                    content.Add(new StringContent(NewItemName ?? ""), "Name");
                    content.Add(new StringContent(NewItemQty.ToString()), "TotalStock");
                    content.Add(new StringContent(NewItemQty.ToString()), "AvailableStock");
                    content.Add(new StringContent(NewItemUnit ?? "pcs"), "Unit");
                    content.Add(new StringContent(((int)NewItemCategory).ToString()), "Category");
                    if (NewItemExpDate.HasValue)
                        content.Add(new StringContent(NewItemExpDate.Value.ToString("o")), "ExpiryDate");
                    content.Add(new StringContent(NewItemRak ?? ""), "Description");

                    var streamContent = new ByteArrayContent(UploadedImageBytes);
                    streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                    // "ImageFile" should match the IFormFile parameter name expected by the API
                    content.Add(streamContent, "ImageFile", "upload.jpg");

                    HttpResponseMessage response;
                    if (EditingItemId.HasValue)
                    {
                        response = await client.PutAsync($"api/v1/items/{EditingItemId.Value}/multipart", content);
                    }
                    else
                    {
                        response = await client.PostAsync("api/v1/items/multipart", content);
                    }

                    if (response.IsSuccessStatusCode)
                    {
                        _snackbar.Add(EditingItemId.HasValue ? "Barang diperbarui!" : "Barang baru disimpan!", Severity.Success);
                        IsAddDialogOpen = false;
                        ResetForm();
                        await LoadDataAsync();
                    }
                    else
                    {
                        var errorBody = await response.Content.ReadAsStringAsync();
                        _snackbar.Add($"Gagal menyimpan: {response.StatusCode}", Severity.Error);
                        Console.WriteLine($"Multipart save failed: {response.StatusCode} - {errorBody}");
                    }
                }
                else
                {
                    // No image bytes -> fallback to your previous JSON-based POST (ImageUrl can be empty)
                    var payload = new CreateItemRequest
                    {
                        Name = NewItemName,
                        TotalStock = NewItemQty,
                        AvailableStock = NewItemQty,
                        Category = NewItemCategory,
                        ImageUrl = ImagePreview,
                        ExpiryDate = NewItemExpDate,
                        Description = NewItemRak,
                        Unit = NewItemUnit,
                        RackId = null
                    };

                    HttpResponseMessage response;
                    if (EditingItemId.HasValue)
                    {
                        var updatePayload = new UpdateItemRequest
                        {
                            Id = EditingItemId.Value,
                            Name = payload.Name,
                            TotalStock = payload.TotalStock,
                            AvailableStock = payload.AvailableStock,
                            Category = payload.Category,
                            ImageUrl = payload.ImageUrl,
                            ExpiryDate = payload.ExpiryDate,
                            Description = payload.Description,
                            Unit = payload.Unit,
                            RackId = payload.RackId
                        };
                        response = await client.PutAsJsonAsync($"api/v1/items/{EditingItemId.Value}", updatePayload);
                    }
                    else
                    {
                        response = await client.PostAsJsonAsync("api/v1/items", payload);
                    }

                    if (response.IsSuccessStatusCode)
                    {
                        _snackbar.Add(EditingItemId.HasValue ? "Barang diperbarui!" : "Barang baru disimpan!", Severity.Success);
                        IsAddDialogOpen = false;
                        ResetForm();
                        await LoadDataAsync();
                    }
                    else
                    {
                        var errorMsg = await response.Content.ReadAsStringAsync();
                        _snackbar.Add($"Gagal menyimpan: {response.StatusCode}", Severity.Error);
                        Console.WriteLine($"JSON save failed: {response.StatusCode} - {errorMsg}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unhandled error in HandleSubmit: {ex}");
                _snackbar.Add($"Error: {ex.Message}", Severity.Error);
            }
            finally
            {
                IsLoading = false;
                NotifyStateChanged();
            }
        }

        public async Task<IEnumerable<string>> SearchExistingItems(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Items.Select(x => x.Nama).Distinct().OrderBy(x => x);

            await Task.Delay(50);
            var valueLower = value.ToLower();
            return Items
                .Where(x => x.Nama.ToLower().Contains(valueLower))
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
                NewItemRak = existingItem.NoRak;
                IsRakAutoFilled = true;
                NewItemCategory = existingItem.ItemCategory;
                if (!string.IsNullOrEmpty(existingItem.ImageUrl)) ImagePreview = existingItem.ImageUrl;
            }
            else
            {
                if (!IsRakAutoFilled) { /* Do nothing */ }
            }
            NotifyStateChanged();
        }

        public void OpenAddDialog()
        {
            ResetForm();
            IsAddDialogOpen = true;
            NotifyStateChanged();
        }

        public void OpenEditDialog(InventoryItemDto item)
        {
            EditingItemId = item.Id;
            NewItemName = item.Nama;
            NewItemQty = item.Stok;
            NewItemRak = item.NoRak;
            NewItemExpDate = item.ExpDate;
            NewItemCategory = item.ItemCategory;
            ImagePreview = item.ImageUrl;
            NewItemUnit = item.Satuan;

            IsAddDialogOpen = true;
            NotifyStateChanged();
        }

        public void CancelDialog()
        {
            IsAddDialogOpen = false;
            NotifyStateChanged();
        }

        public async Task TriggerFileInput()
        {
            // Use a named JS function rather than eval to avoid crashes/JS errors
            await _jsRuntime.InvokeVoidAsync("triggerFileInput");
        }

        // Replace the existing HandleFileSelected method with this safer, more-logged implementation.
        public byte[]? UploadedImageBytes { get; private set; } = null;
        // replace HandleFileSelected with this minimal safe reader for debugging
        // Replace the existing HandleFileSelected method in this file with the code below.
        public async Task HandleFileSelected(InputFileChangeEventArgs e)
        {
            var file = e.File;
            Console.WriteLine($"HandleFileSelected start. file={(file != null ? $"{file.Name}, size={file.Size}" : "null")}");
            if (file == null) return;

            if (!file.ContentType.StartsWith("image/"))
            {
                _snackbar.Add("File harus berupa gambar", Severity.Warning);
                return;
            }

            try
            {
                const long maxReadBytes = 4 * 1024 * 1024; // 4 MB limit for safety
                if (file.Size > maxReadBytes)
                {
                    _snackbar.Add("File terlalu besar (>4MB). Gunakan file lebih kecil.", Severity.Warning);
                    UploadedImageBytes = null;
                    ImagePreview = "";
                    NotifyStateChanged();
                    return;
                }

                // Simple buffered read (NO resizing, NO decoding)
                using var stream = file.OpenReadStream(maxAllowedSize: maxReadBytes);
                using var ms = new MemoryStream();
                var buffer = new byte[81920];
                int read;
                Console.WriteLine("Start copying stream (buffered)...");
                while ((read = await stream.ReadAsync(buffer.AsMemory(0, buffer.Length))) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                var bytes = ms.ToArray();
                Console.WriteLine($"Buffered copy done. bytes={bytes.Length}");

                UploadedImageBytes = bytes;

                // Create small preview only when safe
                try
                {
                    if (bytes.Length <= 200 * 1024) // 200KB preview threshold
                    {
                        UploadedImageBase64 = Convert.ToBase64String(bytes);
                        ImagePreview = $"data:{file.ContentType};base64,{UploadedImageBase64}";
                        Console.WriteLine("Preview created (base64)");
                    }
                    else
                    {
                        UploadedImageBase64 = "";
                        ImagePreview = "";
                        _snackbar.Add("Preview disabled for large images. Image will be uploaded on save.", Severity.Info);
                        Console.WriteLine("Preview disabled due to size");
                    }
                }
                catch (Exception exPreview)
                {
                    Console.WriteLine($"Preview creation failed: {exPreview}");
                    UploadedImageBase64 = "";
                    ImagePreview = "";
                }
                IsImageLoaded = UploadedImageBytes != null && UploadedImageBytes.Length > 0;
                Console.WriteLine($"IsImageLoaded={IsImageLoaded}, UploadedImageBytesLength={(UploadedImageBytes?.Length ?? 0)}");

                NotifyStateChanged();
            }

            catch (Exception ex)
            {
                Console.WriteLine($"HandleFileSelected exception: {ex}");
                _snackbar.Add("Gagal memproses gambar", Severity.Error);
            }
        }


        public void ClearImage()
        {
            ImagePreview = "";
            UploadedImageBase64 = "";
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnStateChange?.Invoke();

        private void ResetForm()
        {
            EditingItemId = null;
            NewItemName = "";
            NewItemQty = 1;
            NewItemRak = "";
            NewItemUnit = "pcs";
            NewItemExpDate = null;
            ImagePreview = "";
            UploadedImageBase64 = "";
            IsRakAutoFilled = false;
            NewItemCategory = ItemCategory.Light;
        }

        private class ItemApiDto
        {
            public Guid Id { get; set; }
            public string? Name { get; set; }
            public string? Description { get; set; }
            public int TotalStock { get; set; }
            public int AvailableStock { get; set; }
            public string? Unit { get; set; }
            public ItemCategory Category { get; set; }
            public string? ImageUrl { get; set; }
            public DateTime? ExpiryDate { get; set; }
            public Guid? RackId { get; set; }
        }

        private class CreateItemRequest
        {
            public string? Name { get; set; }
            public int TotalStock { get; set; }
            public int AvailableStock { get; set; }
            public string? Unit { get; set; } = "pcs";
            public ItemCategory Category { get; set; }
            public string? ImageUrl { get; set; }
            public DateTime? ExpiryDate { get; set; }
            public string? Description { get; set; }
            public Guid? RackId { get; set; }
        }

        private class UpdateItemRequest : CreateItemRequest
        {
            public Guid Id { get; set; }
        }
    }
}