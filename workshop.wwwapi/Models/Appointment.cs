using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace workshop.wwwapi.Models
{
    //TODO: decorate class/columns accordingly
    //[Keyless]
    public class Appointment
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("date")]
        public DateTimeOffset Booking { get; set; }

        [Column("doctor_id")]
        public int DoctorId { get; set; }

        [Column("patient_id")]
        public int PatientId { get; set; }

        //[JsonIgnore]
        [ForeignKey("DoctorId")]
        public Doctor Doctor { get; set; }


        //[JsonIgnore]
        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }

    }
}
