using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

[AllowAnonymous]
public class GenerateHashModel : PageModel
{
    [BindProperty]
    public string Password { get; set; } = string.Empty;

    public string? Hash { get; set; }

    public void OnPost()
    {
        if (!string.IsNullOrWhiteSpace(Password))
        {
            var hasher = new PasswordHasher<object>();
            Hash = hasher.HashPassword(null, Password);
        }
    }
}