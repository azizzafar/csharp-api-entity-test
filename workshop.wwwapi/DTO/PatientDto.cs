using workshop.wwwapi.Models;
namespace workshop.wwwapi.DTO
{
    public class PatientDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public PatientDto(Patient patient)
        {
            Id = patient.Id;
            FullName = patient.FullName;
        }
    }
}
