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
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            var doctor = _context.Doctors
                .FirstOrDefault(d => d.Email == Email && d.Password == Password);

            if (doctor != null)
            {
                TempData["DoctorID"] = doctor.DoctorID;
                return RedirectToPage("/DoctorDashboard");
            }

            ModelState.AddModelError("", "Invalid email or password.");
            return Page();
        }
    }
}
