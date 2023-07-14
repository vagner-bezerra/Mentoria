using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace _1500MentoriaSistema.Migrations
{
    /// <inheritdoc />
    public partial class ModelHoursChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "102c3373-dcf4-4243-9b8b-44bf18ff8eb8");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "7f9ac3be-41b2-4073-8a78-847d3ac08d02");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "b5bd4547-b32b-418b-a799-479b8d72ee08");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "HoursDay",
                newName: "PersonId");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5105ba85-44a5-4b5b-b5fc-a6bd0e32e9dc", null, "Employer", "EMPLOYER" },
                    { "d2759c62-34b6-4bf1-a303-081b6ddb52b3", null, "Admin", "ADMIN" },
                    { "ed58c2c8-0675-4797-8b04-ea780ccd8e46", null, "Basic", "BASIC" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_HoursDay_PersonId",
                table: "HoursDay",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_HoursDay_AspNetUsers_PersonId",
                table: "HoursDay",
                column: "PersonId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HoursDay_AspNetUsers_PersonId",
                table: "HoursDay");

            migrationBuilder.DropIndex(
                name: "IX_HoursDay_PersonId",
                table: "HoursDay");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "5105ba85-44a5-4b5b-b5fc-a6bd0e32e9dc");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "d2759c62-34b6-4bf1-a303-081b6ddb52b3");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "ed58c2c8-0675-4797-8b04-ea780ccd8e46");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "HoursDay",
                newName: "UserId");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "102c3373-dcf4-4243-9b8b-44bf18ff8eb8", "dbbb71f5-7b1f-416d-9949-9ff80f5cc85e", "Basic", "BASIC" },
                    { "7f9ac3be-41b2-4073-8a78-847d3ac08d02", "153f91cd-cca8-4e83-b21b-3bf03026a88e", "Admin", "ADMIN" },
                    { "b5bd4547-b32b-418b-a799-479b8d72ee08", "9b44300a-50cc-4829-b477-6525eeb385d3", "Employer", "EMPLOYER" }
                });
        }
    }
}
