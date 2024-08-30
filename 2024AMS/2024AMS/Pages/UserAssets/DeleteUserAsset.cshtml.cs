using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using _2024AMS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;

namespace _2024AMS.Pages.UserAssets;

[BindProperties]

public class DeleteUserAssetModel : PageModel
{
    private readonly _2024AMS.Models._2024AMSContext _2024AMSContext;

    public DeleteUserAssetModel(_2024AMS.Models._2024AMSContext SPC)
    {
        _2024AMSContext = SPC;
    }
    
    private UserAsset UserAsset { get; set; }
    private User User { get; set; }
    private Asset Asset { get; set; }

    public async Task<IActionResult> OnGetAsync(int intUserAssetID)
    {
        // Attempt to retrieve the row from the table.
        UserAsset = await _2024AMSContext.UserAsset
            .Where(ua => ua.UserAssetID == intUserAssetID)
            .FirstOrDefaultAsync();

        // Look up asset in the database.
        Asset = await _2024AMSContext.Asset.FindAsync(UserAsset.AssetID);
        User = await _2024AMSContext.User.FindAsync(UserAsset.UserID);

        if (UserAsset != null)
        {
            try
            {
                // Delete the row to the table.
                _2024AMSContext.UserAsset.Remove(UserAsset);
                await _2024AMSContext.SaveChangesAsync();

                // Look up asset in the database.
                Asset = await _2024AMSContext.Asset.FindAsync(UserAsset.AssetID);
                User = await _2024AMSContext.User.FindAsync(UserAsset.UserID);

                // Set the message.
                TempData["Message"] = Asset.Asset1 + " checked out to " + User.FirstName + " " + User.LastName + " was successfully deleted.";
                TempData["MessageColor"] = "Green";
            }
            catch (DbUpdateException objDbUpdateException)
            {
                // A database exception occurred.
                SqlException objSqlException = objDbUpdateException
                    .InnerException as SqlException;
                if (objSqlException.Number == 547)
                {
                    // A foreign key constraint database exception occurred.
                    // Set the message.
                    TempData["MessageColor"] = "Red";
                    TempData["Message"] = Asset.Asset1 + " checked out to " + User.FirstName + " " + User.LastName + " was NOT deleted because " +
                        "it is associated with one or more users. To delete this user, you must first delete the associated users.";
                }
                else
                {
                    // Set the message.
                    // A database exception occured while saving to the database.
                    TempData["MessageColor"] = "Red";
                    TempData["Message"] = Asset.Asset1 + " checked out to " + User.FirstName + " " + User.LastName + " was NOT deleted. " +
                        "Please report this message to Robert E. Beasley: ...: " +
                        objDbUpdateException.InnerException.Message;
                }
            }
        }
        else
        {
            // Set the message.
            // Even though someone else deleted the item first,
            // we will still inform the user that the user's asset was
            // deleted successsfully.
            TempData["MessageColor"] = "Green";
            TempData["Message"] = Asset.Asset1 + " checked out to " + User.FirstName + " " + User.LastName + "was successfully deleted.";
        }
        return Redirect("MaintainUserAssets");

    }
}

