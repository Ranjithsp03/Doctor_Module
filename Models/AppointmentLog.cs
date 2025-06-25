using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Appointments.Model;

[Table("AppointmentLogs")]
public class AppointmentLog
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int LogId { get; set; }

    public string PatientId { get; set; }
    
    public string Reason { get; set; }
    public DateTime TimeSlot { get; set; }
    public string DoctorID { get; set; }

    public DateTimeOffset ApprovedAt { get; set; } = DateTime.UtcNow;
    public Guid AppointmentId { get; internal set; }
    public bool IsApproved { get; set; } = false;
}
