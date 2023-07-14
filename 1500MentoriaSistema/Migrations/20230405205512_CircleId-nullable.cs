using Microsoft.EntityFrameworkCore.Migrations;

namespace _1500MentoriaSistema.Migrations
{
    public partial class CircleIdnullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Circle_CircleId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "CircleId",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Circle_CircleId",
                table: "AspNetUsers",
                column: "CircleId",
                principalTable: "Circle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Circle_CircleId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "CircleId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Circle_CircleId",
                table: "AspNetUsers",
                column: "CircleId",
                principalTable: "Circle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
