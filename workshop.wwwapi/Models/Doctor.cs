using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

using System.Text.Json.Serialization;

namespace workshop.wwwapi.Models
{
    //TODO: decorate class/columns accordingly    
    public class Doctor
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("full_name")]
        public string FullName { get; set; }

        public ICollection<Appointment> Appointments { get; set; }

        //// Private field to hold the appointments
        //[JsonIgnore]
        //private ICollection<Appointment> _appointments;

        //// Public property to expose the appointments
        //[NotMapped]
        //[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        //[JsonPropertyName("appointments")]
        //public ICollection<Appointment> Appointments
        //{
        //    get { return _appointments; }
        //    set
        //    {
        //        // If the value is null or empty, set it to null
        //        if (value == null || !value.Any())
        //        {
        //            _appointments = null;
        //        }
        //        else
        //        {
        //            _appointments = value;
        //        }
        //    }
        //}
    }
}

