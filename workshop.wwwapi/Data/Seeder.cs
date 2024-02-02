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

    private List<Patient> _patients = new List<Patient>();
    private List<Doctor> _doctors = new List<Doctor>();
    private List<Appointment> _appointments = new List<Appointment>();

    public Seeder()
    {
        Random random = new Random();

        // Seed Doctors
        for (int y = 1; y <= 10; y++)
        {
            Doctor doctor = new Doctor();
            doctor.Id = y;
            doctor.FullName = $"{_firstnames[random.Next(_firstnames.Count)]} {_lastnames[random.Next(_lastnames.Count)]}";
            _doctors.Add(doctor);
        }

        // Seed Patients
        for (int x = 1; x <= 250; x++)
        {
            Patient patient = new Patient();
            patient.Id = x;
            patient.FullName = $"{_firstnames[random.Next(_firstnames.Count)]} {_lastnames[random.Next(_lastnames.Count)]}";
            patient.DoctorId = _doctors[random.Next(_doctors.Count)].Id;
            _patients.Add(patient);
        }

        // Seed Appointments
        HashSet<(int, int)> appointmentKeys = new HashSet<(int, int)>();

        for (int z = 1; z <= 100; z++)
        {

            int doctorId = _doctors[random.Next(_doctors.Count)].Id;
            int patientId = _patients[random.Next(_patients.Count)].Id;

            var appointmentKey = (doctorId, patientId);
            if (appointmentKeys.Contains(appointmentKey))
            {
                // Skip if appointment with same doctorId and patientId exists
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
    }

    public List<Patient> Patients { get { return _patients; } }
    public List<Doctor> Doctors { get { return _doctors; } }
    public List<Appointment> Appointments { get { return _appointments; } }
}
