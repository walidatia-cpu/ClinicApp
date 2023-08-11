using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicApp.DAL.Migrations
{
    /// <inheritdoc />
    public partial class add_Patient_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PasNumber = table.Column<int>(type: "int", nullable: false),
                    Forenames = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SexCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HomeTelephoneNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NokName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NokRelationshipCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NokAddressLine1 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NokAddressLine2 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NokAddressLine3 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NokAddressLine4 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NokPostcode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GpCode = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    GpSurname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GpInitials = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GpPhone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AccountId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patients_AspNetUsers_AccountId",
                        column: x => x.AccountId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Patients_AccountId",
                table: "Patients",
                column: "AccountId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
