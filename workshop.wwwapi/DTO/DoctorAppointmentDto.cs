using workshop.wwwapi.Models;

public class DoctorAppointmentDto
{
    public int Id { get; set; }
    public string DoctorFullName { get; set; }
    public int? AppointmentId { get; set; }
    public DateTimeOffset? Booking { get; set; }
    public int? PatientId { get; set; }
    public string PatientFullName { get; set; }

    public DoctorAppointmentDto(Doctor doctor)
    {
        Id = doctor.Id;
        DoctorFullName = doctor.FullName;

        // Check if the doctor has any appointments
        if (doctor.Appointments != null && doctor.Appointments.Any())
        {
            // Get the first appointment
            var firstAppointment = doctor.Appointments.First();

            // Set the appointment information
            AppointmentId = firstAppointment.Id;
            Booking = firstAppointment.Booking;

            // Check if the appointment has a patient
            if (firstAppointment.Patient != null)
            {
                // Set the patient information
                PatientId = firstAppointment.Patient.Id;
                PatientFullName = firstAppointment.Patient.FullName;
            }
        }
        else
        {
            // Set default values if there are no appointments
            AppointmentId = null;
            Booking = null;
            PatientId = null;
            PatientFullName = null;
        }
    }
}
