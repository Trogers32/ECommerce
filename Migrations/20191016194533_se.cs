using Microsoft.EntityFrameworkCore.Migrations;

namespace ECommerce.Migrations
{
    public partial class se : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activitees_Users_UserId",
                table: "Activitees");

            migrationBuilder.DropIndex(
                name: "IX_Activitees_UserId",
                table: "Activitees");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Activitees");

            migrationBuilder.AddColumn<string>(
                name: "Coordinator",
                table: "Activitees",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Coordinator",
                table: "Activitees");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Activitees",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Activitees_UserId",
                table: "Activitees",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activitees_Users_UserId",
                table: "Activitees",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
