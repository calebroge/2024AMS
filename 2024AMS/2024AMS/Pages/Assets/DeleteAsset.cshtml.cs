using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using _2024AMS.Models;

namespace _2024AMS.Pages.Assets;

[BindProperties]

public class DeleteAssetModel : PageModel
{

    private readonly _2024AMSContext _2024AMSContext;
    public DeleteAssetModel(_2024AMSContext AMS)
    {
        _2024AMSContext = AMS;
    }

    private Asset Asset { get; set; }

    public async Task<IActionResult> OnGetAsync(int intAssetID)
    {

        // Look up the row in the table to see if it still exists.
        Asset = await _2024AMSContext.Asset.FindAsync(intAssetID);
        if (Asset != null)
        {
            try
            {
                // Delete the row from the table.
                _2024AMSContext.Asset.Remove(Asset);
                await _2024AMSContext.SaveChangesAsync();
                // Set the message.
                TempData["MessageColor"] = "Green";
                TempData["Message"] = Asset.Asset1 + " was successfully deleted.";
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
                    TempData["Message"] = Asset.Asset1 + " was NOT deleted because it has been checked out to a user. To delete " + Asset.Asset1 + ", " +
                        "you must first delete the checked out record on the maintain user assets page.";
                }
                else
                {
                    // A database exception occurred while saving to
                    // the database.
                    // Set the message.
                    TempData["MessageColor"] = "Red";
                    TempData["Message"] = Asset.Asset1 + " was NOT deleted. Please report this message to...: " + objDbUpdateException.InnerException.Message;
                }
            }
        }
        else
        {
            // Even though someone else deleted the item first, still
            // inform the user that the item was deleted successfully.
            // Set the message.
            TempData["MessageColor"] = "Green";
            TempData["Message"] = "The Asset was successfully deleted.";
        }
        return Redirect("MaintainAssets");

    }

}
