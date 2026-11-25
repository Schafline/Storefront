using System.ComponentModel.DataAnnotations;

namespace Storefront.Models;

public class Product : IAuditable
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [Required]
    [StringLength(150)]
    public string Slug { get; set; } = string.Empty;

    [Required]
    [StringLength(500)]
    public string Description { get; set; }

    [Required]
    [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
    public decimal Price { get; set; }

    [Required]
    [StringLength(255)] // Prevents unbounded string allocation in the DB
    public string ImageUrl { get; set; }

    public int CategoryId
    {
        get;
        set;
    }

    public Category Category
    {
        get;
        set;
    } = null!;

    [Required]
    public Audience Audience
    {
        get;
        set;
    }

    // Color duality: Rich descriptive string for display + standardized base color for UI filtering
    [Required]
    public string Colour { get; set; } = string.Empty; // e.g., "Burgundy and Mustard Houndstooth"

    [Required]
    public BaseColour BaseColour { get; set; }

    // One-of-a-kind inventory model (1 = Available, 0 = Sold)
    [Required]
    [Range(0, int.MaxValue)]
    public int StockQuantity
    {
        get;
        set;
    } = 1;

    [StringLength(50)]
    public string? Size { get; set; } = null;

    [Required]
    public ItemCondition Condition { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? UpdatedAt { get; set; }
}