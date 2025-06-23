// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;

// [ApiController]
// [Route("api/[controller]")]
// public class AppointmentController : ControllerBase
// {
//     private readonly AppDbContext _context;

//     private readonly Dictionary<string, string> issueToSpecialization = new()
// {
//     { "heart pain", "Cardiologist" },
//     { "skin rash", "Dermatologist" },
//     { "cough", "General Physician" },
//     { "fever", "General Physician" },
//     { "kidney pain", "Nephrologist" },
//     { "headache", "Neurologist" }
// };

//     public AppointmentController(AppDbContext context)
//     {
//         _context = context;
//     }

//     [HttpPost("create")]
//     public async Task<IActionResult> CreateAppointment([FromBody] Appointment appointment)
//     {
//         // Validate doctor
//         var doctor = await _context.Doctors.FindAsync(appointment.DoctorID);
//         if (doctor == null || doctor.Name != appointment.DoctorName || doctor.Specialization != appointment.Specialization)
//             return BadRequest("Doctor info is invalid.");

//         // Check available timeslot for that doctor
//         // var matchingSlot = await _context.Timeslots.FirstOrDefaultAsync(slot =>
//         //     slot.DoctorID == appointment.DoctorID &&
//         //     slot.Date == appointment.Date_Time.ToString("yyyy-MM-dd") &&
//         //     slot.Start_Time == appointment.Date_Time.ToString("HH:mm") &&
//         //     slot.count > 0);

//         if (matchingSlot == null)
//             return BadRequest("No available timeslot for the selected time.");

//         // Assign timeslot and decrement count
//         appointment.TimeSlotID = matchingSlot.TimeSlotID;
//         appointment.Status = "Pending";
//         appointment.Prescription = "none";
//         appointment.Prescription_ID = Guid.NewGuid().ToString();

//         _context.Appointments.Add(appointment);
//         matchingSlot.count -= 1;  // Reduce slot availability
//         await _context.SaveChangesAsync();

//         return Ok(appointment);
//     }

//     [HttpGet("getall")]
//     public async Task<IActionResult> GetAllAppointments()
//     {
//         var appointments = await _context.Appointments.ToListAsync();
//         return Ok(appointments);
//     }

//     [HttpGet("doctor/{doctorId}")]
//     public async Task<IActionResult> GetAppointmentsByDoctor(string doctorId)
//     {
//         var appointments = await _context.Appointments
//             .Where(a => a.DoctorID == doctorId)
//             .ToListAsync();
//         return Ok(appointments);
//     }

//     [HttpGet("doctor/{doctorId}/status/{status}")]
//     public async Task<IActionResult> GetAppointmentsByStatus(string doctorId, string status)
//     {
//         var appointments = await _context.Appointments
//             .Where(a => a.DoctorID == doctorId && a.Status.ToLower() == status.ToLower())
//             .ToListAsync();
//         return Ok(appointments);
//     }

//     [HttpPut("{id}/accept")]
//     public async Task<IActionResult> AcceptAppointment(int id)
//     {
//         var appointment = await _context.Appointments.FindAsync(id);
//         if (appointment == null) return NotFound();

//         appointment.Status = "Accepted";
//         await _context.SaveChangesAsync();
//         return Ok(appointment);
//     }

//     [HttpPut("{id}/decline")]
//     public async Task<IActionResult> DeclineAppointment(int id)
//     {
//         var appointment = await _context.Appointments.FindAsync(id);
//         if (appointment == null) return NotFound();

//         appointment.Status = "Declined";
//         await _context.SaveChangesAsync();
//         return Ok(appointment);
//     }

//     public class PrescriptionInput
//     {
//         public string Text { get; set; }
//     }

//     [HttpPut("{id}/prescription")]
//     public async Task<IActionResult> AddPrescription(int id, [FromBody] PrescriptionInput input)
//     {
//         var appointment = await _context.Appointments.FindAsync(id);
//         if (appointment == null) return NotFound("Appointment not found");

//         if (appointment.Status != "Accepted")
//             return BadRequest("Prescription can only be added after the appointment is accepted.");

//         appointment.Prescription = input.Text;
//         appointment.Status = "Completed";

//         await _context.SaveChangesAsync();
//         return Ok(new
//         {
//             message = "Prescription added successfully",
//             prescription = appointment.Prescription
//         });
//     }
//     [HttpPut("{id}/feedback")]
//     public async Task<IActionResult> AddFeedback(int id, [FromBody] string feedback)
//     {
//         var appointment = await _context.Appointments.FindAsync(id);
//         if (appointment == null) return NotFound("Appointment not found");

//         if (appointment.Status != "Completed")
//             return BadRequest("Feedback can only be added after completion");

//         appointment.Feedback = feedback;
//         await _context.SaveChangesAsync();

//         return Ok(new
//         {
//             message = "Feedback submitted successfully",
//             feedback = appointment.Feedback
//         });
//     }

// }