using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatesAPI.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DateDetails",
                columns: table => new
                {
                    DateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Event = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EventNote = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    InitialLoggedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DateDetails", x => x.DateId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DateDetails");
        }
    }
}
