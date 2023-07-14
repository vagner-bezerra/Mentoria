using Microsoft.EntityFrameworkCore.Migrations;

namespace _1500MentoriaSistema.Migrations
{
    public partial class ChangeStates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Delivered",
                table: "ActualStates",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.Sql("UPDATE [dbo].[ActualStates] SET Delivered = CASE WHEN Delivered = 0 THEN 1 ELSE 2 END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Delivered",
                table: "ActualStates",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
