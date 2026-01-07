using System;

namespace Pertamina.SolutionTemplate.Bsui.Models
{
    public class InventoryItemDto
    {
        public string Nama { get; set; } = "";
        public string NoRak { get; set; } = "";
        public int Stok { get; set; }
        public string Satuan { get; set; } = "Pcs";
        public string ImageUrl { get; set; } = "";
        public DateTime? ExpDate { get; set; }
        public bool IsExpiredSoon => ExpDate.HasValue && (ExpDate.Value - DateTime.Now).TotalDays < 30;
    }
}