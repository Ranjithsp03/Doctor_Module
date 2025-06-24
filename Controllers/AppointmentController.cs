using Appointments.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Appointments.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AppointmentController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Appointment/all
        [HttpGet("all")]
        public async Task<IActionResult> GetAllAppointments()
        {
            var appointments = await _context.Appointments.ToListAsync();
            return Ok(appointments);
        }

        // GET: api/Appointment/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointmentById(Guid id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
                return NotFound("Appointment not found.");

            return Ok(appointment);
        }

        // GET: api/Appointment/doctor/{doctorId}
        [HttpGet("doctor/{doctorId}")]
        public async Task<IActionResult> GetAppointmentsByDoctor(string doctorId)
        {
            var appointments = await _context.Appointments
                .Where(a => a.DoctorId == doctorId)
                .ToListAsync();

            return Ok(appointments);
        }

        // GET: api/Appointment/doctor/{doctorId}/today
        [HttpGet("doctor/{doctorId}/today")]
        public async Task<IActionResult> GetTodaysAppointments(string doctorId)
        {
            var today = DateTime.Today;
            var appointments = await _context.Appointments
                .Where(a => a.DoctorId == doctorId && a.TimeSlot.Date == today && !a.IsCancelled)
                .ToListAsync();

            return Ok(appointments);
        }

        // POST: api/Appointment
        [HttpPost]
        public async Task<IActionResult> CreateAppointment([FromBody] Appointment appointment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            appointment.AppointmentId = Guid.NewGuid();
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAppointmentById), new { id = appointment.AppointmentId }, appointment);
        }

        // PUT: api/Appointment/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(Guid id, [FromBody] Appointment updatedAppointment)
        {
            if (id != updatedAppointment.AppointmentId)
                return BadRequest("Appointment ID mismatch.");

            var existingAppointment = await _context.Appointments.FindAsync(id);
            if (existingAppointment == null)
                return NotFound("Appointment not found.");

            // Update fields
            existingAppointment.PatientId = updatedAppointment.PatientId;
            existingAppointment.DoctorId = updatedAppointment.DoctorId;
            existingAppointment.Remarks = updatedAppointment.Remarks;
            existingAppointment.Reason = updatedAppointment.Reason;
            existingAppointment.PaymentStatus = updatedAppointment.PaymentStatus;
            existingAppointment.IsCancelled = updatedAppointment.IsCancelled;
            existingAppointment.TimeSlot = updatedAppointment.TimeSlot;

            await _context.SaveChangesAsync();
            return Ok(existingAppointment);
        }

        // PUT: api/Appointment/{id}/approve
        [HttpPut("{id}/approve")]
        public async Task<IActionResult> ApproveAppointment(Guid id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
                return NotFound("Appointment not found.");

            if (appointment.IsCancelled)
                return BadRequest("Cannot approve a cancelled appointment.");

            appointment.PaymentStatus = true;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Appointment approved.", appointment });
        }

        // PUT: api/Appointment/{id}/decline
        [HttpPut("{id}/decline")]
        public async Task<IActionResult> DeclineAppointment(Guid id, [FromBody] string remarks)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
                return NotFound("Appointment not found.");

            appointment.IsCancelled = true;
            appointment.Remarks = remarks;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Appointment declined.", appointment });
        }

        // DELETE: api/Appointment/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(Guid id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
                return NotFound("Appointment not found.");

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Appointment deleted." });
        }
    }
}
