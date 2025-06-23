using Doctor_Module.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Doctor_Module.Models.Prescription;
namespace Doctor_Module.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrescriptionController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PrescriptionController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddPrescription([FromBody] Prescription prescription)
        {
            var doctor = await _context.Doctors.FindAsync(prescription.DoctorID);
            if (doctor == null)
                return BadRequest("Invalid Doctor ID");

            // Optional: Validate patient if you have a Patient table
            // var patient = await _context.Patients.FindAsync(prescription.PatientID);

            _context.Prescriptions.Add(prescription);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Prescription added successfully", prescription });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPrescription(string id)
        {
            var prescription = await _context.Prescriptions.FindAsync(id);
            return prescription == null ? NotFound("Prescription not found") : Ok(prescription);
        }

        [HttpGet("doctor/{doctorId}")]
        public async Task<IActionResult> GetPrescriptionsByDoctor(string doctorId)
        {
            var prescriptions = await _context.Prescriptions
                .Where(p => p.DoctorID == doctorId)
                .ToListAsync();

            return Ok(prescriptions);
        }
    }
}
