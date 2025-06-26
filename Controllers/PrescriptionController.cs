using Doctor_Module.Models.Prescription;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Doctor_Module.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PrescriptionController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/prescription/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Prescription>> GetPrescriptionById(string id)
        {
            var prescription = await _context.Prescriptions.FindAsync(id);
            if (prescription == null)
            {
                return NotFound();
            }
            return prescription;
        }

        // POST: api/prescription
        [HttpPost]
        public async Task<ActionResult<Prescription>> CreatePrescription(Prescription prescription)
        {
            prescription.PrescriptionID = Guid.NewGuid().ToString();
            _context.Prescriptions.Add(prescription);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPrescriptionById), new { id = prescription.PrescriptionID }, prescription);
        }

        // GET: api/prescription/bydoctor/{doctorId}
        [HttpGet("bydoctor/{doctorId}")]
        public async Task<ActionResult<IEnumerable<Prescription>>> GetPrescriptionsByDoctor(string doctorId)
        {
            var prescriptions = await _context.Prescriptions
                .Where(p => p.DoctorID == doctorId)
                .ToListAsync();

            return prescriptions;
        }

        // GET: api/prescription/bypatient/{patientId}
        [HttpGet("bypatient/{patientId}")]
        public async Task<ActionResult<IEnumerable<Prescription>>> GetPrescriptionsByPatient(string patientId)
        {
            var prescriptions = await _context.Prescriptions
                .Where(p => p.PatientID == patientId)
                .ToListAsync();

            return prescriptions;
        }
    }
}
