using workshop.wwwapi.Models;
namespace workshop.wwwapi.DTO
{
    public class PatientAppointmentDto
    {
        public int Id { get; set; }
        public string PatientFullName { get; set; }
        public int AppointmentId { get; set; }
        public DateTimeOffset Booking { get; set; }
        public int DoctorId { get; set; }
        public string DoctorFullName { get; set; }

        public PatientAppointmentDto(Appointment appointment)
        {
            Id = appointment.Patient.Id;
            PatientFullName = appointment.Patient.FullName;
            AppointmentId = appointment.Id;
            Booking = appointment.Booking;
            DoctorId = appointment.Doctor.Id;
            DoctorFullName = appointment.Doctor.FullName;
        }

    }
}
