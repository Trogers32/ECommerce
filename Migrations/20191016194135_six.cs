using Microsoft.EntityFrameworkCore.Migrations;

namespace ECommerce.Migrations
{
    public partial class six : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activitees_Users_CoordinatorUserId",
                table: "Activitees");

            migrationBuilder.DropIndex(
                name: "IX_Activitees_CoordinatorUserId",
                table: "Activitees");

            migrationBuilder.DropColumn(
                name: "CoordinatorUserId",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "CoordinatorUserId",
                table: "Activitees",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Activitees_CoordinatorUserId",
                table: "Activitees",
                column: "CoordinatorUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activitees_Users_CoordinatorUserId",
                table: "Activitees",
                column: "CoordinatorUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
