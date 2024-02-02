using Microsoft.EntityFrameworkCore;
using workshop.wwwapi.Data;
using workshop.wwwapi.Models;
using workshop.wwwapi.Models.DoctorDto;
using workshop.wwwapi.Repository;

namespace workshop.wwwapi.Repository.DoctorRepository
{
    public class DoctorRepository : IDoctorRepository
    {

        private DatabaseContext _db;
        public DoctorRepository(DatabaseContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Doctor>> GetAllDoctors()
        {
            return await _db.Doctors
                .ToListAsync();
        }

        public async Task<IEnumerable<Doctor>> GetDoctorsById(int id)
        {
            return await _db.Doctors.Where(d => d.Id ==  id)
                .ToListAsync();

        }

        public async Task<IEnumerable<Doctor>> GetAllDoctorsAppointments()
        {
            return await _db.Doctors
                .Include(d => d.Patient)
                .Include(d => d.Appointments)
                .ToListAsync();


        }


        public async Task<IEnumerable<Doctor>> GetDoctorsAppointmentById(int id)
        {
            return await _db.Doctors
                .Where(d => d.Id == id)
                .Include(d => d.Patient)
                .Include(d => d.Appointments)
                .ToListAsync();


        }
    }
}
