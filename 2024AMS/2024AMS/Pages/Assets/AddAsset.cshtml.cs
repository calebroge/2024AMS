using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _2024AMS.Models;

namespace _2024AMS.Pages.Assets;

[BindProperties]
public class AddAssetModel : PageModel
{
    private readonly _2024AMSContext _2024AMSContext;
    public AddAssetModel(_2024AMSContext AMS)
    {
        _2024AMSContext = AMS;
    }

    public SelectList AssetCategorySelectList;
    public SelectList ManufacturerSelectList;
    public SelectList OperatingSystemSelectList;

    public Asset Asset { get; set; }

    public void OnGet()
    {
        // Set the message.
        ViewData["Title"] = "Add Asset";
        TempData["MessageColor"] = "Green";
        TempData["Message"] = "Please add the information below and click add.";

        // Populate the category select list.
        AssetCategorySelectList = new SelectList(_2024AMSContext.AssetCategory
            .OrderBy(c => c.AssetCategory1), "AssetCategoryID", "AssetCategory1");

        // Populate the manufacturer select list.
        ManufacturerSelectList = new SelectList(_2024AMSContext.Manufacturer
            .OrderBy(s => s.Manufacturer1), "ManufacturerID", "Manufacturer1");

        // Populate the operating system select list.
        OperatingSystemSelectList = new SelectList(_2024AMSContext.OperatingSystem
            .OrderBy(o => o.OperatingSystem1), "OperatingSystemID", "OperatingSystem1");
    }

    public async Task<IActionResult> OnPostAddAsync()
    {

        try
        {
            // Add the row to the table.
            _2024AMSContext.Asset.Add(Asset);
            await _2024AMSContext.SaveChangesAsync();
            // Set the message.
            TempData["MessageColor"] = "Green";
            TempData["Message"] = Asset.Asset1 + " was successfully added.";
        }
        catch (DbUpdateException objDbUpdateException)
        {
            // A database exception occurred while saving to the
            // database.
            // Set the message.
            TempData["MessageColor"] = "Red";
            TempData["Message"] = Asset.Asset1 + " was NOT added. Please report this message to...: " + objDbUpdateException.InnerException.Message;
        }
        return Redirect("MaintainAssets");

    }

}
