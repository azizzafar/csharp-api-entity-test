using Microsoft.AspNetCore.Mvc;
using workshop.wwwapi.Repository;
using workshop.wwwapi.Models;

using workshop.wwwapi.DTO;

namespace workshop.wwwapi.Endpoints
{
    public static class PatientApi
    {

        public static void ConfigurePatientEndpoint(this WebApplication app)
        {
            var patientGroup = app.MapGroup("patient");

            patientGroup.MapGet("/", GetAllPatients);
            patientGroup.MapPost("/", CreatePatient);
            patientGroup.MapGet("/appointments", GetPatientsAppointments);

        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public static async Task<IResult> GetAllPatients([FromServices] IRepository repository)
        {
            var patients = await repository.GetAllPatients();
            if (patients == null)
            {
                return TypedResults.NotFound("No patients found");
            }
            var results = patients.OrderBy(a => a.Id).Select(a => new PatientDto(a)).ToList();


            return TypedResults.Ok(results);
        }



        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> CreatePatient(IRepository repository, string FullName)
        {
            var newPatient = await repository.CreatePatient(FullName);
            if (newPatient == null)
            {
                return TypedResults.BadRequest("Failed to create a patient, check if uniquness of Id ");
            }

            PatientDto patientDto = new PatientDto(newPatient);

            return TypedResults.Created($"Patient with id: {newPatient.Id} is created", patientDto);

        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetPatientsAppointments(IRepository repository)
        {
            var patients = await repository.GetPatientsAppointments();

            if (patients == null)
            {
                return Results.NotFound("No Appointments for the patients found");
            }

            var result = patients.OrderBy(a => a.Id).Select(a => new PatientDto(a)).ToList();

            return TypedResults.Ok(result);
        }



    }
}
