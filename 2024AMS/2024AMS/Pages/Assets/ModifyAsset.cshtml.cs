using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _2024AMS.Models;

namespace _2024AMS.Pages.Assets;

[BindProperties]
public class ModifyAssetModel : PageModel
{
    private readonly _2024AMSContext _2024AMSContext;
    public ModifyAssetModel(_2024AMSContext AMS)
    {
        _2024AMSContext = AMS;
    }

    public SelectList AssetCategorySelectList;
    public SelectList ManufacturerSelectList;
    public SelectList OperatingSystemSelectList;

    public Asset Asset { get; set; }

    public async Task<IActionResult> OnGetAsync(int intAssetID)
    {
        // Set the message.
        ViewData["Title"] = "Modify Asset";
        TempData["MessageColor"] = "Green";
        TempData["Message"] = "Please modify the information below and click modify.";

        // Attempt to retrieve the row from the table.
        Asset = await _2024AMSContext.Asset
            .Where(a => a.AssetID == intAssetID)
            .FirstOrDefaultAsync();

        if (Asset != null)
        {
            // Populate the asset category select list.
            AssetCategorySelectList = new SelectList(_2024AMSContext.AssetCategory
                .OrderBy(c => c.AssetCategory1), "AssetCategoryID", "AssetCategory1");

            // Populate the manufacturer select list.
            ManufacturerSelectList = new SelectList(_2024AMSContext.Manufacturer
                .OrderBy(m => m.Manufacturer1), "ManufacturerID", "Manufacturer1");

            // Populate the operating system select list.
            OperatingSystemSelectList = new SelectList(_2024AMSContext.OperatingSystem
                .OrderBy(o => o.OperatingSystem1), "OperatingSystemID", "OperatingSystem1");

            return Page();
        }
        else
        {
            // Set the message.
            TempData["MessageColor"] = "Red";
            TempData["Message"] = "The selected asset was deleted by someone else.";
            return Redirect("MaintainAssets");
        }

    }

    public async Task<IActionResult> OnPostModifyAsync()
    {

        try
        {
            // Modify the row in the table.
            _2024AMSContext.Asset.Update(Asset);
            await _2024AMSContext.SaveChangesAsync();
            // Set the message.
            TempData["MessageColor"] = "Green";
            TempData["Message"] = Asset.Asset1 + " was successfully modified.";
        }
        catch (DbUpdateException objDbUpdateException)
        {
            // A database exception occurred while saving to the
            // database.
            // Set the message.
            TempData["MessageColor"] = "Red";
            TempData["Message"] = Asset.Asset1 + " was NOT modified. Please report this message to...: " + objDbUpdateException.InnerException.Message;
        }
        return Redirect("MaintainAssets");

    }

}
