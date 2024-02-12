using System.ComponentModel.DataAnnotations;

namespace workshop.wwwapi.DTO
{

    namespace workshop.wwwapi.DTO
    {
        public record AppointmentPayload
        {
            public DateTimeOffset Booking { get; init; }

            [Required(ErrorMessage = "PatientId required!")]
            public int PatientId { get; init; }

            [Required(ErrorMessage = "DoctorId required!")]
            public int DoctorId { get; init; }

            // Add a parameterless constructor for deserialization
            public AppointmentPayload() { }

            // Remove the existing constructor
            public AppointmentPayload(int patientId, int doctorId)
            {
                PatientId = patientId;
                DoctorId = doctorId;
            }
        }
    };


    public record DoctorPayload
    {
        public string FullName { get; init; }

        public DoctorPayload(string fullName)
        {
            FullName = fullName;

        }



    };


    public record PatientPayload
    {
        public string FullName { get; init; }

        public PatientPayload(string fullName)
        {

            FullName = fullName;

        }



    };
}
