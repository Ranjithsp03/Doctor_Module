using Doctor_Module.Models.Doctor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
public class DoctorController : ControllerBase
{
    public readonly AppDbContext _context;
    public DoctorController(AppDbContext context)
    {
        _context = context;
    }
    [HttpPost("register")]

    public async Task<IActionResult> Register(Doctor doctor)
    {
        _context.Doctors.Add(doctor);
        await
        _context.SaveChangesAsync();
        return Ok(doctor);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDoctor(string id)
    {
        var doc = await _context.Doctors.FindAsync(id);
        return doc == null ? NotFound() : Ok(doc);
    }
    // getall
    [HttpGet("all")]
    public async Task<IActionResult> GetAllDoctors()
    {
        var doctors = await _context.Doctors.Select(d => new { d.DoctorID, d.Name, d.Specialization }).ToListAsync();
        return Ok(doctors);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDoctor(string id, Doctor updatedDoctor)
    {
        var existingDoctor = await _context.Doctors.FindAsync(id);
        if (existingDoctor == null) return NotFound("Doctor Not Found");
        else
        {
            existingDoctor.Name = updatedDoctor.Name;
            existingDoctor.DoctorID = updatedDoctor.DoctorID;
            existingDoctor.Specialization = updatedDoctor.Specialization;
        }
        await _context.SaveChangesAsync();
        return Ok(existingDoctor);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDoctor(string id)
    {
        var To_DeleteDoctor = await _context.Doctors.FindAsync(id);
        if (To_DeleteDoctor == null) return NotFound("Doctor Not Found");
        else
        {
            _context.Remove(To_DeleteDoctor);

            await _context.SaveChangesAsync();

            return Ok("Doctor Deleted Successfully");

        }
    }

}