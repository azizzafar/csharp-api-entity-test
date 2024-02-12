using System.ComponentModel.DataAnnotations.Schema;

namespace workshop.wwwapi.Models
{
    public class Medicine
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }
        [Column("quantity")]
        public int Quantity { get; set; }  // Add quantity property

        [Column("notes")]
        public string Notes { get; set; }   // Add notes property

        // Define the navigation property for PrescriptionMedicines
        public ICollection<PrescriptionMedicine> PrescriptionMedicines { get; set; }


    }
}
