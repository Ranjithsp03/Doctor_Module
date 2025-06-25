using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Appointments.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Appointments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentLogsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AppointmentLogsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/AppointmentLogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointmentLog>>> GetAppointmentLogs()
        {
            return await _context.AppointmentLogs.ToListAsync();
        }

        // GET: api/AppointmentLogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentLog>> GetAppointmentLog(int id)
        {
            var log = await _context.AppointmentLogs.FindAsync(id);

            if (log == null)
            {
                return NotFound();
            }

            return log;
        }

        // POST: api/AppointmentLogs
        [HttpPost]
        public async Task<ActionResult<AppointmentLog>> PostAppointmentLog(AppointmentLog log)
        {
            _context.AppointmentLogs.Add(log);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAppointmentLog), new { id = log.LogId }, log);
        }

        // PUT: api/AppointmentLogs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppointmentLog(int id, AppointmentLog log)
        {
            if (id != log.LogId)
            {
                return BadRequest();
            }

            _context.Entry(log).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentLogExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/AppointmentLogs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointmentLog(int id)
        {
            var log = await _context.AppointmentLogs.FindAsync(id);
            if (log == null)
            {
                return NotFound();
            }

            _context.AppointmentLogs.Remove(log);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AppointmentLogExists(int id)
        {
            return _context.AppointmentLogs.Any(e => e.LogId == id);
        }
    }
}
