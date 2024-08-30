using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _2024AMS.Models;

namespace _2024AMS.Pages.AssetCategories;

[BindProperties]
public class AddAssetCategoryModel : PageModel
{
    private readonly _2024AMSContext _2024AMSContext;
    public AddAssetCategoryModel(_2024AMSContext AMS)
    {
        _2024AMSContext = AMS;
    }

    public AssetCategory AssetCategory { get; set; }

    public void OnGet()
    {
        // Set the message.
        ViewData["Title"] = "Add Asset Category";
        TempData["MessageColor"] = "Green";
        TempData["Message"] = "Please add the information below and click add.";

    }

    public async Task<IActionResult> OnPostAddAsync()
    {

        try
        {
            // Add the row to the table.
            _2024AMSContext.AssetCategory.Add(AssetCategory);
            await _2024AMSContext.SaveChangesAsync();
            // Set the message.
            TempData["MessageColor"] = "Green";
            TempData["Message"] = AssetCategory.AssetCategory1 + " was successfully added.";
        }
        catch (DbUpdateException objDbUpdateException)
        {
            // A database exception occurred while saving to the
            // database.
            // Set the message.
            TempData["MessageColor"] = "Red";
            TempData["Message"] = AssetCategory.AssetCategory1 + " was NOT added. Please report this message to...: " + objDbUpdateException.InnerException.Message;
        }
        return Redirect("MaintainAssetCategories");

    }

}
