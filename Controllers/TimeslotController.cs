using Doctor_Module.Models.Doctor;
using Doctor_Module.Timeslots;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Doctor_Module.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimeslotController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TimeslotController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddTimeslot(Timeslot timeslot)
        {
            var doctorExists = await _context.Doctors.AnyAsync(d => d.DoctorID == timeslot.DoctorID);
            if (!doctorExists)
                return BadRequest("Doctor does not exist.");

            _context.Timeslots.Add(timeslot);
            await _context.SaveChangesAsync();
            return Ok(timeslot);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTimeslot(int id)
        {
            var slot = await _context.Timeslots.Include(t => t.Doctor).FirstOrDefaultAsync(t => t.TimeSlotID == id);
            return slot == null ? NotFound("Timeslot not found.") : Ok(slot);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllTimeslots()
        {
            var slots = await _context.Timeslots.Include(t => t.Doctor).Select(t => new
            {
                t.TimeSlotID,
                t.DoctorID,
                t.Date,
                t.StartTime,
                t.EndTime,
                t.Count,
                t.IsAvailable,
                DoctorName = t.Doctor.Name
            }).ToListAsync();

            return Ok(slots);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTimeslot(int id, Timeslot updatedSlot)
        {
            var existingSlot = await _context.Timeslots.FindAsync(id);
            if (existingSlot == null)
                return NotFound("Timeslot not found.");

            existingSlot.Date = updatedSlot.Date;
            existingSlot.StartTime = updatedSlot.StartTime;
            existingSlot.EndTime = updatedSlot.EndTime;
            existingSlot.IsAvailable = updatedSlot.IsAvailable;
            existingSlot.Count = updatedSlot.Count;
            existingSlot.DoctorID = updatedSlot.DoctorID;

            await _context.SaveChangesAsync();
            return Ok(existingSlot);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTimeslot(int id)
        {
            var timeslot = await _context.Timeslots.FindAsync(id);
            if (timeslot == null)
                return NotFound("Timeslot not found.");

            _context.Timeslots.Remove(timeslot);
            await _context.SaveChangesAsync();
            return Ok("Timeslot deleted successfully.");
        }
    }
}
