namespace workshop.wwwapi.Models.DoctorDto
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public DateTimeOffset Booking { get; set; }
        public PatientDto patient { get; set; }
    }
}
