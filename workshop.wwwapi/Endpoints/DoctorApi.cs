using Microsoft.AspNetCore.Mvc;
using workshop.wwwapi.Repository;
using workshop.wwwapi.Models;
using workshop.wwwapi.DTO;

namespace workshop.wwwapi.Endpoints
{
    public static class DoctorApi
    {
        //TODO:  add additional endpoints in here according to the requirements in the README.md 
        public static void ConfigureDoctorEndpoint(this WebApplication app)
        {
            var doctorGroup = app.MapGroup("doctor");

            doctorGroup.MapGet("/", GetAllDoctors);
            doctorGroup.MapPost("/", CreateDoctor);
            doctorGroup.MapGet("/appointments", GetDoctorsAppointments);
            // doctorGroup.MapGet("/appointment{id}", GetDoctorsAppointmentsById);
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetAllDoctors(IRepository repository)
        {
            var doctors = await repository.GetAllDoctors();

            if (doctors == null)
            {
                return TypedResults.NotFound(" No doctors found");
            }


            var results = doctors.OrderBy(a => a.Id).Select(a => new DoctorDto(a)).ToList();

            return TypedResults.Ok(results);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> CreateDoctor(IRepository repository, string FullName)
        {
            Doctor doctor = await repository.CreateDoctor(FullName);

            if (doctor == null)
            {
                return TypedResults.BadRequest("Doctor with same name/id allready exist");
            }


            repository.SaveChanges();


            DoctorDto doctorDto = new DoctorDto(doctor);

            return TypedResults.Created($"/appointment {doctorDto.Id}", doctorDto);
        }



        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetDoctorsAppointments(IRepository repository)
        {
            var doctors = await repository.GetDoctorsAppointments();
            if (doctors == null)
            {
                return TypedResults.NotFound("No appointments for doctos found");
            }

            var results = doctors.OrderBy(a => a.Id).Select(a => new DoctorAppointmentDto(a)).ToList();

            return TypedResults.Ok(results);
        }


    }
}
