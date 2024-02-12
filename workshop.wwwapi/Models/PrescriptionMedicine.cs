namespace workshop.wwwapi.Models
{
    public class PrescriptionMedicine
    {
        // Other properties...

        public int Quantity { get; set; }
        public string Notes { get; set; }

        // Foreign key properties
        public int PrescriptionId { get; set; }
        public int MedicineId { get; set; }

        // Navigation properties
        public Prescription Prescription { get; set; }
        public Medicine Medicine { get; set; }
    }

}
