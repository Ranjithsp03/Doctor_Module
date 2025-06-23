// using System;
// using System.ComponentModel.DataAnnotations;

// public class Appointment
// {
//     [Key]
//     public int AppointmentID { get; set; }

//     // [Required] 
//     public string DoctorID { get; set; }

//     [Required]
//     public string DoctorName { get; set; }

//     [Required]
//     public string Specialization { get; set; }

//     [Required]
//     public DateTime Date_Time { get; set; }  // used to check timeslot match

//     public string Emergency { get; set; } = "no";

//     public string Status { get; set; } = "Pending";

//     public string Prescription { get; set; } = "none";

//     public string Prescription_ID { get; set; } = Guid.NewGuid().ToString();

//     [Required]
//     public string Patient_ID { get; set; }

//     [Required]
//     public string Issue { get; set; }

//     public int? TimeSlotID { get; set; }  // FK to timeslot

//     public string? Feedback { get; set; }
// }