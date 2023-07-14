using Microsoft.EntityFrameworkCore.Migrations;

namespace _1500MentoriaSistema.Migrations
{
    public partial class ModelPeople : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IdentityRole",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NormalizedName = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRole", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "68d78966-3b8c-42f4-9a0d-547170588e3d", "f105ec29-ead2-4888-ba6e-8c045000fd51", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c35f7b6c-5b7e-4920-86f3-feff61852408", "cb759b84-e859-453f-9b0c-0a8042044f8b", "Employer", "EMPLOYER" });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "141515d4-9737-42fa-8265-ba37603b14ca", "b20425cc-215a-4b8f-ab80-da5a957b72cd", "Basic", "BASIC" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdentityRole");
        }
    }
}
