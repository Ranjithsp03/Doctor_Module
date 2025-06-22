using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
namespace Doctor_Module.Pages;
public class LoginModel : PageModel
{
    [BindProperty]
    [Required]
    public string Username { get; set; }

    [BindProperty]
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // TODO: Validate credentials via Web API or service
        if (Username == "admin" && Password == "admin")
        {
            // Redirect on success (dashboard or homepage)
            return RedirectToPage("/Doctors/Index");
        }

        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        return Page();
    }
}
