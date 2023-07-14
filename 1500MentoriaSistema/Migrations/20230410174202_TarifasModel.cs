using Microsoft.EntityFrameworkCore.Migrations;

namespace _1500MentoriaSistema.Migrations
{
    public partial class TarifasModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Themes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Project",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Learnings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "HoursDay",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Feedbacks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ActualStates",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Themes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Learnings");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "HoursDay");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ActualStates");
        }
    }
}
