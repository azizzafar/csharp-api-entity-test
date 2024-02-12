using System.Collections.Generic;
using System.Threading.Tasks;
using workshop.wwwapi.DTO;
using workshop.wwwapi.Models;

namespace workshop.wwwapi.Repository
{
    public enum PreloadPolicy
    {
        DoNotPreloadRelations,
        PreloadRelations
    }
    public interface IRepository
    {
        Task<Appointment> CreateAppointment(DateTimeOffset booking, int patientId, int doctorId);
        Task<IEnumerable<Appointment>> GetAllAppointments();
        Task<IEnumerable<Appointment>> GetAppointmentById(int id);
        Task<IEnumerable<Appointment>> GetAppointmentByDoctorId(int id);
        Task<IEnumerable<Appointment>> GetAppointmentByPatientId(int id);

        //*************************** Doctor ***************************
        Task<Doctor> CreateDoctor(string FullName);
        Task<IEnumerable<Doctor>> GetDoctorsAppointments();
        Task<IEnumerable<Doctor>> GetAllDoctors();


        // ************************* Patient ***************************
        Task<Patient> CreatePatient(string FullName);
        Task<IEnumerable<Patient>> GetAllPatients();

        Task<IEnumerable<Patient>> GetPatientsAppointments();


        //*************************** Prescription *********************
        Task<IEnumerable<PrescriptionDto>> GetAllPrescriptions();


        Task<Prescription> CreatePrescription(int patientId, int doctorId, DateTimeOffset issuedAt);

        void SaveChanges();

    }
}
