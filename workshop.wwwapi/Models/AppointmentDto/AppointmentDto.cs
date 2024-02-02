using System.ComponentModel.DataAnnotations.Schema;

namespace workshop.wwwapi.Models.AppointmentDto
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public DateTimeOffset Booking { get; set; }

        public PatientDto patient { get; set; }
        public DoctorDto doctor { get; set; }
    }
}
