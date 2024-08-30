using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using _2024AMS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace _2024AMS.Pages.Users;

[BindProperties]

public class MaintainUsersModel : PageModel
{

    private readonly _2024AMS.Models._2024AMSContext _2024AMSContext;

    public MaintainUsersModel(_2024AMS.Models._2024AMSContext AMS)
    {
        _2024AMSContext = AMS;
    }

    public string Filter { get; set; }

    public class JoinResult
    {
        public int UserID;
        public string? FirstName;
        public string? LastName;
        public string? MiddleInitial;
        public string? EmailAddress;
        public string? Status;
    }

    public IList<JoinResult> JoinResultIList;
    private IQueryable<JoinResult> JoinResultIQueryable; // Create a private Iqueryable.

    public async Task OnGetAsync()
    {
        // Set the message.
        ViewData["Title"] = "Maintain Users";

        if (TempData["MessageColor"] == null)
        {
            TempData["MessageColor"] = "Green";
            TempData["Message"] = "This shows all the people currently in the system and their status.";
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
            from u in _2024AMSContext.User
            orderby u.LastName
            select new JoinResult
            {
                UserID = u.UserID,
                LastName = u.LastName,
                FirstName = u.FirstName,
                MiddleInitial = u.MiddleInitial,
                EmailAddress = u.EmailAddress,
                Status = u.Status,
            });

        //// If a filter option is selected, modify the database query.
        //if (Filter != null)
        //{
        //    JoinResultIQueryable = JoinResultIQueryable
        //        .Where(jr => jr.LastName.Contains(Filter) || jr.FirstName.Contains(Filter) || jr.MiddleInitial.Contains(Filter) || jr.Status.Contains(Filter));
        //}

        // Retrieve the rows for display.
        JoinResultIList = await JoinResultIQueryable.ToListAsync();
    }
    public async Task OnPostFilterAsync()
    {
        await OnGetAsync();
    }
}

