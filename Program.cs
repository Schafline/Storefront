using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Storefront.Data;
using Storefront.Services;
using Storefront.Settings;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddUserSecrets<Program>()  // This line loads user secrets
    .AddEnvironmentVariables();
// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Admin");
    options.Conventions.AllowAnonymousToPage("/Admin/Login");
});

builder.Services.AddDbContext<ShopContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("ShopDbConnection"));
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Admin/Login";
    });

builder.Services.AddScoped<BasketService>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddSession();

builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("Email")
);

builder.Services.AddScoped<EmailService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see 
    // https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseSession();

app.MapStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
