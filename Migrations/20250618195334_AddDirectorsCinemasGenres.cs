using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PopCinema.Migrations
{
    /// <inheritdoc />
    public partial class AddDirectorsCinemasGenres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CinemaHalls",
                columns: new[] { "Id", "Name", "Capacity" },
                values: new object[,]
                {
                    { 1, "Grand Hall A", 200 },
                    { 2, "Grand Hall B", 180 },
                    { 3, "Skyline Cinema 1", 150 },
                    { 4, "Skyline Cinema 2", 130 },
                    { 5, "VIP Lounge Theater", 80 },
                    { 6, "Downtown Arena", 250 },
                    { 7, "Sunset Room", 120 },
                    { 8, "Oceanview Screen", 100 }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Action" },
                    { 2, "Adventure" },
                    { 3, "Animation" },
                    { 4, "Biography" },
                    { 5, "Comedy" },
                    { 6, "Crime" },
                    { 7, "Documentary" },
                    { 8, "Drama" },
                    { 9, "Family" },
                    { 10, "Fantasy" },
                    { 11, "History" },
                    { 12, "Horror" },
                    { 13, "Musical" },
                    { 14, "Mystery" },
                    { 15, "Romance" },
                    { 16, "Sci-Fi" },
                    { 17, "Sport" },
                    { 18, "Thriller" },
                    { 19, "War" },
                    { 20, "Western" }
                });

            migrationBuilder.InsertData(
                table: "Directors",
                columns: new[] { "Id", "FullName", "Bio", "PhoneNumber", "PhotoUrl" },
                values: new object[,]
                {
                    { 1, "Christopher Nolan", "British-American filmmaker known for cerebral sci-fi blockbusters.", "1234567890", "https://images.unsplash.com/photo-1603415526960-f7e0328fef67?w=300" },
                    { 2, "Quentin Tarantino", "Known for stylized violence and nonlinear storytelling.", "2345678901", "https://images.unsplash.com/photo-1535713875002-d1d0cf377fde?w=300" },
                    { 3, "Steven Spielberg", "One of the most commercially successful directors in history.", "3456789012", "https://images.unsplash.com/photo-1544723795-3fb6469f5b39?w=300" },
                    { 4, "Martin Scorsese", "Master of gangster films and character studies.", "4567890123", "https://images.unsplash.com/photo-1502767089025-6572583495b0?w=300" },
                    { 5, "Ridley Scott", "Known for Blade Runner, Gladiator, and visual world-building.", "5678901234", "https://images.unsplash.com/photo-1492562080023-ab3db95bfbce?w=300" },
                    { 6, "James Cameron", "Canadian director famous for Titanic and Avatar.", "6789012345", "https://images.unsplash.com/photo-1511367461989-f85a21fda167?w=300" },
                    { 7, "Denis Villeneuve", "Modern visionary behind Dune and Arrival.", "7890123456", "https://images.unsplash.com/photo-1527980965255-d3b416303d12?w=300" },
                    { 8, "Peter Jackson", "Brought Tolkien’s world to life with The Lord of the Rings.", "8901234567", "https://images.unsplash.com/photo-1607746882042-944635dfe10e?w=300" },
                    { 9, "Greta Gerwig", "Acclaimed for Little Women and Barbie.", "9012345678", "https://images.unsplash.com/photo-1502767089025-6572583495b0?w=300" },
                    { 10, "Wes Anderson", "Famous for unique aesthetics and symmetry.", "1122334455", "https://images.unsplash.com/photo-1500648767791-00dcc994a43e?w=300" },
                    { 11, "Guillermo del Toro", "Known for magical realism and creature design.", "9988776655", "https://images.unsplash.com/photo-1500648767791-00dcc994a43e?w=300" },
                    { 12, "Sofia Coppola", "Creates introspective, stylish indie films.", "2211334455", "https://images.unsplash.com/photo-1527980965255-d3b416303d12?w=300" },
                    { 13, "Jordan Peele", "Director of smart horror with social commentary.", "3311224455", "https://images.unsplash.com/photo-1502767089025-6572583495b0?w=300" },
                    { 14, "David Fincher", "Master of psychological thrillers and moody drama.", "4411223344", "https://images.unsplash.com/photo-1492562080023-ab3db95bfbce?w=300" },
                    { 15, "Taika Waititi", "Known for blending humor and heart.", "5511223344", "https://images.unsplash.com/photo-1511367461989-f85a21fda167?w=300" },
                    { 16, "Bong Joon-ho", "Korean director behind Oscar-winning Parasite.", "6611223344", "https://images.unsplash.com/photo-1584999734486-2f3e7f35fc53?w=300" },
                    { 17, "Chloé Zhao", "Oscar-winning director of Nomadland.", "7711223344", "https://images.unsplash.com/photo-1607746882042-944635dfe10e?w=300" },
                    { 18, "Patty Jenkins", "Directed Wonder Woman and other blockbusters.", "8811223344", "https://images.unsplash.com/photo-1603415526960-f7e0328fef67?w=300" },
                    { 19, "Robert Zemeckis", "Director of Forrest Gump and Back to the Future.", "9911223344", "https://images.unsplash.com/photo-1494790108377-be9c29b29330?w=300" },
                    { 20, "Ron Howard", "Award-winning director with decades of hits.", "6611443322", "https://images.unsplash.com/photo-1527980965255-d3b416303d12?w=300" }
                });



        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CinemaHalls",
                keyColumn: "Id",
                keyValues: new object[] { 1, 2, 3, 4, 5, 6, 7, 8 });

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValues: new object[]
                {
                    1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
                    11, 12, 13, 14, 15, 16, 17, 18, 19, 20
                });

            migrationBuilder.DeleteData(
                table: "Directors",
                keyColumn: "Id",
                keyValues: new object[]
                {
                    1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
                    11, 12, 13, 14, 15, 16, 17, 18, 19, 20
                });



        }
    }
}
