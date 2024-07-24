using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace TaskApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTransitionStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StatusTransitions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CurrentStatusId = table.Column<int>(type: "int", nullable: false),
                    NextStatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusTransitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatusTransitions_Statuses_CurrentStatusId",
                        column: x => x.CurrentStatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StatusTransitions_Statuses_NextStatusId",
                        column: x => x.NextStatusId,
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_StatusTransitions_CurrentStatusId",
                table: "StatusTransitions",
                column: "CurrentStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_StatusTransitions_NextStatusId",
                table: "StatusTransitions",
                column: "NextStatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StatusTransitions");
        }
    }
}
