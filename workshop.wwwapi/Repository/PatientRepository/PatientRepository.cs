using Microsoft.EntityFrameworkCore;
using workshop.wwwapi.Data;
using workshop.wwwapi.Models;

namespace workshop.wwwapi.Repository.PatientRepository
{
    public class PatientRepository : IPatientRepository
    {

        private DatabaseContext _db;
        public PatientRepository(DatabaseContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Patient>> GetAllPatients()
        {
            return await _db.Patients.ToListAsync();    
        }

        public async Task<IEnumerable<Patient>> GetPatientById(int id)
        {
            return await _db.Patients.Where(p => p.Id == id).ToListAsync();
        }

        public async Task<IEnumerable<Patient>> GetPatientsAppointmentById(int id)
        {
            return await _db.Patients.
                Where(p => p.Id == id)
                .Include(p => p.Doctor)
                .Include(p => p.Appointments)
                .ToListAsync();
        }

    }
}
