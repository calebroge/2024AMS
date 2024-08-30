using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _2024AMS.Models;

namespace _2024AMS.Pages.Manufacturers;

[BindProperties]
public class AddManufacturerModel : PageModel
{
    private readonly _2024AMSContext _2024AMSContext;
    public AddManufacturerModel(_2024AMSContext AMS)
    {
        _2024AMSContext = AMS;
    }

    public Manufacturer Manufacturer { get; set; }

    public void OnGet()
    {
        // Set the message.
        ViewData["Title"] = "Add Manufacturer";
        TempData["MessageColor"] = "Green";
        TempData["Message"] = "Please add the information below and click add.";

    }

    public async Task<IActionResult> OnPostAddAsync()
    {

        try
        {
            // Add the row to the table.
            _2024AMSContext.Manufacturer.Add(Manufacturer);
            await _2024AMSContext.SaveChangesAsync();
            // Set the message.
            TempData["MessageColor"] = "Green";
            TempData["Message"] = Manufacturer.Manufacturer1 + " was successfully added.";
        }
        catch (DbUpdateException objDbUpdateException)
        {
            // A database exception occurred while saving to the
            // database.
            // Set the message.
            TempData["MessageColor"] = "Red";
            TempData["Message"] = Manufacturer.Manufacturer1 + " was NOT added. Please report this message to...: " + objDbUpdateException.InnerException.Message;
        }
        return Redirect("MaintainManufacturers");

    }

}
