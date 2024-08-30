﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _2024AMS.Pages.OperatingSystems;

public class CancelOperatingSystemModel : PageModel
{

    public RedirectResult OnGet()
    {

        // Set the message.
        TempData["MessageColor"] = "Red";
        TempData["Message"] = "The operation was cancelled. No data was affected.";
        return Redirect("MaintainOperatingSystems");

    }

}
