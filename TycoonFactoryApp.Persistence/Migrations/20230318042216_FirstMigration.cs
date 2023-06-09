using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TycoonFactoryApp.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActivityType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AndroidWorkers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AndroidWorkers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActivitiesWorkers",
                columns: table => new
                {
                    ActivityId = table.Column<int>(type: "int", nullable: false),
                    WorkerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivitiesWorkers", x => new { x.ActivityId, x.WorkerId });
                    table.ForeignKey(
                        name: "FK_ActivitiesWorkers_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivitiesWorkers_AndroidWorkers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "AndroidWorkers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AndroidWorkers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { -26, "Z" },
                    { -25, "Y" },
                    { -24, "X" },
                    { -23, "W" },
                    { -22, "V" },
                    { -21, "U" },
                    { -20, "T" },
                    { -19, "S" },
                    { -18, "R" },
                    { -17, "Q" },
                    { -16, "P" },
                    { -15, "O" },
                    { -14, "N" },
                    { -13, "M" },
                    { -12, "L" },
                    { -11, "K" },
                    { -10, "J" },
                    { -9, "I" },
                    { -8, "H" },
                    { -7, "G" },
                    { -6, "F" },
                    { -5, "E" },
                    { -4, "D" },
                    { -3, "C" },
                    { -2, "B" },
                    { -1, "A" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivitiesWorkers_WorkerId",
                table: "ActivitiesWorkers",
                column: "WorkerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivitiesWorkers");

            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "AndroidWorkers");
        }
    }
}
