using System.ComponentModel.DataAnnotations.Schema;

namespace workshop.wwwapi.Models
{
    public class Prescription
    {
        [Column("id")]
        public int Id { get; set; }

        // Change the foreign key property to match the composite key of Appointment
        [Column("patient_id")]
        public int PatientId { get; set; }

        [Column("doctor_id")]
        public int DoctorId { get; set; }

        [Column("issue_date")]
        public DateTimeOffset IssuedAt { get; set; }

        // Define the navigation properties for Patient and Doctor
        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }

        [ForeignKey("DoctorId")]
        public Doctor Doctor { get; set; }

        // Define the navigation property for PrescriptionMedicines
        public ICollection<PrescriptionMedicine> PrescriptionMedicines { get; set; }
    }

}
