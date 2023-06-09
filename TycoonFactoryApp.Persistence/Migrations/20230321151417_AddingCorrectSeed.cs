using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TycoonFactoryApp.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddingCorrectSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ActivitiesWorkers",
                keyColumns: new[] { "ActivityId", "WorkerId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "ActivitiesWorkers",
                keyColumns: new[] { "ActivityId", "WorkerId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "ActivitiesWorkers",
                keyColumns: new[] { "ActivityId", "WorkerId" },
                keyValues: new object[] { 3, 8 });

            migrationBuilder.DeleteData(
                table: "ActivitiesWorkers",
                keyColumns: new[] { "ActivityId", "WorkerId" },
                keyValues: new object[] { 3, 9 });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 3, 21, 12, 14, 17, 549, DateTimeKind.Local).AddTicks(4048), new DateTime(2023, 3, 21, 10, 14, 17, 549, DateTimeKind.Local).AddTicks(4034) });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 3, 21, 14, 14, 17, 549, DateTimeKind.Local).AddTicks(4056), new DateTime(2023, 3, 21, 12, 15, 17, 549, DateTimeKind.Local).AddTicks(4055) });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 3, 21, 17, 14, 17, 549, DateTimeKind.Local).AddTicks(4059), new DateTime(2023, 3, 21, 14, 15, 17, 549, DateTimeKind.Local).AddTicks(4058) });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 3, 21, 19, 14, 17, 549, DateTimeKind.Local).AddTicks(4060), new DateTime(2023, 3, 21, 17, 15, 17, 549, DateTimeKind.Local).AddTicks(4060) });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ActivityType", "EndDate", "StartDate" },
                values: new object[] { 2, new DateTime(2023, 3, 21, 21, 14, 17, 549, DateTimeKind.Local).AddTicks(4063), new DateTime(2023, 3, 21, 19, 15, 17, 549, DateTimeKind.Local).AddTicks(4062) });

            migrationBuilder.InsertData(
                table: "ActivitiesWorkers",
                columns: new[] { "ActivityId", "WorkerId" },
                values: new object[,]
                {
                    { 4, 8 },
                    { 5, 2 },
                    { 5, 9 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ActivitiesWorkers",
                keyColumns: new[] { "ActivityId", "WorkerId" },
                keyValues: new object[] { 4, 8 });

            migrationBuilder.DeleteData(
                table: "ActivitiesWorkers",
                keyColumns: new[] { "ActivityId", "WorkerId" },
                keyValues: new object[] { 5, 2 });

            migrationBuilder.DeleteData(
                table: "ActivitiesWorkers",
                keyColumns: new[] { "ActivityId", "WorkerId" },
                keyValues: new object[] { 5, 9 });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 3, 18, 1, 49, 21, 560, DateTimeKind.Local).AddTicks(7940), new DateTime(2023, 3, 17, 23, 49, 21, 560, DateTimeKind.Local).AddTicks(7930) });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 3, 18, 2, 49, 21, 560, DateTimeKind.Local).AddTicks(7947), new DateTime(2023, 3, 17, 23, 49, 21, 560, DateTimeKind.Local).AddTicks(7946) });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 3, 18, 3, 49, 21, 560, DateTimeKind.Local).AddTicks(7949), new DateTime(2023, 3, 17, 23, 49, 21, 560, DateTimeKind.Local).AddTicks(7948) });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 3, 18, 4, 49, 21, 560, DateTimeKind.Local).AddTicks(7950), new DateTime(2023, 3, 17, 23, 49, 21, 560, DateTimeKind.Local).AddTicks(7950) });

            migrationBuilder.UpdateData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ActivityType", "EndDate", "StartDate" },
                values: new object[] { 1, new DateTime(2023, 3, 18, 5, 49, 21, 560, DateTimeKind.Local).AddTicks(7953), new DateTime(2023, 3, 17, 23, 49, 21, 560, DateTimeKind.Local).AddTicks(7952) });

            migrationBuilder.InsertData(
                table: "ActivitiesWorkers",
                columns: new[] { "ActivityId", "WorkerId" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 1, 3 },
                    { 3, 8 },
                    { 3, 9 }
                });
        }
    }
}
