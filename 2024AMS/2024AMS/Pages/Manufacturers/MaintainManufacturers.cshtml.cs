using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using _2024AMS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;

namespace _2024AMS.Pages.Manufacturers;

[BindProperties]
public class MaintainManufacturersModel : PageModel
{

    private readonly _2024AMSContext _2024AMSContext;
    public MaintainManufacturersModel(_2024AMSContext AMS)
    {
        _2024AMSContext = AMS;
    }

    public class JoinResult
    {
        public int? ManufacturerID;
        public string? Manufacturer;
    }

    private IQueryable<JoinResult> JoinResultIQueryable;
    public IList<JoinResult> JoinResultIList;

    public async Task OnGetAsync()
    {
        // Set the message.
        ViewData["Title"] = "Maintain Manufacturers";
        if (TempData["MessageColor"] == null)
        {
            TempData["MessageColor"] = "Green";
            TempData["Message"] = "This shows all the manufacturers currently in the system.";
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
            from m in _2024AMSContext.Manufacturer
            orderby m.Manufacturer1
            select new JoinResult
            {
                ManufacturerID = m.ManufacturerID,
                Manufacturer = m.Manufacturer1,

            }); ;

        // Retrieve the rows for display.
        JoinResultIList = await JoinResultIQueryable.ToListAsync();
        
    }
}