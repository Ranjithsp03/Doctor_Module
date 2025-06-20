using System;
using Doctor_Module.Models.Doctor;
public class Appointment
{
    public string AppointmentID { get; set; }
    public string DoctorID { get; set; }
    public string Time { get; set; }
    public string Emergency { get; set; }

    public string Prescription { get; set; }
    public string Prescription_ID { get; set; }

    public string Patient_ID { get; set; }
    public string Issue { get; set; }
    // to write
    public Doctor doctor { get; set; }
}