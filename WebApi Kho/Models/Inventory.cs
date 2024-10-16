using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApi_Kho.Models
{
    public class Inventory
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Quantity { get; set; }

        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product? Products { get; set; }
    }
}
