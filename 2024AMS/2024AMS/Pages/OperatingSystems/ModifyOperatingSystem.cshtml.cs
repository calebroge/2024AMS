using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _2024AMS.Models;

namespace _2024AMS.Pages.OperatingSystems;

[BindProperties]
public class ModifyOperatingSystemModel : PageModel
{
    private readonly _2024AMSContext _2024AMSContext;
    public ModifyOperatingSystemModel(_2024AMSContext AMS)
    {
        _2024AMSContext = AMS;
    }

    public Models.OperatingSystem OperatingSystem { get; set; }

    public async Task<IActionResult> OnGetAsync(int intOperatingSystemID)
    {
        // Set the message.
        ViewData["Title"] = "Modify Operating System";
        TempData["MessageColor"] = "Green";
        TempData["Message"] = "Please modify the information below and click modify.";

        // Attempt to retrieve the row from the table.
        OperatingSystem = await _2024AMSContext.OperatingSystem.FindAsync(intOperatingSystemID);
        if (OperatingSystem != null)
        {
            return Page();
        }
        else
        {
            // Set the message.
            TempData["MessageColor"] = "Red";
            TempData["Message"] = "The selected asset category was deleted by someone else.";
            return Redirect("MaintainOperatingSystems");
        }

    }

    public async Task<IActionResult> OnPostModifyAsync()
    {

        try
        {
            // Modify the row in the table.
            _2024AMSContext.OperatingSystem.Update(OperatingSystem);
            await _2024AMSContext.SaveChangesAsync();
            // Set the message.
            TempData["MessageColor"] = "Green";
            TempData["Message"] = OperatingSystem.OperatingSystem1 + " was successfully modified.";
        }
        catch (DbUpdateException objDbUpdateException)
        {
            // A database exception occurred while saving to the
            // database.
            // Set the message.
            TempData["MessageColor"] = "Red";
            TempData["Message"] = OperatingSystem.OperatingSystem1 + " was NOT modified. Please report this message to...: " + objDbUpdateException.InnerException.Message;
        }
        return Redirect("MaintainOperatingSystems");

    }

}
