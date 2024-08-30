using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _2024AMS.Pages.Manufacturers;

public class CancelManufacturerModel : PageModel
{

    public RedirectResult OnGet()
    {

        // Set the message.
        TempData["MessageColor"] = "Red";
        TempData["Message"] = "The operation was cancelled. No data was affected.";
        return Redirect("MaintainManufacturers");

    }

}
