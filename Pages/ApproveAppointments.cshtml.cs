using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Appointments.Model; // ✅ Use the correct AppointmentLog model
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doctor_Module.Pages
{
    public class ApproveAppointmentsModel : PageModel
    {
        private readonly AppDbContext _context;

        public ApproveAppointmentsModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string DoctorId { get; set; }

        public List<Appointment> Appointments { get; set; }

        public async Task OnGetAsync()
        {
            Appointments = await _context.Appointments
                .Where(a => a.DoctorId == DoctorId && !a.IsApproved && !a.IsCancelled)
                .OrderBy(a => a.TimeSlot)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostApproveAsync(Guid id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
                return NotFound();

            appointment.IsApproved = true;

            // ✅ Insert into AppointmentLog from Appointments.Model
            var log = new AppointmentLog
            {
                AppointmentId = appointment.AppointmentId,
                PatientId = appointment.PatientId,
                DoctorID=appointment.DoctorId,
                Reason = appointment.Reason,
                TimeSlot = appointment.TimeSlot,
                ApprovedAt = DateTime.UtcNow
            };

            _context.AppointmentLogs.Add(log);
            await _context.SaveChangesAsync();

            return RedirectToPage(new { doctorId = appointment.DoctorId });
        }

        public async Task<IActionResult> OnPostDeclineAsync(Guid id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
                return NotFound();

            appointment.IsCancelled = true;
            await _context.SaveChangesAsync();

            return RedirectToPage(new { doctorId = appointment.DoctorId });
        }
    }
}
