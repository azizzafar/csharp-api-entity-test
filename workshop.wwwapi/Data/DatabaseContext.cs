using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using workshop.wwwapi.Models;

namespace workshop.wwwapi.Data
{
    public class DatabaseContext : DbContext
    {
        private string _connectionString;
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            _connectionString = configuration.GetValue<string>("ConnectionStrings:DefaultConnectionString")!;
            //this.Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //TODO: Appointment Key etc.. Add Here

            modelBuilder.Entity<Appointment>().HasKey(a => new { a.PatientId, a.DoctorId });


            // Configure the many-to-many relationship between Prescription and Medicine
            modelBuilder.Entity<PrescriptionMedicine>()
                .HasKey(pm => new { pm.PrescriptionId, pm.MedicineId });

            modelBuilder.Entity<PrescriptionMedicine>()
                .HasOne(pm => pm.Prescription)
                .WithMany(p => p.PrescriptionMedicines)
                .HasForeignKey(pm => pm.PrescriptionId);

            modelBuilder.Entity<PrescriptionMedicine>()
                .HasOne(pm => pm.Medicine)
                .WithMany(m => m.PrescriptionMedicines)
                .HasForeignKey(pm => pm.MedicineId);

            // Seed Data
            Seeder seeder = new Seeder();
            modelBuilder.Entity<Doctor>().HasData(seeder.Doctors);
            modelBuilder.Entity<Patient>().HasData(seeder.Patients);
            modelBuilder.Entity<Appointment>().HasData(seeder.Appointments);
            modelBuilder.Entity<Medicine>().HasData(seeder.Medicines);
            modelBuilder.Entity<Prescription>().HasData(seeder.Prescriptions);
            modelBuilder.Entity<PrescriptionMedicine>().HasData(seeder.PrescriptionMedicines);

            //TODO: Seed Data Here

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseInMemoryDatabase(databaseName: "Database");
            optionsBuilder.EnableSensitiveDataLogging();

            optionsBuilder.UseNpgsql(_connectionString);
            optionsBuilder.LogTo(message => Debug.WriteLine(message)); //see the sql EF using in the console

        }


        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<PrescriptionMedicine> PrescriptionMedicines { get; set; }

    }
}
