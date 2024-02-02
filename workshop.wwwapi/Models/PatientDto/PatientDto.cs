using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace workshop.wwwapi.Models.PatientDto
{
    public class PatientDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }


        [NotMapped]
        private ICollection<AppointmentDto> _appointments;

        // Define a public getter method for the appointments
        [NotMapped]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] // Ignore this property during JSON serialization when null
        [JsonPropertyName("appointments")] // Rename the property back to "appointments"
        public ICollection<AppointmentDto> appointments
        {
            get { return _appointments; }
            set
            {
                // If the value is null or empty, set it to null
                if (value == null || value.Count == 0)
                {
                    _appointments = null;
                }
                else
                {
                    _appointments = value;
                }
            }
        }
    }
}
