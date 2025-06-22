using System;
using System.ComponentModel.DataAnnotations;
using Doctor_Module.Models.Doctor;

namespace Doctor_Module.Timeslots
{
    public class Timeslot
    {
        public int TimeSlotID { get; set; }

        [Required]
        public string DoctorID { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }


        [Required]
        [DataType(DataType.Time)]
        public TimeSpan StartTime { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan EndTime { get; set; }

        public bool IsAvailable { get; set; } = true;

        [Range(1, 100, ErrorMessage = "Count must be between 1 and 100.")]
        public int Count { get; set; } = 1; // ✅ Number of patients allowed for this slot

        public Doctor Doctor { get; set; }
        public TimeSpan GetTimeDifference() => EndTime - StartTime;
    }
}
