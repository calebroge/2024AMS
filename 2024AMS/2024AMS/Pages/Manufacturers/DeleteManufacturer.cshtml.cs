using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using _2024AMS.Models;

namespace _2024AMS.Pages.Manufacturers;

[BindProperties]

public class DeleteManufacturerModel : PageModel
{

    private readonly _2024AMSContext _2024AMSContext;
    public DeleteManufacturerModel(_2024AMSContext AMS)
    {
        _2024AMSContext = AMS;
    }

    private Manufacturer Manufacturer { get; set; }

    public async Task<IActionResult> OnGetAsync(int intManufacturerID)
    {

        // Look up the row in the table to see if it still exists.
        Manufacturer = await _2024AMSContext.Manufacturer.FindAsync(intManufacturerID);
        if (Manufacturer != null)
        {
            try
            {
                // Delete the row from the table.
                _2024AMSContext.Manufacturer.Remove(Manufacturer);
                await _2024AMSContext.SaveChangesAsync();
                // Set the message.
                TempData["MessageColor"] = "Green";
                TempData["Message"] = Manufacturer.Manufacturer1 + " was successfully deleted.";
            }
            catch (DbUpdateException objDbUpdateException)
            {
                // A database exception occurred.
                SqlException objSqlException = objDbUpdateException.InnerException as SqlException;
                if (objSqlException.Number == 547)
                {
                    // A foreign key constraint database exception
                    // occurred.
                    // Set the message.
                    TempData["MessageColor"] = "Red";
                    TempData["Message"] = Manufacturer.Manufacturer1 + " was NOT deleted because it is associated with one or more order lines. To delete this asset category, you must first delete the associated order lines.";
                }
                else
                {
                    // A database exception occurred while saving to
                    // the database.
                    // Set the message.
                    TempData["MessageColor"] = "Red";
                    TempData["Message"] = Manufacturer.Manufacturer1 + " was NOT deleted. Please report this message to...: " + objDbUpdateException.InnerException.Message;
                }
            }
        }
        else
        {
            // Even though someone else deleted the item first, still
            // inform the user that the item was deleted successfully.
            // Set the message.
            TempData["MessageColor"] = "Green";
            TempData["Message"] = "The asset category was successfully deleted.";
        }
        return Redirect("MaintainManufacturers");

    }

}
