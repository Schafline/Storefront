using System.Globalization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Storefront.Data;
using Storefront.Services;
using Storefront.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
.AddUserSecrets<Program>();

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
  options.Conventions
    .AuthorizeFolder("/Admin");
  options.Conventions
    .AllowAnonymousToPage(
      "/Admin/Login");
});

builder.Services.AddHttpClient();

var culture = new CultureInfo("en-US");
CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

var environment = builder.Environment;
if (environment.IsDevelopment())
{
  builder.Services.AddDbContext<ShopContext>(
    options =>
    {
      options.UseSqlite(
        builder.Configuration
          .GetConnectionString(
            "ShopDbConnection"));
    });
}
else
{
  var rawUrl =
    Environment.GetEnvironmentVariable(
      "DATABASE_URL");
  var uri = new Uri(rawUrl);
  var userInfo = uri.UserInfo.Split(':');
  var connectionString =
    $@"Host={uri.Host};Port={uri.Port};
    Username={userInfo[0]};Password={userInfo[1]};
    Database={uri.AbsolutePath.TrimStart('/')};
    SSL Mode=Require;
    Trust Server Certificate=true";

  builder.Services.AddDbContext<ShopContext>(
    options =>
      options.UseNpgsql(
        connectionString,
        npgsqlOptions =>
        {
          npgsqlOptions
            .EnableRetryOnFailure();
        }));
}

builder.Services
  .AddAuthentication(
    CookieAuthenticationDefaults
      .AuthenticationScheme)
  .AddCookie(options =>
  {
    options.LoginPath =
      "/Admin/Login";
  });

builder.Services.AddScoped<BasketService>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddSession();

builder.Services.Configure<
  EmailSettings>(
    builder.Configuration
      .GetSection("Email"));

builder.Services.AddScoped<
  EmailService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Error");
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

using (var scope = app.Services.CreateScope())
{
  var context = scope.ServiceProvider
    .GetRequiredService<ShopContext>();
  context.Database.Migrate();
  SeedData.Initialize(context);
}

app.Run();
