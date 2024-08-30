using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _2024AMS.Pages.Home;
public class PrivacyModel : PageModel
{

    public void OnGet()
    {
        ViewData["Title"] = "Privacy";
        if (TempData["MessageColor"] == null)
        {
            TempData["MessageColor"] = "Green";
            TempData["Message"] = "The data here is property of Franklin College.";
        }
        else
        {
            ViewData["MessageColor"] =
            HttpContext.Session.GetString("MessageColor");
            ViewData["Message"] =
                HttpContext.Session.GetString("Message");
        }
    }
}

