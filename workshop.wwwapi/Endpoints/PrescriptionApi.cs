using Microsoft.AspNetCore.Mvc;
using workshop.wwwapi.DTO;
using workshop.wwwapi.Models;
using workshop.wwwapi.Repository;

namespace workshop.wwwapi.Endpoints
{
    public static class PrescriptionApi
    {

        public static void ConfigurePrescriptionEndpoint(this WebApplication app)
        {
            var prescriptionGroup = app.MapGroup("prescription");

            prescriptionGroup.MapGet("/", GetAllPrescriptions);
            prescriptionGroup.MapPost("/", CreatePrescription);
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public static async Task<IResult> GetAllPrescriptions(IRepository repository)
        {
            var prescriptions = await repository.GetAllPrescriptions();
            if (prescriptions == null)
            {
                return TypedResults.NotFound("No prescription found");

            }

            return TypedResults.Ok(prescriptions);
        }



        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public static async Task<IResult> CreatePrescription(IRepository repository, int patientId, int doctorId, int medicineId, int quantity)
        {
            var newPrescription = await repository.CreatePrescription(patientId, doctorId, DateTime.UtcNow);
            if (newPrescription == null)
            {
                return TypedResults.BadRequest("Prescription data is missing");
            }

            // Create a list to hold PrescriptionMedicineDto objects
            var prescriptionMedicines = new List<PrescriptionMedicineDto>();

            // Create a PrescriptionMedicineDto object for the new medicine
            var medicineDto = new PrescriptionMedicineDto
            {
                MedicineId = medicineId,
                Quantity = quantity,
                Notes = null // You can set notes here if needed
            };

            // Add the new medicine to the list of prescription medicines
            prescriptionMedicines.Add(medicineDto);

            // Create the PrescriptionDto object
            var prescriptionDto = new PrescriptionDto()
            {
                Id = newPrescription.Id,
                PatientId = newPrescription.PatientId,
                DoctorId = newPrescription.DoctorId,
                IssuedAt = newPrescription.IssuedAt,
                Medicines = prescriptionMedicines // Assign the list of medicines to the Medicines property
            };

            return TypedResults.Created($"Prescription with id: {newPrescription.Id} is created", prescriptionDto);
        }


    }
}
