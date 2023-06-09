using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TycoonFactoryApp.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddingNewSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AndroidWorkers",
                keyColumn: "Id",
                keyValue: -26);

            migrationBuilder.DeleteData(
                table: "AndroidWorkers",
                keyColumn: "Id",
                keyValue: -25);

            migrationBuilder.DeleteData(
                table: "AndroidWorkers",
                keyColumn: "Id",
                keyValue: -24);

            migrationBuilder.DeleteData(
                table: "AndroidWorkers",
                keyColumn: "Id",
                keyValue: -23);

            migrationBuilder.DeleteData(
                table: "AndroidWorkers",
                keyColumn: "Id",
                keyValue: -22);

            migrationBuilder.DeleteData(
                table: "AndroidWorkers",
                keyColumn: "Id",
                keyValue: -21);

            migrationBuilder.DeleteData(
                table: "AndroidWorkers",
                keyColumn: "Id",
                keyValue: -20);

            migrationBuilder.DeleteData(
                table: "AndroidWorkers",
                keyColumn: "Id",
                keyValue: -19);

            migrationBuilder.DeleteData(
                table: "AndroidWorkers",
                keyColumn: "Id",
                keyValue: -18);

            migrationBuilder.DeleteData(
                table: "AndroidWorkers",
                keyColumn: "Id",
                keyValue: -17);

            migrationBuilder.DeleteData(
                table: "AndroidWorkers",
                keyColumn: "Id",
                keyValue: -16);

            migrationBuilder.DeleteData(
                table: "AndroidWorkers",
                keyColumn: "Id",
                keyValue: -15);

            migrationBuilder.DeleteData(
                table: "AndroidWorkers",
                keyColumn: "Id",
                keyValue: -14);

            migrationBuilder.DeleteData(
                table: "AndroidWorkers",
                keyColumn: "Id",
                keyValue: -13);

            migrationBuilder.DeleteData(
                table: "AndroidWorkers",
                keyColumn: "Id",
                keyValue: -12);

            migrationBuilder.DeleteData(
                table: "AndroidWorkers",
                keyColumn: "Id",
                keyValue: -11);

            migrationBuilder.DeleteData(
                table: "AndroidWorkers",
                keyColumn: "Id",
                keyValue: -10);

            migrationBuilder.DeleteData(
                table: "AndroidWorkers",
                keyColumn: "Id",
                keyValue: -9);

            migrationBuilder.DeleteData(
                table: "AndroidWorkers",
                keyColumn: "Id",
                keyValue: -8);

            migrationBuilder.DeleteData(
                table: "AndroidWorkers",
                keyColumn: "Id",
                keyValue: -7);

            migrationBuilder.DeleteData(
                table: "AndroidWorkers",
                keyColumn: "Id",
                keyValue: -6);

            migrationBuilder.DeleteData(
                table: "AndroidWorkers",
                keyColumn: "Id",
                keyValue: -5);

            migrationBuilder.DeleteData(
                table: "AndroidWorkers",
                keyColumn: "Id",
                keyValue: -4);

            migrationBuilder.DeleteData(
                table: "AndroidWorkers",
                keyColumn: "Id",
                keyValue: -3);

            migrationBuilder.DeleteData(
                table: "AndroidWorkers",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "AndroidWorkers",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.InsertData(
                table: "Activities",
                columns: new[] { "Id", "ActivityType", "EndDate", "StartDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 3, 18, 1, 49, 21, 560, DateTimeKind.Local).AddTicks(7940), new DateTime(2023, 3, 17, 23, 49, 21, 560, DateTimeKind.Local).AddTicks(7930) },
                    { 2, 2, new DateTime(2023, 3, 18, 2, 49, 21, 560, DateTimeKind.Local).AddTicks(7947), new DateTime(2023, 3, 17, 23, 49, 21, 560, DateTimeKind.Local).AddTicks(7946) },
                    { 3, 1, new DateTime(2023, 3, 18, 3, 49, 21, 560, DateTimeKind.Local).AddTicks(7949), new DateTime(2023, 3, 17, 23, 49, 21, 560, DateTimeKind.Local).AddTicks(7948) },
                    { 4, 1, new DateTime(2023, 3, 18, 4, 49, 21, 560, DateTimeKind.Local).AddTicks(7950), new DateTime(2023, 3, 17, 23, 49, 21, 560, DateTimeKind.Local).AddTicks(7950) },
                    { 5, 1, new DateTime(2023, 3, 18, 5, 49, 21, 560, DateTimeKind.Local).AddTicks(7953), new DateTime(2023, 3, 17, 23, 49, 21, 560, DateTimeKind.Local).AddTicks(7952) }
                });

            migrationBuilder.InsertData(
                table: "AndroidWorkers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "A" },
                    { 2, "B" },
                    { 3, "C" },
                    { 4, "D" },
                    { 5, "E" },
                    { 6, "F" },
                    { 7, "G" },
                    { 8, "H" },
                    { 9, "I" },
                    { 10, "J" },
                    { 11, "K" },
                    { 12, "L" },
                    { 13, "M" },
                    { 14, "N" },
                    { 15, "O" },
                    { 16, "P" },
                    { 17, "Q" },
                    { 18, "R" },
                    { 19, "S" },
                    { 20, "T" },
                    { 21, "U" },
                    { 22, "V" },
                    { 23, "W" },
                    { 24, "X" },
                    { 25, "Y" },
                    { 26, "Z" }
                });

            migrationBuilder.InsertData(
                table: "ActivitiesWorkers",
                columns: new[] { "ActivityId", "WorkerId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 2, 4 },
                    { 2, 5 },
                    { 2, 6 },
                    { 3, 7 },
                    { 3, 8 },
                    { 3, 9 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ActivitiesWorkers",
                keyColumns: new[] { "ActivityId", "WorkerId" },
                keyValues: new object[] { 1, 1 });

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
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "ActivitiesWorkers",
                keyColumns: new[] { "ActivityId", "WorkerId" },
                keyValues: new object[] { 2, 5 });

            migrationBuilder.DeleteData(
                table: "ActivitiesWorkers",
                keyColumns: new[] { "ActivityId", "WorkerId" },
                keyValues: new object[] { 2, 6 });

            migrationBuilder.DeleteData(
                table: "ActivitiesWorkers",
                keyColumns: new[] { "ActivityId", "WorkerId" },
                keyValues: new object[] { 3, 7 });

            migrationBuilder.DeleteData(
                table: "ActivitiesWorkers",
                keyColumns: new[] { "ActivityId", "WorkerId" },
                keyValues: new object[] { 3, 8 });

            migrationBuilder.DeleteData(
                table: "ActivitiesWorkers",
                keyColumns: new[] { "ActivityId", "WorkerId" },
                keyValues: new object[] { 3, 9 });

            migrationBuilder.DeleteData(
                table: "AndroidWorkers",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AndroidWorkers",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "AndroidWorkers",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "AndroidWorkers",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "AndroidWorkers",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "AndroidWorkers",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "AndroidWorkers",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "AndroidWorkers",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "AndroidWorkers",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "AndroidWorkers",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "AndroidWorkers",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "AndroidWorkers",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "AndroidWorkers",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "AndroidWorkers",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "AndroidWorkers",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "AndroidWorkers",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "AndroidWorkers",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Activities",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AndroidWorkers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AndroidWorkers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AndroidWorkers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AndroidWorkers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AndroidWorkers",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AndroidWorkers",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AndroidWorkers",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AndroidWorkers",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AndroidWorkers",
                keyColumn: "Id",
                keyValue: 9);

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
        }
    }
}
