using workshop.wwwapi.Models;

namespace workshop.wwwapi.DTO
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public DateTimeOffset Booking { get; set; }
        public int PatientId { get; set; }
        public string PatientFullName { get; set; }
        public int DoctorId { get; set; }
        public string DoctorFullName { get; set; }

        public AppointmentDto(Appointment appointment)
        {
            Id = appointment.Id;
            Booking = appointment.Booking;

            // Populate patient information if available
            if (appointment.Patient != null)
            {
                PatientId = appointment.PatientId;
                PatientFullName = appointment.Patient.FullName;
            }

            // Populate doctor information if available
            if (appointment.Doctor != null)
            {
                DoctorId = appointment.DoctorId;
                DoctorFullName = appointment.Doctor.FullName;
            }
        }
    }

}
