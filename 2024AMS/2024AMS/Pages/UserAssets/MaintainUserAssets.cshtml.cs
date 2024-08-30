using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using _2024AMS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;

namespace _2024AMS.Pages.UserAssets;

[BindProperties]
public class MaintainUserAssetsModel : PageModel
{

    private readonly _2024AMSContext _2024AMSContext;
    public MaintainUserAssetsModel(_2024AMSContext AMS)
    {
        _2024AMSContext = AMS;
    }

    public class JoinResult
    {
        public int UserAssetID;
        public int? AssetID;
        public int? UserID;
        public int? AssetCategoryID;
        public string? LastName;
        public string? FirstName;
        public string? Asset1;
        public string? AssetCategory1;        
        public DateTime? CheckedOutDate;
        public DateTime? CheckedInDate;
    }

    public string Filter { get; set; }
    public int AssetCategoryID { get; set; }
    
    public SelectList AssetCategorySelectList;
    public SelectList ManufacturerSelectList;
    public SelectList OperatingSystemSelectList;
    private IQueryable<JoinResult> JoinResultIQueryable;
    public IList<JoinResult> JoinResultIList;

    public async Task OnGetAsync()
    {
        // Set the message.
        ViewData["Title"] = "Maintain User Assets";
        if (TempData["MessageColor"] == null)
        {
            TempData["MessageColor"] = "Green";
            TempData["Message"] = "This shows all the assets that have been assigned to users in the system and their check out dates.";
        }
        else
        {
            ViewData["MessageColor"] =
            HttpContext.Session.GetString("MessageColor");
            ViewData["Message"] =
                HttpContext.Session.GetString("Message");
        }

        // Populate the category select list.
        AssetCategorySelectList = new SelectList(_2024AMSContext.AssetCategory
            .OrderBy(c => c.AssetCategory1), "AssetCategoryID", "AssetCategory1");


        await RetrieveRowsForDisplay();
    }
    public async Task OnPostFilterAsync()
    {
        // Populate the category select list.
        AssetCategorySelectList = new SelectList(_2024AMSContext.AssetCategory
            .OrderBy(c => c.AssetCategory1), "AssetCategoryID", "AssetCategory1");

        // Populate the manufacturer select list.
        ManufacturerSelectList = new SelectList(_2024AMSContext.Manufacturer
            .OrderBy(s => s.Manufacturer1), "ManufacturerID", "Manufacturer1");

        //// Populate the operating system select list.
        //OperatingSystemSelectList = new SelectList(_2024AMSContext.OperatingSystem
        //    .OrderBy(o => o.OperatingSystem1), "OperatingSystemID", "OperatingSystem1");

        await RetrieveRowsForDisplay();

        ViewData["Title"] = "Maintain Assets";
        TempData["MessageColor"] = "Green";
        TempData["Message"] = "The assets has been filtered.";
    }

    private async Task RetrieveRowsForDisplay()
    {
        // Define the database query.
        JoinResultIQueryable = (
            from ua in _2024AMSContext.UserAsset
            join u in _2024AMSContext.User on ua.UserID equals u.UserID
            join a in _2024AMSContext.Asset on ua.AssetID equals a.AssetID
            join c in _2024AMSContext.AssetCategory on a.AssetCategoryID equals c.AssetCategoryID
            orderby u.LastName, a.Asset1, c.AssetCategory1
            select new JoinResult
            {
                UserAssetID = ua.UserAssetID,
                AssetID = a.AssetID,
                UserID = u.UserID,
                AssetCategoryID = c.AssetCategoryID,             
                LastName = u.LastName,
                FirstName = u.FirstName,
                Asset1 = a.Asset1,
                AssetCategory1 = c.AssetCategory1,
                CheckedOutDate = ua.CheckedOutDate,
                CheckedInDate = ua.CheckedInDate
            }); ;

        // If a filter option was selected, modify the database query.
        if (Filter != null)
        {
            JoinResultIQueryable = JoinResultIQueryable
                .Where(jr => jr.Asset1.Contains(Filter) || 
                jr.LastName.Contains(Filter) || jr.FirstName.Contains(Filter));
        }

        if (AssetCategoryID != 0)
        {
            JoinResultIQueryable = JoinResultIQueryable
                .Where(jr => jr.AssetCategoryID == AssetCategoryID);
        }

        // Retrieve the rows for display.
        JoinResultIList = await JoinResultIQueryable.ToListAsync();

    }
}