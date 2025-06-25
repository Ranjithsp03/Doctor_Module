using Doctor_Module.Timeslots;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class TimeSlotEntryModel : PageModel
{
    private readonly AppDbContext _context;

    public TimeSlotEntryModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Timeslot Timeslot { get; set; } = new Timeslot();
  public IActionResult OnGet()
    {
        // Set the date to today's date
        Timeslot.Date = DateTime.Today;
 
        // Fetch DoctorID from TempData
        if (TempData["DoctorID"] is string doctorId && !string.IsNullOrEmpty(doctorId))
        {
            Timeslot.DoctorID = doctorId;
            TempData.Keep("DoctorID");
        }
        else
        {
            TempData["Error"] = "Doctor ID is missing. Please login again.";
            return RedirectToPage("/Login");
        }
 
        return Page();
    }
    public IActionResult OnPost()
    {
        // ✅ Fetch DoctorID from TempData
        if (TempData["DoctorID"] is string doctorId && !string.IsNullOrEmpty(doctorId))
        {
            Timeslot.DoctorID = doctorId;
            TempData.Keep("DoctorID");

            // ✅ Remove DoctorID from model validation since it's not part of the form
            ModelState.Remove("Timeslot.DoctorID");
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Doctor ID is missing. Please login again.");
            return RedirectToPage("/Login");
        }

        // ✅ Validate remaining form fields
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // ✅ Save the timeslot
        _context.Timeslots.Add(Timeslot);
        _context.SaveChanges();

        TempData["SuccessMessage"] = "Time slot recorded successfully!";
        return RedirectToPage("/DoctorDashboard");
    }
}
