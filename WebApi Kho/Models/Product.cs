using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi_Kho.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int Stock { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public string Description { get; set; } = "VIP";

        public string ImageUrl { get; set; } = string.Empty;

        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category? Categorys { get; set; }


    }
}
