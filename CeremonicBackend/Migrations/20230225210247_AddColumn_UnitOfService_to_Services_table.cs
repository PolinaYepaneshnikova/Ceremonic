using Microsoft.EntityFrameworkCore.Migrations;

namespace CeremonicBackend.Migrations
{
    public partial class addColumn_UnitOfService_to_Services_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "ScalarReturn<int>");

            migrationBuilder.AddColumn<string>(
                name: "UnitOfService",
                table: "Services",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitOfService",
                table: "Services");

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
