using System.ComponentModel.DataAnnotations.Schema;

namespace workshop.wwwapi.Models.AppointmentDto
{
    public class PatientDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        //public int DoctorId { get; set; }
        // public DoctorDto doctorDto { get; set; }
    }
}
