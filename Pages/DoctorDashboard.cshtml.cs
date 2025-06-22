using Doctor_Module.Models.Doctor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class DoctorDashboardModel : PageModel
{
    private readonly AppDbContext _context;

    public DoctorDashboardModel(AppDbContext context)
    {
        _context = context;
    }

    public Doctor Doctor { get; set; }

    public IActionResult OnGet()
    {
        if (TempData["DoctorID"] == null)
            return RedirectToPage("/Login");

        string doctorId = TempData["DoctorID"].ToString();

        Doctor = _context.Doctors.FirstOrDefault(d => d.DoctorID == doctorId);
        if (Doctor == null)
        {
            return RedirectToPage("/Login");
        }

        TempData.Keep("DoctorID"); // Optional: Keep it for further requests
        return Page();
    }
}
