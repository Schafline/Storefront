using Microsoft.EntityFrameworkCore;
using Storefront.Models;

namespace Storefront.Data
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