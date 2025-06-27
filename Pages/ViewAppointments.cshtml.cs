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
        [BindProperty(SupportsGet = true)]
        public DateTime? FilterDate { get; set; }
 

        public List<AppointmentLog> Appointments { get; set; }
        public string DoctorId { get; set; }


        public async Task OnGetAsync(string doctorId)
        {
            DoctorId = doctorId;

            if (string.IsNullOrEmpty(doctorId))
            {
                Appointments = new List<AppointmentLog>();
                return;
            }
             if (!FilterDate.HasValue)
    {
        FilterDate = DateTime.Today;
    }

   
 var selectedDate = FilterDate.Value.Date;
    var nextDate = selectedDate.AddDays(1);

    var query = await _context.AppointmentLogs
        .Where(a => a.DoctorID == doctorId &&
                    a.TimeSlot >= selectedDate &&
                    a.TimeSlot < nextDate)
        .ToListAsync(); // still async and SQL-safe

    // in-memory ordering
    Appointments = query
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
