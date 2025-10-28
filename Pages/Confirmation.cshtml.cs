using Microsoft.AspNetCore.Mvc.RazorPages;

public class ConfirmationModel : PageModel
{
    public void OnGet()
    {
        // Clear the basket when landing on this page
        TempData["Basket"] = "[]";
    }
}

