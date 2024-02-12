using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace workshop.wwwapi.Migrations
{
    /// <inheritdoc />
    public partial class firstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    full_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Medicines",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    notes = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicines", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    full_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    doctor_id = table.Column<int>(type: "integer", nullable: false),
                    patient_id = table.Column<int>(type: "integer", nullable: false),
                    id = table.Column<int>(type: "integer", nullable: false),
                    date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => new { x.patient_id, x.doctor_id });
                    table.ForeignKey(
                        name: "FK_Appointments_Doctors_doctor_id",
                        column: x => x.doctor_id,
                        principalTable: "Doctors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Patients_patient_id",
                        column: x => x.patient_id,
                        principalTable: "Patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prescriptions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    patient_id = table.Column<int>(type: "integer", nullable: false),
                    doctor_id = table.Column<int>(type: "integer", nullable: false),
                    issue_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescriptions", x => x.id);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Doctors_doctor_id",
                        column: x => x.doctor_id,
                        principalTable: "Doctors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Patients_patient_id",
                        column: x => x.patient_id,
                        principalTable: "Patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrescriptionMedicines",
                columns: table => new
                {
                    PrescriptionId = table.Column<int>(type: "integer", nullable: false),
                    MedicineId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrescriptionMedicines", x => new { x.PrescriptionId, x.MedicineId });
                    table.ForeignKey(
                        name: "FK_PrescriptionMedicines_Medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicines",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrescriptionMedicines_Prescriptions_PrescriptionId",
                        column: x => x.PrescriptionId,
                        principalTable: "Prescriptions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "id", "full_name" },
                values: new object[,]
                {
                    { 1, "Donald Middleton" },
                    { 2, "Mick Jagger" },
                    { 3, "Oprah Hepburn" },
                    { 4, "Jimi Hendrix" },
                    { 5, "Jimi Winfrey" }
                });

            migrationBuilder.InsertData(
                table: "Medicines",
                columns: new[] { "id", "name", "notes", "quantity" },
                values: new object[,]
                {
                    { 1, "Aspirin", "Take with food", 19 },
                    { 2, "Ibuprofen", "Take on an empty stomach", 33 },
                    { 3, "Paracetamol", "Take with food", 21 },
                    { 4, "Omeprazole", "Avoid alcohol while taking", 12 },
                    { 5, "Lisinopril", "Take with meals", 15 },
                    { 6, "Simvastatin", "Take with meals", 23 },
                    { 7, "Metformin", "Take with food", 21 },
                    { 8, "Amlodipine", "Take with plenty of water", 29 },
                    { 9, "Atorvastatin", "Take with meals", 24 },
                    { 10, "Hydrocodone", "Take before bedtime", 28 }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "id", "full_name" },
                values: new object[,]
                {
                    { 1, "Kate Obama" },
                    { 2, "Kate Windsor" },
                    { 3, "Jimi Winfrey" },
                    { 4, "Kate Hepburn" },
                    { 5, "Oprah Winslet" },
                    { 6, "Kate Hepburn" },
                    { 7, "Oprah Winfrey" },
                    { 8, "Kate Winslet" },
                    { 9, "Kate Hendrix" },
                    { 10, "Elvis Obama" },
                    { 11, "Mick Obama" },
                    { 12, "Elvis Middleton" },
                    { 13, "Charles Hepburn" },
                    { 14, "Kate Winfrey" },
                    { 15, "Elvis Windsor" },
                    { 16, "Audrey Jagger" },
                    { 17, "Elvis Hendrix" },
                    { 18, "Oprah Presley" },
                    { 19, "Kate Hendrix" },
                    { 20, "Kate Middleton" }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "doctor_id", "patient_id", "date", "id" },
                values: new object[,]
                {
                    { 4, 1, new DateTimeOffset(new DateTime(2024, 2, 21, 9, 24, 33, 735, DateTimeKind.Unspecified).AddTicks(9250), new TimeSpan(0, 1, 0, 0, 0)), 18 },
                    { 5, 1, new DateTimeOffset(new DateTime(2024, 2, 13, 9, 24, 33, 735, DateTimeKind.Unspecified).AddTicks(9195), new TimeSpan(0, 1, 0, 0, 0)), 4 },
                    { 2, 2, new DateTimeOffset(new DateTime(2024, 2, 19, 9, 24, 33, 735, DateTimeKind.Unspecified).AddTicks(9157), new TimeSpan(0, 1, 0, 0, 0)), 2 },
                    { 5, 3, new DateTimeOffset(new DateTime(2024, 2, 3, 9, 24, 33, 735, DateTimeKind.Unspecified).AddTicks(9203), new TimeSpan(0, 1, 0, 0, 0)), 5 },
                    { 1, 4, new DateTimeOffset(new DateTime(2024, 1, 28, 9, 24, 33, 735, DateTimeKind.Unspecified).AddTicks(9162), new TimeSpan(0, 1, 0, 0, 0)), 3 },
                    { 4, 4, new DateTimeOffset(new DateTime(2024, 3, 4, 9, 24, 33, 735, DateTimeKind.Unspecified).AddTicks(9212), new TimeSpan(0, 1, 0, 0, 0)), 7 },
                    { 5, 4, new DateTimeOffset(new DateTime(2024, 2, 12, 9, 24, 33, 735, DateTimeKind.Unspecified).AddTicks(9241), new TimeSpan(0, 1, 0, 0, 0)), 15 },
                    { 5, 5, new DateTimeOffset(new DateTime(2024, 2, 23, 9, 24, 33, 735, DateTimeKind.Unspecified).AddTicks(9247), new TimeSpan(0, 1, 0, 0, 0)), 17 },
                    { 1, 8, new DateTimeOffset(new DateTime(2024, 2, 29, 9, 24, 33, 735, DateTimeKind.Unspecified).AddTicks(9256), new TimeSpan(0, 1, 0, 0, 0)), 20 },
                    { 3, 9, new DateTimeOffset(new DateTime(2024, 2, 22, 9, 24, 33, 735, DateTimeKind.Unspecified).AddTicks(9228), new TimeSpan(0, 1, 0, 0, 0)), 11 },
                    { 5, 10, new DateTimeOffset(new DateTime(2024, 2, 10, 9, 24, 33, 735, DateTimeKind.Unspecified).AddTicks(9244), new TimeSpan(0, 1, 0, 0, 0)), 16 },
                    { 5, 12, new DateTimeOffset(new DateTime(2024, 2, 20, 9, 24, 33, 735, DateTimeKind.Unspecified).AddTicks(9220), new TimeSpan(0, 1, 0, 0, 0)), 9 },
                    { 5, 13, new DateTimeOffset(new DateTime(2024, 2, 25, 9, 24, 33, 735, DateTimeKind.Unspecified).AddTicks(9208), new TimeSpan(0, 1, 0, 0, 0)), 6 },
                    { 3, 14, new DateTimeOffset(new DateTime(2024, 2, 24, 9, 24, 33, 735, DateTimeKind.Unspecified).AddTicks(9224), new TimeSpan(0, 1, 0, 0, 0)), 10 },
                    { 1, 16, new DateTimeOffset(new DateTime(2024, 1, 29, 9, 24, 33, 735, DateTimeKind.Unspecified).AddTicks(9051), new TimeSpan(0, 1, 0, 0, 0)), 1 },
                    { 4, 16, new DateTimeOffset(new DateTime(2024, 1, 29, 9, 24, 33, 735, DateTimeKind.Unspecified).AddTicks(9215), new TimeSpan(0, 1, 0, 0, 0)), 8 },
                    { 3, 18, new DateTimeOffset(new DateTime(2024, 3, 1, 9, 24, 33, 735, DateTimeKind.Unspecified).AddTicks(9238), new TimeSpan(0, 1, 0, 0, 0)), 14 },
                    { 4, 18, new DateTimeOffset(new DateTime(2024, 2, 13, 9, 24, 33, 735, DateTimeKind.Unspecified).AddTicks(9253), new TimeSpan(0, 1, 0, 0, 0)), 19 }
                });

            migrationBuilder.InsertData(
                table: "Prescriptions",
                columns: new[] { "id", "doctor_id", "issue_date", "patient_id" },
                values: new object[,]
                {
                    { 1, 2, new DateTimeOffset(new DateTime(2024, 1, 17, 9, 24, 33, 735, DateTimeKind.Unspecified).AddTicks(9271), new TimeSpan(0, 1, 0, 0, 0)), 6 },
                    { 2, 4, new DateTimeOffset(new DateTime(2024, 1, 29, 9, 24, 33, 735, DateTimeKind.Unspecified).AddTicks(9304), new TimeSpan(0, 1, 0, 0, 0)), 9 },
                    { 3, 5, new DateTimeOffset(new DateTime(2024, 2, 5, 9, 24, 33, 735, DateTimeKind.Unspecified).AddTicks(9310), new TimeSpan(0, 1, 0, 0, 0)), 11 },
                    { 4, 1, new DateTimeOffset(new DateTime(2024, 2, 4, 9, 24, 33, 735, DateTimeKind.Unspecified).AddTicks(9313), new TimeSpan(0, 1, 0, 0, 0)), 14 },
                    { 5, 1, new DateTimeOffset(new DateTime(2024, 2, 7, 9, 24, 33, 735, DateTimeKind.Unspecified).AddTicks(9317), new TimeSpan(0, 1, 0, 0, 0)), 3 },
                    { 6, 1, new DateTimeOffset(new DateTime(2024, 2, 5, 9, 24, 33, 735, DateTimeKind.Unspecified).AddTicks(9322), new TimeSpan(0, 1, 0, 0, 0)), 1 },
                    { 7, 4, new DateTimeOffset(new DateTime(2024, 1, 16, 9, 24, 33, 735, DateTimeKind.Unspecified).AddTicks(9325), new TimeSpan(0, 1, 0, 0, 0)), 9 },
                    { 8, 5, new DateTimeOffset(new DateTime(2024, 1, 27, 9, 24, 33, 735, DateTimeKind.Unspecified).AddTicks(9330), new TimeSpan(0, 1, 0, 0, 0)), 4 },
                    { 9, 3, new DateTimeOffset(new DateTime(2024, 2, 7, 9, 24, 33, 735, DateTimeKind.Unspecified).AddTicks(9334), new TimeSpan(0, 1, 0, 0, 0)), 8 },
                    { 10, 5, new DateTimeOffset(new DateTime(2024, 1, 26, 9, 24, 33, 735, DateTimeKind.Unspecified).AddTicks(9338), new TimeSpan(0, 1, 0, 0, 0)), 3 }
                });

            migrationBuilder.InsertData(
                table: "PrescriptionMedicines",
                columns: new[] { "MedicineId", "PrescriptionId", "Notes", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, "Take on an empty stomach", 3 },
                    { 2, 1, "Do not exceed recommended dosage", 1 },
                    { 1, 2, "Take with food", 3 },
                    { 3, 2, "Take with meals", 1 },
                    { 5, 2, "Avoid alcohol while taking", 1 },
                    { 1, 3, "Take with meals", 3 },
                    { 4, 3, "Avoid alcohol while taking", 2 },
                    { 5, 3, "Take with plenty of water", 4 },
                    { 1, 4, "Take with food", 4 },
                    { 1, 5, "Take with plenty of water", 2 },
                    { 3, 5, "Take before bedtime", 4 },
                    { 5, 5, "Take with plenty of water", 4 },
                    { 4, 6, "Take with food", 3 },
                    { 2, 7, "Take with meals", 1 },
                    { 3, 7, "Avoid alcohol while taking", 2 },
                    { 5, 7, "Take on an empty stomach", 4 },
                    { 2, 8, "Do not exceed recommended dosage", 1 },
                    { 4, 8, "Take with meals", 1 },
                    { 2, 9, "Take on an empty stomach", 3 },
                    { 4, 9, "Take before bedtime", 1 },
                    { 4, 10, "Take with meals", 1 },
                    { 5, 10, "Take before bedtime", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_doctor_id",
                table: "Appointments",
                column: "doctor_id");

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionMedicines_MedicineId",
                table: "PrescriptionMedicines",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_doctor_id",
                table: "Prescriptions",
                column: "doctor_id");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_patient_id",
                table: "Prescriptions",
                column: "patient_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "PrescriptionMedicines");

            migrationBuilder.DropTable(
                name: "Medicines");

            migrationBuilder.DropTable(
                name: "Prescriptions");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
