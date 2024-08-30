using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using _2024AMS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace _2024AMS.Pages.Users;

[BindProperties]

public class AddUserModel : PageModel
{
    public string Message;
    public string MessageColor;

    private readonly _2024AMS.Models._2024AMSContext _2024AMSContext;

    public AddUserModel(_2024AMS.Models._2024AMSContext AMS)
    {
        _2024AMSContext = AMS;
    }

    //public SelectList StatusSelectList; // Creates a select list.

    public User User { get; set; }

    public void OnGet()
    {
        // Set the message.
        ViewData["Title"] = "Add User";
        TempData["MessageColor"] = "Green";
        TempData["Message"] = "Please add the information below and click add.";

        // Populate the select list
      //  StatusSelectList = new SelectList(_2024AMSContext.User.OrderBy(u => u.Status), "FirstName", "Status");
    }

    public async Task<IActionResult> OnPostAddAsync()
    {
        try
        {
            // Add the row to the table.
            _2024AMSContext.User.Add(User);
            await _2024AMSContext.SaveChangesAsync();

            // Set the message.
            TempData["Message"] = User.FirstName + " " + User.LastName + " was successfully added.";
            TempData["MessageColor"] = "Green";
        }
        catch (DbUpdateException objDbUpdateException)
        {
            // A database update exception occurred while saving to the database.
            // Set the message.
            TempData["MessageColor"] = "Red";
            TempData["Message"] = User.FirstName + " " + User.MiddleInitial + " " + User.LastName + " was NOT added. " +
                "Please report this message to Robert E. Beasley: ...: " +
                objDbUpdateException.InnerException.Message;
        }
        return Redirect("MaintainUsers");
    }
}


