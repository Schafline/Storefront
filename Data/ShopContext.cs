using Microsoft.EntityFrameworkCore;
using storefront.Models;

namespace storefront.Data
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options)
        : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}