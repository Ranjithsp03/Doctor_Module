using Doctor_Module.Models.Doctor;
using Doctor_Module.Models.Prescription;
using Doctor_Module.Timeslots;
using Microsoft.EntityFrameworkCore;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }
    public DbSet<Doctor> Doctors { get; set; }
    // public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Timeslot> Timeslots { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }

    //dbcontext keywords

}