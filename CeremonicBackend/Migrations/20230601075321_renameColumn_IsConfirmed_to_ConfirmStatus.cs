using Microsoft.EntityFrameworkCore.Migrations;

namespace CeremonicBackend.Migrations
{
    public partial class renameColumn_IsConfirmed_to_ConfirmStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsConfirmed",
                table: "Agreements",
                newName: "ConfirmStatus");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ConfirmStatus",
                table: "Agreements",
                newName: "IsConfirmed");
        }
    }
}
