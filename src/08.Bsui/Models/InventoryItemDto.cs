using System;
using Pertamina.SolutionTemplate.Shared.Common.Enums;

namespace Pertamina.SolutionTemplate.Bsui.Models
{
    public class InventoryItemDto
    {
        // [BARU] Tambahkan ID untuk keperluan update data ke API
        public Guid Id { get; set; }

        public string Nama { get; set; } = string.Empty;
        public string NoRak { get; set; } = string.Empty;
        public int Stok { get; set; }
        public string Satuan { get; set; } = "Pcs"; // Default
        public ItemCategory ItemCategory { get; set; } = ItemCategory.Light;
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime? ExpiredDate { get; set; } // Sesuaikan nama dengan ExpDate di razor jika perlu, tapi razor pakai ExpDate? cek razor.
        // Di razor: @item.ExpDate. Jadi property ini harus ExpDate
        public DateTime? ExpDate { get; set; }

        public ItemStatus Status { get; set; }

        public bool IsExpiredSoon
        {
            get
            {
                if (!ExpDate.HasValue) return false;
                return (ExpDate.Value - DateTime.Now).TotalDays <= 30;
            }
        }
    }
}