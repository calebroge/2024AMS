using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using _2024AMS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace _2024AMS.Pages.UserAssets;

[BindProperties]

public class AddUserAssetModel : PageModel
{

    private readonly _2024AMSContext _2024AMSContext;

    public AddUserAssetModel(_2024AMSContext AMS)
    {
        _2024AMSContext = AMS;
    }

    public SelectList AssetSelectList;
    public SelectList UserSelectList;
    public UserAsset UserAsset { get; set; }

    public User User { get; set; }

    public Asset Asset { get; set; }

    public void OnGet()
    {
        // Set the message.
        ViewData["Title"] = "Add User Asset";
        TempData["MessageColor"] = "Green";
        TempData["Message"] = "Please add the information below and click add.";

        // Populate the select list.
        AssetSelectList = new SelectList(_2024AMSContext.Asset
            .OrderBy(a => a.Asset1), "AssetID", "Asset1");

        // Populate the select list.
        UserSelectList = new SelectList(_2024AMSContext.User.Select(x => new
        {
            ID = x.UserID,
            UserName = x.LastName + ", " + x.FirstName
        }).OrderBy(u => u.UserName), "ID", "UserName");

    }

    public async Task<IActionResult> OnPostAddAsync()
    {
        try
        {
            // Add the row to the table.
            _2024AMSContext.UserAsset.Add(UserAsset);
            await _2024AMSContext.SaveChangesAsync();

            // Look up asset in the database.
            Asset = await _2024AMSContext.Asset.FindAsync(UserAsset.AssetID);
            User = await _2024AMSContext.User.FindAsync(UserAsset.UserID);

            // Set the message.
            TempData["Message"] =  Asset.Asset1 + " checked out to " + User.FirstName + " " + User.LastName + " was successfully added.";
            TempData["MessageColor"] = "Green";
        }
        catch (DbUpdateException objDbUpdateException)
        {
            // A database update exception occurred while saving to the database.
            // Set the message.
            TempData["MessageColor"] = "Red";
            TempData["Message"] = Asset.Asset1 + "assigned to " + User.FirstName + User.LastName + " was NOT added. " +
                "Please report this message to Robert E. Beasley: ...: " +
                objDbUpdateException.InnerException.Message;
        }
        return Redirect("MaintainUserAssets");
    }
}


