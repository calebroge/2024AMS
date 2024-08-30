using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using _2024AMS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;

namespace _2024AMS.Pages.AssetCategories;

[BindProperties]
public class MaintainAssetCategoriesModel : PageModel
{

    private readonly _2024AMSContext _2024AMSContext;
    public MaintainAssetCategoriesModel(_2024AMSContext AMS)
    {
        _2024AMSContext = AMS;
    }

    public class JoinResult
    {
        public int? AssetCategoryID;
        public string? AssetCategory;
    }

    private IQueryable<JoinResult> JoinResultIQueryable;
    public IList<JoinResult> JoinResultIList;

    public async Task OnGetAsync()
    {
        // Set the message.
        ViewData["Title"] = "Maintain Asset Categories";
        if (TempData["MessageColor"] == null)
        {
            TempData["MessageColor"] = "Green";
            TempData["Message"] = "This shows all the asset categories currently in the system.";
        }
        else
        {
            ViewData["MessageColor"] =
            HttpContext.Session.GetString("MessageColor");
            ViewData["Message"] =
                HttpContext.Session.GetString("Message");
        }


        // Define the database query.
        JoinResultIQueryable = (
            from c in _2024AMSContext.AssetCategory
            orderby c.AssetCategory1
            select new JoinResult
            {
                AssetCategoryID = c.AssetCategoryID,
                AssetCategory = c.AssetCategory1,

            }); ;

        // Retrieve the rows for display.
        JoinResultIList = await JoinResultIQueryable.ToListAsync();
        
    }
}