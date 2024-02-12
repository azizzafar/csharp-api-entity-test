using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using System.ComponentModel.DataAnnotations.Schema;
using workshop.wwwapi.Data;
using workshop.wwwapi.DTO;
using workshop.wwwapi.Endpoints;
using workshop.wwwapi.Models;
namespace workshop.wwwapi.Repository
{
    public class Repository : IRepository
    {

        private DatabaseContext _db;
        public Repository(DatabaseContext db)
        {
            _db = db;
        }


        public async Task<Appointment> CreateAppointment(DateTimeOffset booking, int patientId, int doctorId)
        {
            // Get the highest existing appointment ID
            int maxAppointmentId = await _db.Appointments.MaxAsync(a => (int?)a.Id) ?? 0;

            // Increment the highest existing appointment ID to generate a new unique ID
            int newAppointmentId = maxAppointmentId + 1;

            var appointment = new Appointment
            {
                Id = newAppointmentId,
                Booking = booking,
                PatientId = patientId,
                DoctorId = doctorId
            };

            await _db.Appointments.AddAsync(appointment);
            await _db.SaveChangesAsync();

            // Include related entities (Patient and Doctor) when retrieving the newly created appointment
            return await _db.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .SingleOrDefaultAsync(a => a.Id == newAppointmentId);
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






        public void SaveChanges()
        {
            _db.SaveChanges();
        }


        //******************* Doctor ****************************
        public async Task<Doctor> CreateDoctor(string FullName)
        {
            // Get the highest existing appointment ID
            int maxDoctorId = await _db.Doctors.MaxAsync(a => (int?)a.Id) ?? 0;

            // Increment the highest existing appointment ID to generate a new unique ID
            int newDoctorId = maxDoctorId + 1;

            var doctor = new Doctor
            {
                Id = newDoctorId,
                FullName = FullName,
            };

            await _db.Doctors.AddAsync(doctor);
            await _db.SaveChangesAsync();
            return doctor;
            // Include related entities (Patient and Doctor) when retrieving the newly created appointment
            //return await _db.Doctors
            //  .Include(a => a.Appointments)
            //.SingleOrDefaultAsync(a => a.Id == newDoctorId);

        }

        public async Task<IEnumerable<Doctor>> GetDoctorsAppointments()
        {
            return await _db.Doctors
                .Include(d => d.Appointments)              // Include appointments for each doctor
                    .ThenInclude(a => a.Patient)           // Include patients for each appointment
                .ToListAsync();
        }


        public async Task<IEnumerable<Doctor>> GetAllDoctors()
        {
            return await _db.Doctors
                .ToListAsync();
        }


        // ************************** Patient ***********************

        public async Task<Patient> CreatePatient(string FullName)
        {
            int maxPatientId = await _db.Patients.MaxAsync(a => (int?)a.Id) ?? 0;

            // Increment the highest existing appointment ID to generate a new unique ID
            int newPatientId = maxPatientId + 1;


            var newPatient = new Patient
            {
                Id = newPatientId,
                FullName = FullName,
            };

            if (newPatient == null)
            {
                return null;
            }


            await _db.Patients.AddAsync(newPatient);
            await _db.SaveChangesAsync();
            return newPatient;

        }

        public async Task<IEnumerable<Patient>> GetAllPatients()
        {
            return await _db.Patients.ToListAsync();
        }

        public async Task<IEnumerable<Patient>> GetPatientsAppointments()
        {
            return await _db.Patients
                .Include(d => d.Appointments)
                .ThenInclude(a => a.Patient)
                .ToListAsync();
        }

        //************************ Prescription ***************************
        public async Task<IEnumerable<PrescriptionDto>> GetAllPrescriptions()
        {
            return await _db.Prescriptions
                .Select(p => new PrescriptionDto
                {
                    Id = p.Id,
                    PatientId = p.PatientId,
                    DoctorId = p.DoctorId,
                    IssuedAt = p.IssuedAt,
                    Medicines = p.PrescriptionMedicines.Select(pm => new PrescriptionMedicineDto
                    {
                        //PrescriptionId = pm.PrescriptionId,
                        MedicineId = pm.MedicineId,
                        Quantity = pm.Quantity,
                        Notes = pm.Notes
                    }).ToList()
                })
                .ToListAsync();
        }


        public async Task<Prescription> CreatePrescription(int patientId, int doctorId, DateTimeOffset issuedAt)
        {

            int maxPrescription = await _db.Prescriptions.MaxAsync(a => (int?)a.Id) ?? 0;

            // Increment the highest existing appointment ID to generate a new unique ID
            int newPrescriptionId = maxPrescription + 1;

            Prescription prescription = new Prescription
            {
                Id = newPrescriptionId,
                PatientId = patientId,
                DoctorId = doctorId,
                IssuedAt = issuedAt,

            };

            if (prescription == null)
            {
                return null;
            }

            await _db.Prescriptions.AddAsync(prescription);
            await _db.SaveChangesAsync();
            return prescription;
        }
    }
}

