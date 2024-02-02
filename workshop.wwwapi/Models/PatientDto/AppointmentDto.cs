using System.ComponentModel.DataAnnotations.Schema;

namespace workshop.wwwapi.Models.PatientDto
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public DateTimeOffset Booking { get; set; }
        public DoctorDto doctor { get; set; }

    }
}
