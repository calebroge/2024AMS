using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using _2024AMS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;

namespace _2024AMS.Pages.Users;

[BindProperties]

public class DeleteUserModel : PageModel
{
    private readonly _2024AMS.Models._2024AMSContext _2024AMSContext;

    public DeleteUserModel(_2024AMS.Models._2024AMSContext SPC)
    {
        _2024AMSContext = SPC;
    }
    
    private User User { get; set; }

    public async Task<IActionResult> OnGetAsync(int intUserID)
    {
        // Attempt to retrieve the row from the table.
        User = await _2024AMSContext.User
            .Where(c => c.UserID == intUserID)
            .FirstOrDefaultAsync();

        if (User != null)
        {
            try
            {
                // Delete the row to the table.
                _2024AMSContext.User.Remove(User);
                await _2024AMSContext.SaveChangesAsync();

                // Set the message.
                TempData["Message"] = User.FirstName + " " + User.LastName + " was successfully deleted.";
                TempData["MessageColor"] = "Green";
            }
            catch (DbUpdateException objDbUpdateException)
            {
                // A database exception occurred.
                SqlException objSqlException = objDbUpdateException
                    .InnerException as SqlException;
                if (objSqlException.Number == 547)
                {
                    // A foreign key constraint database exception occurred.
                    // Set the message.
                    TempData["MessageColor"] = "Red";
                    TempData["Message"] = User.FirstName + " " + User.LastName + " was NOT deleted because " +
                        "it is associated with one or more users. To delete this user, you must first delete the associated users.";
                }
                else
                {
                    // Set the message.
                    // A database exception occured while saving to the database.
                    TempData["MessageColor"] = "Red";
                    TempData["Message"] = User.FirstName + " " + User.MiddleInitial + " " + User.LastName + " was NOT deleted. " +
                        "Please report this message to Robert E. Beasley: ...: " +
                        objDbUpdateException.InnerException.Message;
                }
            }
        }
        else
        {
            // Set the message.
            // Even though someone else deleted the item first,
            // we will still inform the user that the User was
            // deleted successsfully.
            TempData["MessageColor"] = "Green";
            TempData["Message"] = "The User was successfully deleted.";
        }
        return Redirect("MaintainUsers");

    }
}

