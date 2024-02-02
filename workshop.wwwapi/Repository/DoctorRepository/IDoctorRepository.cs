using System.Threading.Tasks;
using workshop.wwwapi.Models;

namespace workshop.wwwapi.Repository.DoctorRepository
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> GetAllDoctors();
        Task<IEnumerable<Doctor>> GetAllDoctorsAppointments();
        Task<IEnumerable<Doctor>> GetDoctorsById(int id);
        Task<IEnumerable<Doctor>> GetDoctorsAppointmentById(int id);

    }
}
