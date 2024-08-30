using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using _2024AMS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace _2024AMS.Pages.UserAssets;

[BindProperties]

public class ModifyUserAssetModel : PageModel
{
    private readonly _2024AMSContext _2024AMSContext;

    public ModifyUserAssetModel(_2024AMSContext AMS)
    {
        _2024AMSContext = AMS;
    }

    public SelectList AssetSelectList;
    public SelectList UserSelectList;

    public UserAsset UserAsset { get; set; }
    public User User { get; set; }
    public Asset Asset { get; set; }

    public async Task<IActionResult> OnGetAsync(int intUserAssetID)
    {
        // Set the message.
        ViewData["Title"] = "Modify User Asset";
        TempData["MessageColor"] = "Green";
        TempData["Message"] = "Please modify the information below and click modify.";

        // Populate the select list.
        AssetSelectList = new SelectList(_2024AMSContext.Asset
            .OrderBy(a => a.Asset1), "AssetID", "Asset1");

        // Populate the select list.
        UserSelectList = new SelectList(_2024AMSContext.User.Select(x => new
        {
            ID = x.UserID,
            UserName = x.LastName + ", " + x.FirstName
        }).OrderBy(u => u.UserName), "ID", "UserName");

        // Attempt to retrieve the row from the table.
        UserAsset = await _2024AMSContext.UserAsset.FindAsync(intUserAssetID);

        if (UserAsset != null)
        {
            return Page();
        }
        else
        {
            // Set the message.
            TempData["MessageColor"] = "Red";
            TempData["Message"] = "The selected user asset was deleted by someone else.";
            return Redirect("MaintainUserAssets");
        }
    }

    public async Task<IActionResult> OnPostModifyAsync()
    {
        try
        {
            // Add the row to the table.
            _2024AMSContext.UserAsset.Update(UserAsset);
            await _2024AMSContext.SaveChangesAsync();

            // Look up user in the database.
            Asset = await _2024AMSContext.Asset.FindAsync(UserAsset.AssetID);
            User = await _2024AMSContext.User.FindAsync(UserAsset.UserID);

            // Set the message.
            TempData["Message"] =  "The asset " + Asset.Asset1 + " checked out to " + User.FirstName + " " + User.LastName + "  was successfully modified.";
            TempData["MessageColor"] = "Green";
        }
        catch (DbUpdateException objDbUpdateException)
        {
            // A database update exception occurred while saving to the database.
            // Set the message.
            TempData["MessageColor"] = "Red";
            TempData["Message"] = User.FirstName + " " + User.LastName + "'s asset was NOT modified. " +
                "Please report this message to Robert E. Beasley: ...: " +
                objDbUpdateException.InnerException.Message;
        }
        return Redirect("MaintainUserAssets");
    }
}

