using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PopCinema.Migrations
{
    /// <inheritdoc />
    public partial class AddShowTimes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ShowTimes",
                columns: new[] { "Id", "StartTime", "EndTime", "TicketPrice", "MovieId", "CinemaHallId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 6, 20, 14, 0, 0), new DateTime(2025, 6, 20, 16, 0, 0), 80m, 1, 1 },
                    { 2, new DateTime(2025, 6, 20, 17, 0, 0), new DateTime(2025, 6, 20, 19, 0, 0), 85m, 2, 2 },
                    { 3, new DateTime(2025, 6, 21, 12, 0, 0), new DateTime(2025, 6, 21, 14, 0, 0), 75m, 3, 3 },
                    { 4, new DateTime(2025, 6, 21, 15, 0, 0), new DateTime(2025, 6, 21, 17, 0, 0), 90m, 4, 4 },
                    { 5, new DateTime(2025, 6, 22, 18, 0, 0), new DateTime(2025, 6, 22, 20, 0, 0), 100m, 5, 5 },
                    { 6, new DateTime(2025, 6, 22, 20, 30, 0), new DateTime(2025, 6, 22, 22, 30, 0), 95m, 6, 6 },
                    { 7, new DateTime(2025, 6, 23, 13, 0, 0), new DateTime(2025, 6, 23, 15, 0, 0), 70m, 7, 7 },
                    { 8, new DateTime(2025, 6, 23, 16, 0, 0), new DateTime(2025, 6, 23, 18, 0, 0), 85m, 8, 8 },
                    { 9, new DateTime(2025, 6, 24, 14, 0, 0), new DateTime(2025, 6, 24, 16, 0, 0), 80m, 9, 1 },
                    { 10, new DateTime(2025, 6, 24, 19, 0, 0), new DateTime(2025, 6, 24, 21, 0, 0), 100m, 10, 2 },
                    { 11, new DateTime(2025, 6, 25, 12, 0, 0), new DateTime(2025, 6, 25, 14, 0, 0), 75m, 1, 3 },
                    { 12, new DateTime(2025, 6, 25, 15, 0, 0), new DateTime(2025, 6, 25, 17, 0, 0), 90m, 2, 4 },
                    { 13, new DateTime(2025, 6, 26, 18, 0, 0), new DateTime(2025, 6, 26, 20, 0, 0), 100m, 3, 5 },
                    { 14, new DateTime(2025, 6, 26, 20, 30, 0), new DateTime(2025, 6, 26, 22, 30, 0), 95m, 4, 6 },
                    { 15, new DateTime(2025, 6, 27, 13, 0, 0), new DateTime(2025, 6, 27, 15, 0, 0), 70m, 5, 7 },
                    { 16, new DateTime(2025, 6, 27, 16, 0, 0), new DateTime(2025, 6, 27, 18, 0, 0), 85m, 6, 8 },
                    { 17, new DateTime(2025, 6, 28, 14, 0, 0), new DateTime(2025, 6, 28, 16, 0, 0), 80m, 7, 1 },
                    { 18, new DateTime(2025, 6, 28, 19, 0, 0), new DateTime(2025, 6, 28, 21, 0, 0), 100m, 8, 2 },
                    { 19, new DateTime(2025, 6, 29, 12, 0, 0), new DateTime(2025, 6, 29, 14, 0, 0), 75m, 9, 3 },
                    { 20, new DateTime(2025, 6, 29, 15, 0, 0), new DateTime(2025, 6, 29, 17, 0, 0), 90m, 10, 4 }
                });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ShowTimes",
                keyColumn: "Id",
                keyValues: Enumerable.Range(1, 20).Select(id => (object)id).ToArray());

        }
    }
}
