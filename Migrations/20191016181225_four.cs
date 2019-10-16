using Microsoft.EntityFrameworkCore.Migrations;

namespace ECommerce.Migrations
{
    public partial class four : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activitee_Users_CoordinatorUserId",
                table: "Activitee");

            migrationBuilder.DropForeignKey(
                name: "FK_Associations_Activitee_ActivityId",
                table: "Associations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Activitee",
                table: "Activitee");

            migrationBuilder.RenameTable(
                name: "Activitee",
                newName: "Activitees");

            migrationBuilder.RenameIndex(
                name: "IX_Activitee_CoordinatorUserId",
                table: "Activitees",
                newName: "IX_Activitees_CoordinatorUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Activitees",
                table: "Activitees",
                column: "ActivityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activitees_Users_CoordinatorUserId",
                table: "Activitees",
                column: "CoordinatorUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Associations_Activitees_ActivityId",
                table: "Associations",
                column: "ActivityId",
                principalTable: "Activitees",
                principalColumn: "ActivityId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activitees_Users_CoordinatorUserId",
                table: "Activitees");

            migrationBuilder.DropForeignKey(
                name: "FK_Associations_Activitees_ActivityId",
                table: "Associations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Activitees",
                table: "Activitees");

            migrationBuilder.RenameTable(
                name: "Activitees",
                newName: "Activitee");

            migrationBuilder.RenameIndex(
                name: "IX_Activitees_CoordinatorUserId",
                table: "Activitee",
                newName: "IX_Activitee_CoordinatorUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Activitee",
                table: "Activitee",
                column: "ActivityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activitee_Users_CoordinatorUserId",
                table: "Activitee",
                column: "CoordinatorUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Associations_Activitee_ActivityId",
                table: "Associations",
                column: "ActivityId",
                principalTable: "Activitee",
                principalColumn: "ActivityId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
