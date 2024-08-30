using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _2024AMS.Pages.Common;

public class ViewWelcomeModel : PageModel
{

    public void OnGet()
    {
        // Set the message.
        ViewData["Title"] = "Welcome";

        // Sets header data and checks to see if data is null.
        // If the data is null, it sets the message and if it is not, it gets the data from the previous page.
        if (ViewData["MessageColor"] == null)
        {
            ViewData["MessageColor"] = "Green";
            ViewData["Message"] = "Welcome to the Asset Management System.";
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
