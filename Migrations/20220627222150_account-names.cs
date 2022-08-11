using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bankproject.Migrations
{
    public partial class accountnames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountName",
                table: "Transaction",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TransferAccountName",
                table: "Transaction",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountName",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "TransferAccountName",
                table: "Transaction");
        }
    }
}
