using Microsoft.AspNetCore.Mvc;
using workshop.wwwapi.Repository.AppointmentRepository;
using workshop.wwwapi.Models;

using static System.Reflection.Metadata.BlobBuilder;
using workshop.wwwapi.Repository;
using workshop.wwwapi.Models.AppointmentDto;

namespace workshop.wwwapi.Endpoints
{
    public static class AppointmentApi
    {
        //TODO:  add additional endpoints in here according to the requirements in the README.md 
        public static void ConfigureAppointmentEndpoint(this WebApplication app)
        {
            var appointmentGroup = app.MapGroup("appointment");

            appointmentGroup.MapGet("/", GetAllAppointments);
            appointmentGroup.MapGet("/{id}", GetAppointmentsById);
            appointmentGroup.MapGet("/doctor{id}", GetAppointmentsByDoctorId);
            appointmentGroup.MapGet("/patient{id}", GetAppointmentsByPatientId);
            appointmentGroup.MapPost("/create{id}", CreateAppointment);


            // Add other endpoints as needed
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetAllAppointments(IAppointmentRepository repository)
        {
            var appointments = await repository.GetAllAppointments();
            var results = appointments.OrderBy(a => a.Id).Select(a => new AppointmentDto
            {
                Id = a.Id,
                Booking = a.Booking,
                patient = a.Patient != null ? new PatientDto()
                {
                    Id = a.Patient.Id,
                    FullName = a.Patient.FullName
                } : null,
                doctor = a.Doctor != null ? new DoctorDto()
                {
                    Id = a.Doctor.Id,
                    FullName = a.Doctor.FullName
                } : null
            }).ToList();

            return TypedResults.Ok(results);
        }



        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetAppointmentsById(IAppointmentRepository repository, int id)
        {
            var appointments = (await repository.GetAppointmentById(id)).FirstOrDefault();

            if (appointments == null)
            {
                return TypedResults.NotFound("Appointment not found");
            }

            var appointment = new AppointmentDto()
            {
                Id = appointments.Id,
                Booking = appointments.Booking,
                patient = appointments.Patient != null ? new PatientDto()
                {
                    Id = appointments.Patient.Id,
                    FullName = appointments.Patient.FullName
                } : null,
                doctor = appointments.Doctor != null ? new DoctorDto()
                {
                    Id = appointments.Doctor.Id,
                    FullName = appointments.Doctor.FullName
                } : null
            };

            return TypedResults.Ok(appointment);
        }

        //**************************************************************************
        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetAppointmentsByDoctorId(IAppointmentRepository repository, int id)
        {
            var appointments = (await repository.GetAppointmentByDoctorId(id)).FirstOrDefault();

            if (appointments == null)
            {
                return TypedResults.NotFound("Appointment not found");
            }

            var appointment = new AppointmentDto()
            {
                Id = appointments.Id,
                Booking = appointments.Booking,
                patient = appointments.Patient != null ? new PatientDto()
                {
                    Id = appointments.Patient.Id,
                    FullName = appointments.Patient.FullName
                } : null,
                doctor = appointments.Doctor != null ? new DoctorDto()
                {
                    Id = appointments.Doctor.Id,
                    FullName = appointments.Doctor.FullName
                } : null
            };

            return TypedResults.Ok(appointment);



        }

        //*************************************************************
        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetAppointmentsByPatientId(IAppointmentRepository repository, int id)
        {
            var appointments = (await repository.GetAppointmentByPatientId(id)).FirstOrDefault();

            if (appointments == null)
            {
                return TypedResults.NotFound("Appointment not found");
            }

            var appointmentDto = new AppointmentDto()
            {
                Id = appointments.Id,
                Booking = appointments.Booking,
                patient = appointments.Patient != null ? new PatientDto()
                {
                    Id = appointments.Patient.Id,
                    FullName = appointments.Patient.FullName
                } : null,
                doctor = appointments.Doctor != null ? new DoctorDto()
                {
                    Id = appointments.Doctor.Id,
                    FullName = appointments.Doctor.FullName
                } : null
            };

            return TypedResults.Ok(appointmentDto);



        }



        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> CreateAppointment(IAppointmentRepository repository, Appointment appointment)
        {
            // Ensure that the appointment object is valid
            if (appointment == null || appointment.DoctorId == 0 || appointment.PatientId == 0)
            {
                return TypedResults.BadRequest("Invalid appointment data");
            }

            // Create a new appointment
            var newAppointment = new Appointment
            {
                Booking = appointment.Booking,
                DoctorId = appointment.DoctorId,
                PatientId = appointment.PatientId
            };

            // Add the new appointment to the database
            var createdAppointment = await repository.CreateAppointment(newAppointment);

            // Check if the appointment was successfully created
            if (createdAppointment == null)
            {
                return TypedResults.BadRequest("Failed to create appointment");
            }

            // Return the created appointment
            return TypedResults.Created("", createdAppointment);
        }



    }
}


