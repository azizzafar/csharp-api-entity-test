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
            //modelBuilder.Entity<Appointment>()
            //    .HasOne(a => a.Doctor)
            //    .WithMany() // Assuming Doctor can have multiple Appointments
            //    .HasForeignKey(a => a.DoctorId)
            //    .OnDelete(DeleteBehavior.Restrict);


            //modelBuilder.Entity<Appointment>()
            //    .HasOne(a => a.Patient)
            //    .WithMany() // Assuming a Patient can have multiple Appointments
            //    .HasForeignKey(a => a.PatientId)
            //    .OnDelete(DeleteBehavior.Restrict);


            //base.OnModelCreating(modelBuilder);

            Seeder seeder = new Seeder();

            modelBuilder.Entity<Doctor>().HasData(seeder.Doctors);
            modelBuilder.Entity<Patient>().HasData(seeder.Patients);
            modelBuilder.Entity<Appointment>().HasData(seeder.Appointments);


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
    }
}
