using System;

namespace Pertamina.SolutionTemplate.Bsui.Models
{
    // Class ini berfungsi sebagai kontrak data (Payload) untuk dikirim ke API.
    // Propertinya harus match dengan apa yang diharapkan oleh ConfirmPlacementCommand di API.
    public class ConfirmPlacementRequest
    {
        public Guid ItemId { get; set; }
        public bool IsRackFull { get; set; }
    }
}