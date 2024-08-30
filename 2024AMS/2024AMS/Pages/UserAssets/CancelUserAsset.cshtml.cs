using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _2024AMS.Pages.UserAssets;

public class CancelUserAssetModel : PageModel
{
    public RedirectResult OnGet()
    {
        // Initialize the message.
        TempData["MessageColor"] = "Red";
        TempData["Message"] = "The operation was cancelled. No data was affected.";
        return Redirect("MaintainUserAssets");
    }
}