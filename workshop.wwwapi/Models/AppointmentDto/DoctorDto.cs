using System.ComponentModel.DataAnnotations.Schema;

namespace workshop.wwwapi.Models.AppointmentDto
{
    public class DoctorDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }

    }
}
