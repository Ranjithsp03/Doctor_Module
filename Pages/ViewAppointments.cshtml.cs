using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Appointments.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Doctor_Module.Pages
{
    public class ViewAppointmentsModel : PageModel
    {
        private readonly AppDbContext _context;

        public ViewAppointmentsModel(AppDbContext context)
        {
            _context = context;
        }

        public List<AppointmentLog> Appointments { get; set; }

  public async Task OnGetAsync(string doctorId)
{
    if (string.IsNullOrEmpty(doctorId))
    {
        Appointments = new List<AppointmentLog>();
        return;
    }

            // ✅ Now this will work because ApprovedAt is DateTime
            Appointments = await _context.AppointmentLogs
                .Where(a => a.DoctorID == doctorId)
                .ToListAsync();
            Appointments = Appointments
        .OrderByDescending(a => a.ApprovedAt.UtcDateTime)
        .ToList();
}

        public async Task<IActionResult> OnPostCancelAsync(Guid id)
        {
            var log = await _context.AppointmentLogs.FindAsync(id);
            if (log != null)
            {
                _context.AppointmentLogs.Remove(log);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}
