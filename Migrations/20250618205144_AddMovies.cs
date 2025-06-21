using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PopCinema.Migrations
{
    /// <inheritdoc />
    public partial class AddMovies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[]
                {
                    "Id", "Title", "Description", "DurationMinutes", "TrailerUrl", "PosterUrl",
                    "ReleaseDate", "Language", "Traffic", "DirectorId", "GenreId", "CinemaHallId"
                },
                values: new object[,]
                {
                    { 1, "Eclipse of Time", "A thrilling sci-fi journey through space-time.", 120, "https://youtu.be/trailer1", "https://images.unsplash.com/photo-1581905764498-3a7e8d3e8e60?w=500&h=750&fit=crop", new DateTime(2024, 5, 15), "English", 1000, 1, 1, 1 },
                    { 2, "Shadow Alley", "Crime drama in the dark alleys of Chicago.", 110, "https://youtu.be/trailer2", "https://images.unsplash.com/photo-1502139214984-6d84ad3c9b94?w=500&h=750&fit=crop", new DateTime(2023, 11, 10), "English", 850, 2, 2, 1 },
                    { 3, "Dreamcatcher", "A fantasy story of a girl who enters dreams.", 95, "https://youtu.be/trailer3", "https://images.unsplash.com/photo-1524985069026-dd778a71c7b4?w=500&h=750&fit=crop", new DateTime(2024, 2, 20), "French", 700, 1, 3, 2 },
                    { 4, "Final Verdict", "Courtroom drama based on a true story.", 130, "https://youtu.be/trailer4", "https://images.unsplash.com/photo-1581093588401-84e8ed5d942d?w=500&h=750&fit=crop", new DateTime(2024, 8, 1), "English", 950, 3, 4, 2 },
                    { 5, "Silent Pulse", "An action-packed thriller with no dialogue.", 100, "https://youtu.be/trailer5", "https://images.unsplash.com/photo-1504384308090-c894fdcc538d?w=500&h=750&fit=crop", new DateTime(2025, 1, 5), "None", 620, 2, 1, 3 },
                    { 6, "Beyond the Horizon", "An inspiring space adventure.", 140, "https://youtu.be/trailer6", "https://images.unsplash.com/photo-1500530855697-b586d89ba3ee?w=500&h=750&fit=crop", new DateTime(2023, 9, 23), "English", 1100, 1, 2, 3 },
                    { 7, "The Forgotten Tune", "Musical mystery wrapped in emotion.", 105, "https://youtu.be/trailer7", "https://images.unsplash.com/photo-1507525428034-b723cf961d3e?w=500&h=750&fit=crop", new DateTime(2022, 12, 10), "Spanish", 510, 3, 3, 1 },
                    { 8, "Virtual Rebirth", "When AI becomes self-aware in a digital afterlife.", 115, "https://youtu.be/trailer8", "https://images.unsplash.com/photo-1558980664-10e7170a8a66?w=500&h=750&fit=crop", new DateTime(2024, 6, 5), "English", 870, 2, 4, 2 },
                    { 9, "Crimson Tide", "A psychological thriller on a sinking submarine.", 125, "https://youtu.be/trailer9", "https://images.unsplash.com/photo-1610057099483-7f94995c9b4b?w=500&h=750&fit=crop", new DateTime(2023, 7, 7), "English", 990, 1, 1, 3 },
                    { 10, "City of Echoes", "Voices of the past haunt a modern city.", 90, "https://youtu.be/trailer10", "https://images.unsplash.com/photo-1497032628192-86f99bcd76bc?w=500&h=750&fit=crop", new DateTime(2025, 3, 3), "German", 760, 3, 2, 1 }
                });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValues: new object[]
                {
                    1, 2, 3, 4, 5, 6, 7, 8, 9, 10
                });

        }
    }
}
