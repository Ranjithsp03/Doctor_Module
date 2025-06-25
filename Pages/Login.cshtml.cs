using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Doctor_Module.Models.Doctor;
 
namespace Doctor_Module.Pages
{
    public class LoginModel : PageModel
    {
        private readonly AppDbContext _context;
 
        public LoginModel(AppDbContext context)
        {
            _context = context;
        }
 
        [BindProperty]
        [Required(ErrorMessage = "Please Enter Email here.")]
        [EmailAddress]
        public string Email { get; set; }
 
        [BindProperty]
        [Required(ErrorMessage = "Please Enter Password here.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
 
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();
 
            var doctor = _context.Doctors.FirstOrDefault(d => d.Email == Email);
 
            if (doctor == null)
            {
                ModelState.AddModelError("Email", "No account exists with this email. Please sign up first.");
                return Page();
            }
 
            if (string.IsNullOrEmpty(Password))
            {
                ModelState.AddModelError("Password", "Please Enter Password here.");
                return Page();
            }
 
            if (doctor.Password != Password)
            {
                ModelState.AddModelError("Password", "Invalid password.");
                return Page();
            }
 
            TempData["DoctorID"] = doctor.DoctorID;
            return RedirectToPage("/DoctorDashboard");
        }
    }
}