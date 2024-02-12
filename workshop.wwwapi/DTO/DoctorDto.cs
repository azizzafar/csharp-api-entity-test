using workshop.wwwapi.Models;
namespace workshop.wwwapi.DTO
{
    public class DoctorDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public DoctorDto(Doctor doctor)
        {
            Id = doctor.Id;

            FullName = doctor.FullName;
        }
    }
}
