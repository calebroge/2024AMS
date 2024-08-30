using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using _2024AMS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;

namespace _2024AMS.Pages.OperatingSystems;

[BindProperties]
public class MaintainOperatingSystemsModel : PageModel
{

    private readonly _2024AMSContext _2024AMSContext;
    public MaintainOperatingSystemsModel(_2024AMSContext AMS)
    {
        _2024AMSContext = AMS;
    }

    public class JoinResult
    {
        public int? OperatingSystemID;
        public string? OperatingSystem;
    }

    private IQueryable<JoinResult> JoinResultIQueryable;
    public IList<JoinResult> JoinResultIList;

    public async Task OnGetAsync()
    {
        // Set the message.
        ViewData["Title"] = "Maintain Operating Systems";
        if (TempData["MessageColor"] == null)
        {
            TempData["MessageColor"] = "Green";
            TempData["Message"] = "This shows all the operating systems currently in the system.";
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
            from o in _2024AMSContext.OperatingSystem
            orderby o.OperatingSystem1
            select new JoinResult
            {
                OperatingSystemID = o.OperatingSystemID,
                OperatingSystem = o.OperatingSystem1,

            }); ;

        // Retrieve the rows for display.
        JoinResultIList = await JoinResultIQueryable.ToListAsync();
        
    }
}