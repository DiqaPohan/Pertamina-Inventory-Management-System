using System;
using System.Text.Json.Serialization; // Wajib ada
using Pertamina.SolutionTemplate.Shared.Common.Enums;

namespace Pertamina.SolutionTemplate.Bsui.Models
{
    public class InventoryItemDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        // API kirim "name", kita paksa masuk ke "Nama"
        [JsonPropertyName("name")]
        public string Nama { get; set; } = string.Empty;

        // Coba mapping ke "rackId" atau "rackName". 
        // Berdasarkan controller temanmu yang pakai param 'rackId', kemungkinan di sini juga 'rackId'
        [JsonPropertyName("rackId")]
        public string NoRak { get; set; } = string.Empty;

        [JsonPropertyName("stock")]
        public int Stok { get; set; }

        [JsonPropertyName("uom")]
        public string Satuan { get; set; } = "Pcs";

        [JsonPropertyName("itemCategory")]
        public ItemCategory ItemCategory { get; set; } = ItemCategory.Light;

        [JsonPropertyName("imageUrl")]
        public string ImageUrl { get; set; } = string.Empty;

        [JsonPropertyName("expiredDate")]
        public DateTime? ExpiredDate { get; set; }

        [JsonPropertyName("expDate")]
        public DateTime? ExpDate { get; set; }

        [JsonPropertyName("status")]
        public ItemStatus Status { get; set; }

        public bool IsExpiredSoon
        {
            get
            {
                if (!ExpDate.HasValue) return false;
                return (ExpDate.Value - DateTime.Now).TotalDays <= 30;
            }
        }

        // Helper Property: Kalau Nama masih null (mapping gagal), tampilkan ID biar kita sadar
        public string DisplayText => string.IsNullOrEmpty(Nama) ? $"Mapping Error (ID: {Id})" : $"{Nama} (Rak: {NoRak})";
    }
}