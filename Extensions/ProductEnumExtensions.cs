using Storefront.Models;

namespace Storefront.Extensions;

public static class ProductEnumExtensions
{
    public static string ToDisplayLabel(this ItemCondition condition) => condition switch
    {
        ItemCondition.Mint => "Mint",
        ItemCondition.Excellent => "Excellent",
        ItemCondition.VeryGood => "Very Good",
        ItemCondition.Good => "Good",
        _ => condition.ToString()
    };

    public static string ToBadgeCssClass(this ItemCondition condition) => condition switch
    {
        ItemCondition.Mint => "badge-condition-mint",
        ItemCondition.Excellent => "badge-condition-excellent",
        ItemCondition.VeryGood => "badge-condition-very-good",
        ItemCondition.Good => "badge-condition-good",
        _ => "badge-condition-good"
    };

    public static string ToHexCode(this BaseColour colour) => colour switch
    {
        BaseColour.Black => "#1f2937",
        BaseColour.White => "#f9fafb",
        BaseColour.Grey => "#6b7280",
        BaseColour.Blue => "#2563eb",
        BaseColour.Green => "#15803d",
        BaseColour.Brown => "#6b4423",
        BaseColour.Beige => "#e5dcc5",
        BaseColour.Red => "#b91c1c",
        BaseColour.Yellow => "#eab308",
        BaseColour.Orange => "#ea580c",
        BaseColour.Purple => "#7e22ce",
        BaseColour.Pink => "#db2777",
        _ => "#9ca3af"
    };
}