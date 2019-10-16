using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ECommerce.Migrations
{
    public partial class three : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Associations_Activity_ActivityId",
                table: "Associations");

            migrationBuilder.DropTable(
                name: "Activity");

            migrationBuilder.CreateTable(
                name: "Activitee",
                columns: table => new
                {
                    ActivityId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ActivityTitle = table.Column<string>(nullable: false),
                    ActivityDate = table.Column<DateTime>(nullable: false),
                    ActivityDuration = table.Column<int>(nullable: false),
                    ActivityStart = table.Column<string>(nullable: false),
                    ActivityTimeType = table.Column<string>(nullable: false),
                    ActivityDescription = table.Column<string>(nullable: false),
                    CoordinatorUserId = table.Column<int>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activitee", x => x.ActivityId);
                    table.ForeignKey(
                        name: "FK_Activitee_Users_CoordinatorUserId",
                        column: x => x.CoordinatorUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activitee_CoordinatorUserId",
                table: "Activitee",
                column: "CoordinatorUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Associations_Activitee_ActivityId",
                table: "Associations",
                column: "ActivityId",
                principalTable: "Activitee",
                principalColumn: "ActivityId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Associations_Activitee_ActivityId",
                table: "Associations");

            migrationBuilder.DropTable(
                name: "Activitee");

            migrationBuilder.CreateTable(
                name: "Activity",
                columns: table => new
                {
                    ActivityId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ActivityDate = table.Column<DateTime>(nullable: false),
                    ActivityDescription = table.Column<string>(nullable: false),
                    ActivityDuration = table.Column<int>(nullable: false),
                    ActivityStart = table.Column<string>(nullable: false),
                    ActivityTimeType = table.Column<string>(nullable: false),
                    ActivityTitle = table.Column<string>(nullable: false),
                    CoordinatorUserId = table.Column<int>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activity", x => x.ActivityId);
                    table.ForeignKey(
                        name: "FK_Activity_Users_CoordinatorUserId",
                        column: x => x.CoordinatorUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activity_CoordinatorUserId",
                table: "Activity",
                column: "CoordinatorUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Associations_Activity_ActivityId",
                table: "Associations",
                column: "ActivityId",
                principalTable: "Activity",
                principalColumn: "ActivityId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
