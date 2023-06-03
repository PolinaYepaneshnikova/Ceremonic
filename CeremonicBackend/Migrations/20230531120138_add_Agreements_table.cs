using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CeremonicBackend.Migrations
{
    public partial class add_Agreements_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "ScalarReturn<int>");

            migrationBuilder.CreateTable(
                name: "Agreements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProviderId = table.Column<int>(type: "INTEGER", nullable: false),
                    ClientId = table.Column<int>(type: "INTEGER", nullable: false),
                    DateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    Location = table.Column<string>(type: "TEXT", nullable: true),
                    Service = table.Column<string>(type: "TEXT", nullable: true),
                    IsConfirmed = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agreements", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Agreements");

            //migrationBuilder.CreateTable(
            //    name: "ScalarReturn<int>",
            //    columns: table => new
            //    {
            //        Value = table.Column<int>(type: "INTEGER", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //    });
        }
    }
}
