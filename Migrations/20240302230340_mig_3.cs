using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebUI.Migrations
{
    /// <inheritdoc />
    public partial class mig_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FClasses_Users_UserId",
                table: "FClasses");

            migrationBuilder.DropForeignKey(
                name: "FK_LClasses_Users_UserId",
                table: "LClasses");

            migrationBuilder.DropIndex(
                name: "IX_LClasses_UserId",
                table: "LClasses");

            migrationBuilder.DropIndex(
                name: "IX_FClasses_UserId",
                table: "FClasses");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "LClasses");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "FClasses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "LClasses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "FClasses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LClasses_UserId",
                table: "LClasses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FClasses_UserId",
                table: "FClasses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FClasses_Users_UserId",
                table: "FClasses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LClasses_Users_UserId",
                table: "LClasses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
