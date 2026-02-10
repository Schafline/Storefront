using System.ComponentModel.DataAnnotations;

namespace Storefront.Models
{
    public class ShippingInfo
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string AddressLine1 { get; set; }

        public string? AddressLine2 { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Postcode { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}