using Microsoft.AspNetCore.Mvc;
using workshop.wwwapi.DTO;
using workshop.wwwapi.Repository;
using workshop.wwwapi.Models;

namespace workshop.wwwapi.Endpoints
{
    public static class AppointmentApi
    {
        public static void ConfigureAppointmentEndpoint(this WebApplication app)
        {
            var appointmentGroup = app.MapGroup("appointment");

            appointmentGroup.MapPost("/", CreateAppointment);
            appointmentGroup.MapGet("/", GetAllAppointments);
            appointmentGroup.MapGet("/{id}", GetAppointmentsById);
            appointmentGroup.MapGet("/doctor{id}", GetAppointmentsByDoctorId);
            appointmentGroup.MapGet("/patient{id}", GetAppointmentsByPatientId);

            // Add other endpoints as needed
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> CreateAppointment(IRepository repository, int PatientId, int DoctorId)
        {
            Appointment appointment = await repository.CreateAppointment(DateTime.UtcNow, PatientId, DoctorId);

            if (appointment == null)
            {
                return Results.BadRequest("Failed to create customer.");
            }


            repository.SaveChanges();


            AppointmentDto appointmentDto = new AppointmentDto(appointment);

            return TypedResults.Created($"/appointment {appointmentDto.Id}", appointmentDto);
        }


        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetAllAppointments(IRepository repository)
        {
            var appointments = await repository.GetAllAppointments();
            if (appointments == null)
            {
                return TypedResults.NotFound(" No appointments found");
            }

            var results = appointments.OrderBy(a => a.Id).Select(a => new AppointmentDto(a)).ToList();

            return TypedResults.Ok(results);
        }



        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetAppointmentsById(IRepository repository, int id)
        {
            var appointment = (await repository.GetAppointmentById(id)).FirstOrDefault();

            if (appointment == null)
            {
                return TypedResults.NotFound($"Appointment with the given id {appointment.Id} found");
            }

            var appointmentDto = new AppointmentDto(appointment);

            return TypedResults.Ok(appointmentDto);
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetAppointmentsByDoctorId(IRepository repository, int id)
        {
            var appointment = (await repository.GetAppointmentByDoctorId(id)).FirstOrDefault();

            if (appointment == null)
            {
                return TypedResults.NotFound($"Appointment with the given doctor id: {appointment.Id} not found");
            }

            var appointmentDto = new AppointmentDto(appointment);

            return TypedResults.Ok(appointmentDto);
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetAppointmentsByPatientId(IRepository repository, int id)
        {
            var appointments = (await repository.GetAppointmentByPatientId(id)).FirstOrDefault();

            if (appointments == null)
            {
                return TypedResults.NotFound("Appointment not found");
            }

            var appointmentDto = new AppointmentDto(appointments);

            return TypedResults.Ok(appointmentDto);
        }
    }
}
