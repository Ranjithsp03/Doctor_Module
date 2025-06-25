using Doctor_Module.Models.Doctor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
 
public class EditDoctorModel : PageModel
{
    private readonly AppDbContext _context;
 
    public EditDoctorModel(AppDbContext context)
    {
        _context = context;
    }
 
    [BindProperty]
    public Doctor Doctor { get; set; }
 
    public async Task<IActionResult> OnGetAsync()
    {
        // ✅ Fetch DoctorID from TempData
        if (TempData["DoctorID"] == null)
        {
            TempData["Error"] = "Doctor ID is missing. Please login again.";
            return RedirectToPage("/Login");
        }
 
        string doctorId = TempData["DoctorID"].ToString();
        TempData.Keep("DoctorID");
 
        Doctor = await _context.Doctors.FindAsync(doctorId);
        if (Doctor == null)
        {
            TempData["Error"] = "Doctor not found.";
            return RedirectToPage("/DoctorDashboard");
        }
 
        return Page();
    }
 
    public async Task<IActionResult> OnPostAsync()
    {
        // ✅ Fetch DoctorID again for update
        if (TempData["DoctorID"] == null)
        {
            TempData["Error"] = "Doctor ID is missing. Please login again.";
            return RedirectToPage("/Login");
        }
 
        string doctorId = TempData["DoctorID"].ToString();
        TempData.Keep("DoctorID");
 
        var existingDoctor = await _context.Doctors.FindAsync(doctorId);
        if (existingDoctor == null)
        {
            TempData["Error"] = "Doctor not found.";
            return RedirectToPage("/DoctorDashboard");
        }
 
        // ✅ Check if password is entered
        if (string.IsNullOrEmpty(Doctor.Password))
        {
            TempData["Error"] = "Please enter the password.";
            return Page();
        }
 
        // ✅ Check if the entered password matches the existing password
        if (Doctor.Password != existingDoctor.Password)
        {
            TempData["Error"] = "Incorrect password. Please enter the correct password.";
            return Page();
        }
 
        // ✅ Update fields
        existingDoctor.Name = Doctor.Name;
        existingDoctor.Specialization = Doctor.Specialization;
        existingDoctor.Experiance = Doctor.Experiance;
        existingDoctor.Email = Doctor.Email;
 
        await _context.SaveChangesAsync();
 
        TempData["SuccessMessage"] = "Profile updated successfully!";
        return RedirectToPage("/DoctorDashboard");
    }
}
 