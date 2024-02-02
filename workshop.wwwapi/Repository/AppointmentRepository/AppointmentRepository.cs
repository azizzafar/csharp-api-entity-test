using Microsoft.EntityFrameworkCore;
using workshop.wwwapi.Data;
using workshop.wwwapi.Models;
using workshop.wwwapi.Models.AppointmentDto;
using workshop.wwwapi.Repository;

namespace workshop.wwwapi.Repository.AppointmentRepository
{
    public class AppointmentRepository : IAppointmentRepository
    {

        private DatabaseContext _db;
        public AppointmentRepository(DatabaseContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Appointment>> GetAllAppointments()
        {
            return await _db.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .ToListAsync();
        
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentById(int id)
        {
            return await _db.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Where(a => a.Id == id).ToListAsync();
        }


        public async Task<IEnumerable<Appointment>> GetAppointmentByDoctorId(int id)
        {
            return await _db.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Where(a => a.DoctorId == id).ToListAsync();

        }

        public async Task<IEnumerable<Appointment>> GetAppointmentByPatientId(int id)
        {
            return await _db.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Where(a => a.PatientId == id).ToListAsync();

        }

        public async Task<Appointment> CreateAppointment(Appointment appointment)
        {
            _db.Appointments.Add(appointment);
            await _db.SaveChangesAsync();
            return appointment;
        }


    }
}

