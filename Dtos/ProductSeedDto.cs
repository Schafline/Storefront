using Storefront.Models;

namespace StoreFront.Dtos;

public class ProductSeedDto
{
    public string Name
    {
        get;
        set;
    }

    public string Description
    {
        get;
        set;
    }

    public string Category
    {
        get;
        set;
    }

    public Audience Audience
    {
        get;
        set;
    }

    public BaseColour BaseColour { get; set; }

    public string Colour
    {
        get;
        set;
    }

    public decimal Price
    {
        get;
        set;
    }

    public string Slug 
    { 
        get;
        set; 
    }

    public string? Size 
    { 
        get; 
        set; 
    }

    public ItemCondition Condition 
    { 
        get; 
        set; 
    }

    public int StockQuantity
    {
        get;
        set;
    }

    public string ImageUrl
    {
        get;
        set;
    }
}