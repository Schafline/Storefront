namespace Storefront.ViewModels;

public class ProductCardViewModel
{
    // Routing & Identity
    public int Id { get; set; }
    public string Slug { get; set; } = string.Empty;

    // Core Display
    public string Name { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public decimal Price { get; set; }

    // Badges
    public string AudienceLabel { get; set; } = string.Empty;      // e.g. "Mens"
    public string CategoryName { get; set; } = string.Empty;       // e.g. "Footwear"
    public string ConditionLabel { get; set; } = string.Empty;     // e.g. "Excellent"
    public string ConditionBadgeCss { get; set; } = string.Empty;  // e.g. "badge-condition-excellent"

    // Secondary Metadata Row
    public string ColourName { get; set; } = string.Empty;         // e.g. "Chestnut Brown"
    public string ColourHex { get; set; } = "#808080";             // Mapped from BaseColour for the swatch dot
    public string? Size { get; set; }                              // e.g. "UK 9 / US 10" (hidden when null)
    public int StockQuantity { get; set; }                         // Drives "1 available" vs "Sold Out"
}