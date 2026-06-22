using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Storefront.Data;

public class ShopContextFactory
: IDesignTimeDbContextFactory<ShopContext>
{
  public ShopContext
  CreateDbContext(string[] args)
  {
    var config = new ConfigurationBuilder()
      .AddJsonFile("appsettings.json")
      .AddJsonFile("appsettings.Development.json",
       optional: true)
      .AddEnvironmentVariables()
      .Build();

    var env =
      Environment.GetEnvironmentVariable(
      "ASPNETCORE_ENVIRONMENT");

    var optionsBuilder =
      new DbContextOptionsBuilder<ShopContext>();

    if (env == "Production")
    {
      var connectionString =
        Environment.GetEnvironmentVariable(
        "DATABASE_URL");

      optionsBuilder.UseNpgsql(connectionString);
    }
    else
    {
      // Use SQLite in development
      var sqlite =
        config.GetConnectionString(
       "ShopDbConnection");

      optionsBuilder.UseSqlite(sqlite);
    }
    return new ShopContext
    (optionsBuilder.Options);
  }
}