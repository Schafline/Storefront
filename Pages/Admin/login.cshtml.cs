using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

public class LoginModel : PageModel
{
    private readonly IConfiguration _config;

    public LoginModel(IConfiguration config)
    {
        _config = config;
    }

    [BindProperty]
    public string Password { get; set; } = string.Empty;

    public string? ErrorMessage { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        // Read the stored hash from appsettings.json
        var storedHash = _config["Admin:PasswordHash"];

        if (string.IsNullOrWhiteSpace(storedHash))
        {
            ErrorMessage = "Admin password has not been configured.";
            return Page();
        }

        // Verify the supplied password against the stored hash
        var hasher = new PasswordHasher<object>();
        var result = hasher.VerifyHashedPassword(null, storedHash, Password);

        if (result == PasswordVerificationResult.Success)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "AdminUser")
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToPage("/Admin/Index");
        }

        ErrorMessage = "Incorrect password.";
        return Page();
    }
}