using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bankproject.Migrations
{
    public partial class changingtransactiontype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionTypeId",
                table: "Transaction");

            migrationBuilder.AddColumn<string>(
                name: "TransactionType",
                table: "Transaction",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionType",
                table: "Transaction");

            migrationBuilder.AddColumn<int>(
                name: "TransactionTypeId",
                table: "Transaction",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
