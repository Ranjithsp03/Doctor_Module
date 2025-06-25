// AddPrescription.cshtml.cs
using Doctor_Module.Models.Prescription;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

public class AddPrescriptionModel : PageModel
{
    private readonly AppDbContext _context;

    public AddPrescriptionModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Prescription Prescription { get; set; }

    [BindProperty(SupportsGet = true)]
    public string DoctorId { get; set; }

    public IActionResult OnGet()
    {
        if (!string.IsNullOrEmpty(DoctorId))
        {
            TempData["DoctorID"] = DoctorId;
        }

        TempData.Keep("DoctorID");
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (TempData["DoctorID"] == null)
        {
            TempData["Error"] = "Doctor ID is missing. Please login again.";
            return RedirectToPage("/Login");
        }

        string doctorId = TempData["DoctorID"].ToString();
        TempData.Keep("DoctorID");

        Prescription.DoctorID = doctorId;
        Prescription.PrescriptionID = Guid.NewGuid().ToString();

        _context.Prescriptions.Add(Prescription);
        await _context.SaveChangesAsync();

        TempData["Success"] = "Prescription submitted successfully!";
        return RedirectToPage("/ViewAppointments", new { doctorId = doctorId });
    }
}
