using Microsoft.AspNetCore.Mvc;
using workshop.wwwapi.Repository.DoctorRepository;
using workshop.wwwapi.Models;

using static System.Reflection.Metadata.BlobBuilder;
using workshop.wwwapi.Repository;
using workshop.wwwapi.Models.DoctorDto;
using System.Numerics;

namespace workshop.wwwapi.Endpoints
{
    public static class DoctorApi
    {
        //TODO:  add additional endpoints in here according to the requirements in the README.md 
        public static void ConfigureDoctorEndpoint(this WebApplication app)
        {
            var doctorGroup = app.MapGroup("doctor");

            doctorGroup.MapGet("/", GetDoctors);
            doctorGroup.MapGet("/{id}", GetDoctorById);
            doctorGroup.MapGet("/doctors appointments", GetDoctorsAppointments);
            doctorGroup.MapGet("/appointment{id}", GetDoctorsAppointmentsById);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetDoctors([FromServices] IDoctorRepository repository)
        {
            var doctors = await repository.GetAllDoctors();
            var results = doctors.Select(d => new DoctorDto
            {
                Id = d.Id,
                FullName = d.FullName,
                //appointments = null

            }).ToList();
            return TypedResults.Ok(results);
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetDoctorById([FromServices] IDoctorRepository repository, int id)
        {
            var doctors = await repository.GetDoctorsById(id);
            var results = doctors.Select(d => new DoctorDto
            {
                Id = d.Id,
                FullName = d.FullName,
                //appointments = null

            }).ToList();
            return TypedResults.Ok(results);
        }





        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetDoctorsAppointments([FromServices] IDoctorRepository repository)
        {
            var doctors = await repository.GetAllDoctorsAppointments();
            List<DoctorDto> results = new List<DoctorDto>();

            foreach (var d in doctors)
            {
                var doctorDto = new DoctorDto()
                {
                    Id = d.Id,
                    FullName = d.FullName,
                    appointments = d.Appointments != null ? d.Appointments.Select(a => new AppointmentDto()
                    {
                        Id = a.Id,
                        Booking = a.Booking,
                        patient = d.Patient != null ? new PatientDto()
                        {
                            Id = d.Patient.Id,
                            FullName = d.Patient.FullName
                            } : null,

                    }).ToList() : null
                };

                results.Add(doctorDto);
            }

            return TypedResults.Ok(results);
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetDoctorsAppointmentsById([FromServices] IDoctorRepository repository, int id)
        {
            var doctor = (await repository.GetDoctorsAppointmentById(id)).FirstOrDefault();
            if (doctor == null)
            {
                return TypedResults.NotFound("Doctor not found");
            }

            var doctorDto = new DoctorDto()
            {
                Id = doctor.Id,
                FullName = doctor.FullName,
                appointments = doctor.Appointments != null ? doctor.Appointments.Select(a => new AppointmentDto()
                {
                    Id = a.Id,
                    Booking = a.Booking,
                    patient = a.Patient != null ? new PatientDto()
                    {
                        Id = a.Patient.Id,
                        FullName = a.Patient.FullName
                    } : null
                }).ToList() : null
            };

            return TypedResults.Ok(doctorDto);
        }



    }
}
