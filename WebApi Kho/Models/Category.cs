using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApi_Kho.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        [JsonIgnore]
        public ICollection<Product> Products { get; set; } = new List<Product>();

    }
}
