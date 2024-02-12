namespace workshop.wwwapi.DTO
{
    public class PrescriptionDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTimeOffset IssuedAt { get; set; }
        public List<PrescriptionMedicineDto> Medicines { get; set; }
    }
}
