using Microsoft.AspNetCore.Mvc;
using workshop.wwwapi.Repository.AppointmentRepository;
using workshop.wwwapi.Models;

using static System.Reflection.Metadata.BlobBuilder;
using workshop.wwwapi.Repository;
using workshop.wwwapi.Models.PatientDto;
using workshop.wwwapi.Repository.PatientRepository;
using workshop.wwwapi.Repository.DoctorRepository;

namespace workshop.wwwapi.Endpoints
{
    public static class PatientApi
    {

        public static void ConfigurePatientEndpoint(this WebApplication app)
        {
            var patientGroup = app.MapGroup("patient");

            patientGroup.MapGet("/", GetPatients);
            patientGroup.MapGet("/{id}", GetPatientById);
            patientGroup.MapGet("/appointments{id}", GetPatientAppointmentsById);
            //patientGroup.MapGet("/appointment{id}", GetDoctorsAppointmentsById);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetPatients([FromServices] IPatientRepository repository)
        {
            var patients = await repository.GetAllPatients();
            var results = patients.Select(p => new DoctorDto
            {
                Id = p.Id,
                FullName = p.FullName,
                //appointments = null

            }).ToList();
            return TypedResults.Ok(results);
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetPatientById([FromServices] IPatientRepository repository, int id)
        {
            var patient = await repository.GetPatientById(id);
            var results = patient.Select(p => new PatientDto
            {
                Id = p.Id,
                FullName = p.FullName,
                //appointments = null

            }).ToList();
            return TypedResults.Ok(results);
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetPatientAppointmentsById([FromServices] IPatientRepository repository, int id)
        {
            var patient = (await repository.GetPatientsAppointmentById(id)).FirstOrDefault();
            if (patient == null)
            {
                return TypedResults.NotFound("Patient not found");
            }

            var patientDto = new PatientDto()
            {
                Id = patient.Id,
                FullName = patient.FullName,
                appointments = patient.Appointments != null ? patient.Appointments.Select(a => new AppointmentDto()
                {
                    Id = a.Id,
                    Booking = a.Booking,
                    doctor = a.Doctor != null ? new DoctorDto()
                    {
                        Id = a.Doctor.Id,
                        FullName = a.Doctor.FullName
                    } : null
                }).ToList() : null
            };

            return TypedResults.Ok(patientDto);
        }
    }
}
