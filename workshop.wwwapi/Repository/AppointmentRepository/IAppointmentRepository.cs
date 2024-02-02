using System.Collections.Generic;
using System.Threading.Tasks;
using workshop.wwwapi.Models;

namespace workshop.wwwapi.Repository.AppointmentRepository
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<Appointment>> GetAllAppointments();
        Task<IEnumerable<Appointment>> GetAppointmentById(int id);
        Task<IEnumerable<Appointment>> GetAppointmentByDoctorId(int id);
        Task<IEnumerable<Appointment>> GetAppointmentByPatientId(int id);
        Task<Appointment> CreateAppointment(Appointment appointment);




    }
}
