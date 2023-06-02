using Microsoft.EntityFrameworkCore.Migrations;

namespace CeremonicBackend.Migrations
{
    public partial class addInitialDataTo_Services_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "ScalarReturn<int>");

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Name", "UnitOfService" },
                values: new object[] { 1, "Банкетна зала", "*за годину аренди" });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Name", "UnitOfService" },
                values: new object[] { 16, "Автомобіль наречених", "*за годину аренди" });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Name", "UnitOfService" },
                values: new object[] { 15, "Перукар", "*за весільну укладку" });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Name", "UnitOfService" },
                values: new object[] { 14, "Візажист", "*за весільний макіяж" });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Name", "UnitOfService" },
                values: new object[] { 13, "Технічна підтримка", "*за годину роботи" });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Name", "UnitOfService" },
                values: new object[] { 12, "Музика", "*за годину роботи" });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Name", "UnitOfService" },
                values: new object[] { 11, "Відеозйомка", "*за весілля" });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Name", "UnitOfService" },
                values: new object[] { 10, "Фотозйомка", "*за весілля" });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Name", "UnitOfService" },
                values: new object[] { 9, "Кондитер", "*за кг весільного торту" });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Name", "UnitOfService" },
                values: new object[] { 8, "Поліграфія", "*усі паперові вироби для весілля" });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Name", "UnitOfService" },
                values: new object[] { 7, "Декор та освіщення", "*за стандартну композицію" });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Name", "UnitOfService" },
                values: new object[] { 6, "Флористика та декор", "*на весілля" });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Name", "UnitOfService" },
                values: new object[] { 5, "Їжа/кайтеринг", "*середній чек на одну людину" });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Name", "UnitOfService" },
                values: new object[] { 4, "Ведучий", "*за весілля" });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Name", "UnitOfService" },
                values: new object[] { 3, "Цермоніймейстр", "*за церемонію" });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Name", "UnitOfService" },
                values: new object[] { 2, "Місце проведення церемонії", "*за годину аренди" });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Name", "UnitOfService" },
                values: new object[] { 17, "Транспорт для гостей", "*за кожні 10 км" });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Name", "UnitOfService" },
                values: new object[] { 18, "Букет нареченої", "*за букет" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 18);

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
