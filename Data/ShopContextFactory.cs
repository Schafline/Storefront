/*

A factory is a design pattern used to create instances of a class. In this case, the `ShopContextFactory` is used to create instances of the `ShopContext` DbContext at design time, which is especially important for tools like Entity Framework Core migrations.

Why is this needed?
- EF Core design-time tools (such as migrations) need a way to create a `DbContext` instance without running the full application.
- The factory provides a way to configure and instantiate the `DbContext` with the correct options (like the connection string) during design time.
- This helps avoid issues where the tools cannot find or create the `DbContext` because the normal runtime configuration is not available.

Here is the factory implementation:

*/
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Storefront.Data
{
    public class ShopContextFactory : IDesignTimeDbContextFactory<ShopContext>
    {
        public ShopContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ShopContext>();
            optionsBuilder.UseSqlite("Data Source=shop.db");

            return new ShopContext(optionsBuilder.Options);
        }
    }
}