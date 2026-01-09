using System.ComponentModel.DataAnnotations;

namespace Storefront.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Range(0.01, 10000)]
        public decimal Price { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}

