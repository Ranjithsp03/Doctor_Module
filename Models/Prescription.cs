using System.ComponentModel.DataAnnotations;

namespace Doctor_Module.Models.Prescription
{
    public class Prescription
    {
        [Key]
        public string PrescriptionID { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string DoctorID { get; set; }

        [Required]
        public string PatientID { get; set; }
        public string PatientName { get; set; }
        [Required]
        public string Dosage { get; set; }

        [Required]
        public string Instructions { get; set; }
    }
}
