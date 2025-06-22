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

   public IActionResult OnPost()
{
    // ✅ Set DoctorID from TempData before model validation
    if (TempData["DoctorID"] != null)
    {
        Timeslot.DoctorID = TempData["DoctorID"].ToString();
        TempData.Keep("DoctorID");
    }
    else
    {
        ModelState.AddModelError("", "Doctor ID is missing. Please login again.");
        return RedirectToPage("/Login");
    }

    // ✅ Now validation will succeed
    if (!ModelState.IsValid)
    {
        return Page();
    }

    _context.Timeslots.Add(Timeslot);
    _context.SaveChanges();

    TempData["SuccessMessage"] = "Time slot recorded successfully!";
    return RedirectToPage("/DoctorDashboard");
}

}
