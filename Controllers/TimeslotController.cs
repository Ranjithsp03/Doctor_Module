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

        // POST: api/timeslot/add
        //Check the doctor exist or not and post the timeslot
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

        // GET: api/timeslot/{id}
        //Get the time slot for the particular doctor
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTimeslot(int id)
        {
            var slot = await _context.Timeslots
                .Include(t => t.doctor)
                .FirstOrDefaultAsync(t => t.TimeSlotID == id);

            return slot == null ? NotFound("Timeslot not found.") : Ok(slot);
        }

        // GET: api/timeslot/all
        //Get all the timeslots in the database
        [HttpGet("all")]
        public async Task<IActionResult> GetAllTimeslots()
        {
            var slots = await _context.Timeslots
                .Include(t => t.doctor)
                .Select(t => new
                {
                    t.TimeSlotID,
                    t.DoctorID,
                    t.Date,
                    t.Start_Time,
                    t.End_Time,
                    t.count,
                    DoctorName = t.doctor.Name
                }).ToListAsync();

            return Ok(slots);
        }

        // PUT: api/timeslot/{id}
        //Update the timeslot
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTimeslot(int id, Timeslot updatedSlot)
        {
            var existingSlot = await _context.Timeslots.FindAsync(id);
            if (existingSlot == null)
                return NotFound("Timeslot not found.");

            existingSlot.Date = updatedSlot.Date;
            existingSlot.Start_Time = updatedSlot.Start_Time;
            existingSlot.End_Time = updatedSlot.End_Time;
            existingSlot.count = updatedSlot.count;
            existingSlot.DoctorID = updatedSlot.DoctorID;

            await _context.SaveChangesAsync();
            return Ok(existingSlot);
        }

        // DELETE: api/timeslot/{id}
        //Delete the timeslot by checking whether it is available or not
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
