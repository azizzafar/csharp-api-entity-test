using System;
using System.Collections.Generic;
using workshop.wwwapi.Models;

public class Seeder
{
    private List<string> _firstnames = new List<string>()
    {
        "Audrey", "Donald", "Elvis", "Barack", "Oprah",
        "Jimi", "Mick", "Kate", "Charles", "Kate"
    };

    private List<string> _lastnames = new List<string>()
    {
        "Hepburn", "Trump", "Presley", "Obama", "Winfrey",
        "Hendrix", "Jagger", "Winslet", "Windsor", "Middleton"
    };

    private List<string> _medicineNames = new List<string>()
    {
        "Aspirin", "Ibuprofen", "Paracetamol", "Omeprazole", "Lisinopril",
        "Simvastatin", "Metformin", "Amlodipine", "Atorvastatin", "Hydrocodone"
    };

    private List<string> _instructions = new List<string>()
    {
        "Take with food", "Take on an empty stomach", "Take before bedtime", "Take with plenty of water",
        "Take with meals", "Avoid alcohol while taking", "Do not exceed recommended dosage"
    };

    private List<Patient> _patients = new List<Patient>();
    private List<Doctor> _doctors = new List<Doctor>();
    private List<Appointment> _appointments = new List<Appointment>();
    private List<Prescription> _prescriptions = new List<Prescription>();
    private List<Medicine> _medicines = new List<Medicine>();
    private List<PrescriptionMedicine> _prescriptionMedicines = new List<PrescriptionMedicine>();

    public Seeder()
    {
        Random random = new Random();

        // Seed Doctors
        for (int y = 1; y <= 5; y++)
        {
            Doctor doctor = new Doctor();
            doctor.Id = y;
            doctor.FullName = $"{_firstnames[random.Next(_firstnames.Count)]} {_lastnames[random.Next(_lastnames.Count)]}";
            _doctors.Add(doctor);
        }

        // Seed Patients
        for (int x = 1; x <= 20; x++)
        {
            Patient patient = new Patient();
            patient.Id = x;
            patient.FullName = $"{_firstnames[random.Next(_firstnames.Count)]} {_lastnames[random.Next(_lastnames.Count)]}";
            _patients.Add(patient);
        }

        // Seed Appointments
        // Seed Appointments
        HashSet<(int, int)> appointmentKeys = new HashSet<(int, int)>();

        for (int z = 1; z <= 20; z++)
        {
            int doctorId = _doctors[random.Next(_doctors.Count)].Id;
            int patientId = _patients[random.Next(_patients.Count)].Id;

            var appointmentKey = (patientId, doctorId);
            if (appointmentKeys.Contains(appointmentKey))
            {
                // Skip if appointment with same patientId and doctorId exists
                continue;
            }

            Appointment appointment = new Appointment();
            appointment.Id = z;
            appointment.Booking = DateTime.Now.AddDays(random.Next(-30, 30));
            appointment.DoctorId = doctorId;
            appointment.PatientId = patientId;
            _appointments.Add(appointment);

            appointmentKeys.Add(appointmentKey);
        }

        // Seed Medicines

        // Seed Medicines
        for (int i = 1; i <= 10; i++)
        {
            Medicine medicine = new Medicine
            {
                Id = i,
                Name = _medicineNames[i - 1],
                Quantity = random.Next(1, 50),  // Generate random quantity between 1 and 100
                Notes = _instructions[random.Next(_instructions.Count)]  // Randomly select instruction for notes
            };
            _medicines.Add(medicine);
        }

        // Seed Prescriptions and PrescriptionMedicines
        for (int j = 1; j <= 10; j++)
        {
            Prescription prescription = new Prescription
            {
                Id = j,
                IssuedAt = DateTime.Now.AddDays(-random.Next(1, 30)), // Random date within the past month
                PatientId = _patients[random.Next(_patients.Count)].Id,
                DoctorId = _doctors[random.Next(_doctors.Count)].Id
            };
            _prescriptions.Add(prescription);

            // Randomly assign 1 to 3 medicines to each prescription
            int numMedicines = random.Next(1, 4);
            List<int> selectedMedicineIndices = new List<int>();
            while (selectedMedicineIndices.Count < numMedicines)
            {
                int index = random.Next(0, 5); // There are 5 medicines
                if (!selectedMedicineIndices.Contains(index))
                {
                    selectedMedicineIndices.Add(index);
                    PrescriptionMedicine prescriptionMedicine = new PrescriptionMedicine
                    {
                        PrescriptionId = j,
                        MedicineId = _medicines[index].Id,
                        Quantity = random.Next(1, 5), // Adjust as needed
                        Notes = _instructions[random.Next(_instructions.Count)]
                    };
                    _prescriptionMedicines.Add(prescriptionMedicine);
                }
            }
        }
    }

    public List<Patient> Patients { get { return _patients; } }
    public List<Doctor> Doctors { get { return _doctors; } }
    public List<Appointment> Appointments { get { return _appointments; } }

    public List<Prescription> Prescriptions { get { return _prescriptions; } }
    public List<Medicine> Medicines { get { return _medicines; } }
    public List<PrescriptionMedicine> PrescriptionMedicines { get { return _prescriptionMedicines; } }
}
