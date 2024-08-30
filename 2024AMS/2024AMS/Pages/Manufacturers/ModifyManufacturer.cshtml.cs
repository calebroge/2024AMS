using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _2024AMS.Models;

namespace _2024AMS.Pages.Manufacturers;

[BindProperties]
public class ModifyManufacturerModel : PageModel
{
    private readonly _2024AMSContext _2024AMSContext;
    public ModifyManufacturerModel(_2024AMSContext AMS)
    {
        _2024AMSContext = AMS;
    }

    public Manufacturer Manufacturer { get; set; }

    public async Task<IActionResult> OnGetAsync(int intManufacturerID)
    {
        // Set the message.
        ViewData["Title"] = "Modify Manufacturer";
        TempData["MessageColor"] = "Green";
        TempData["Message"] = "Please modify the information below and click modify.";

        // Attempt to retrieve the row from the table.
        Manufacturer = await _2024AMSContext.Manufacturer.FindAsync(intManufacturerID);
        if (Manufacturer != null)
        {
            return Page();
        }
        else
        {
            // Set the message.
            TempData["MessageColor"] = "Red";
            TempData["Message"] = "The selected asset category was deleted by someone else.";
            return Redirect("MaintainManufacturers");
        }

    }

    public async Task<IActionResult> OnPostModifyAsync()
    {

        try
        {
            // Modify the row in the table.
            _2024AMSContext.Manufacturer.Update(Manufacturer);
            await _2024AMSContext.SaveChangesAsync();
            // Set the message.
            TempData["MessageColor"] = "Green";
            TempData["Message"] = Manufacturer.Manufacturer1 + " was successfully modified.";
        }
        catch (DbUpdateException objDbUpdateException)
        {
            // A database exception occurred while saving to the
            // database.
            // Set the message.
            TempData["MessageColor"] = "Red";
            TempData["Message"] = Manufacturer.Manufacturer1 + " was NOT modified. Please report this message to...: " + objDbUpdateException.InnerException.Message;
        }
        return Redirect("MaintainManufacturers");

    }

}
