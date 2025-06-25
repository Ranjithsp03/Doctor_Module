using Doctor_Module.Models.Doctor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

public class RegisterModel : PageModel
{
    private readonly AppDbContext _context;

    public RegisterModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Doctor Doctor { get; set; }
    public List<SelectListItem> Specializations { get; set; }
    public void OnGet()
    {
        LoadSpecializations();
    }



    public async Task<IActionResult> OnPostAsync()
{
    if (!ModelState.IsValid)
        return Page();
 
    // Check if DoctorID already exists
    var existingDoctorByID = await _context.Doctors.FindAsync(Doctor.DoctorID);
    if (existingDoctorByID != null)
    {
        ModelState.AddModelError("Doctor.DoctorID", "This Doctor ID already exists.");
        return Page();
    }
 
    // Check if Email already exists
    var existingDoctorByEmail = _context.Doctors.FirstOrDefault(d => d.Email == Doctor.Email);
    if (existingDoctorByEmail != null)
    {
        ModelState.AddModelError("Doctor.Email", "This email is already registered.");
        return Page();
    }
 
    _context.Doctors.Add(Doctor);
    await _context.SaveChangesAsync();
 
    return RedirectToPage("/Login");
}
    private void LoadSpecializations()
        {
            Specializations = new List<SelectListItem>
            {
                new SelectListItem { Value = "Cardiology", Text = "Cardiology" },
                new SelectListItem { Value = "Neurology", Text = "Neurology" },
                new SelectListItem { Value = "Orthopedics", Text = "Orthopedics" },
                new SelectListItem { Value = "Pediatrics", Text = "Pediatrics" },
                new SelectListItem { Value = "General Medicine", Text = "General Medicine" }
            };
        }
}
