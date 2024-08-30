using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using _2024AMS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace _2024AMS.Pages.Users;

[BindProperties]

public class ModifyUserModel : PageModel
{
    private readonly _2024AMS.Models._2024AMSContext _2024AMSContext;

    public ModifyUserModel(_2024AMS.Models._2024AMSContext SPC)
    {
        _2024AMSContext = SPC;
    }

  //  public SelectList StatusSelectList; // Creates a select list.

    public User User { get; set; }

    public async Task<IActionResult> OnGetAsync(int intUserID)
    {
        // Set the message.
        ViewData["Title"] = "Modify User";
        TempData["MessageColor"] = "Green";
        TempData["Message"] = "Please modify the information below and click modify.";


        // Attempt to retrieve the row from the table.
        User = await _2024AMSContext.User
            .Where(c => c.UserID == intUserID)
            .FirstOrDefaultAsync();

        if (User != null)
        {
            // Return the page to the user.
            return Page();
        }
        else
        {
            // Set the message.
            TempData["MessageColor"] = "Red";
            TempData["Message"] = "The selected user was deleted by someone else.";
            return Redirect("MaintainUsers");
        }
    }

    public async Task<IActionResult> OnPostModifyAsync()
    {
        try
        {
            // Add the row to the table.
            _2024AMSContext.User.Update(User);
            await _2024AMSContext.SaveChangesAsync();

            // Set the message.
            TempData["Message"] = User.FirstName + " " + User.LastName + " was successfully modified.";
            TempData["MessageColor"] = "Green";
        }
        catch (DbUpdateException objDbUpdateException)
        {
            // A database update exception occurred while saving to the database.
            // Set the message.
            TempData["MessageColor"] = "Red";
            TempData["Message"] = User.FirstName + " " + User.LastName + " was NOT modified. " +
                "Please report this message to Robert E. Beasley: ...: " +
                objDbUpdateException.InnerException.Message;
        }
        return Redirect("MaintainUsers");
    }
}

