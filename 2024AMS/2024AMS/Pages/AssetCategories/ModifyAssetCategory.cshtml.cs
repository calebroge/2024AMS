using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _2024AMS.Models;

namespace _2024AMS.Pages.AssetCategories;

[BindProperties]
public class ModifyAssetCategoryModel : PageModel
{
    private readonly _2024AMSContext _2024AMSContext;
    public ModifyAssetCategoryModel(_2024AMSContext AMS)
    {
        _2024AMSContext = AMS;
    }

    public AssetCategory AssetCategory { get; set; }

    public async Task<IActionResult> OnGetAsync(int intAssetCategoryID)
    {
        // Set the message.
        ViewData["Title"] = "Modify Asset Category";
        TempData["MessageColor"] = "Green";
        TempData["Message"] = "Please modify the information below and click modify.";

        // Attempt to retrieve the row from the table.
        AssetCategory = await _2024AMSContext.AssetCategory.FindAsync(intAssetCategoryID);
        if (AssetCategory != null)
        {
            return Page();
        }
        else
        {
            // Set the message.
            TempData["MessageColor"] = "Red";
            TempData["Message"] = "The selected asset category was deleted by someone else.";
            return Redirect("MaintainAssetCategories");
        }

    }

    public async Task<IActionResult> OnPostModifyAsync()
    {

        try
        {
            // Modify the row in the table.
            _2024AMSContext.AssetCategory.Update(AssetCategory);
            await _2024AMSContext.SaveChangesAsync();
            // Set the message.
            TempData["MessageColor"] = "Green";
            TempData["Message"] = AssetCategory.AssetCategory1 + " was successfully modified.";
        }
        catch (DbUpdateException objDbUpdateException)
        {
            // A database exception occurred while saving to the
            // database.
            // Set the message.
            TempData["MessageColor"] = "Red";
            TempData["Message"] = AssetCategory.AssetCategory1 + " was NOT modified. Please report this message to...: " + objDbUpdateException.InnerException.Message;
        }
        return Redirect("MaintainAssetCategories");

    }

}
