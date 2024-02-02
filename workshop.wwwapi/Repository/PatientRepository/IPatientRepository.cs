using workshop.wwwapi.Models;

namespace workshop.wwwapi.Repository.PatientRepository
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetAllPatients();
        Task<IEnumerable<Patient>> GetPatientById(int id);
        Task<IEnumerable<Patient>> GetPatientsAppointmentById(int id);
    }
}
