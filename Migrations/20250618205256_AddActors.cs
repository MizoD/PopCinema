using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PopCinema.Migrations
{
    /// <inheritdoc />
    public partial class AddActors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Actors",
                columns: new[] { "Id", "FullName", "Bio", "PhoneNumber", "PhotoUrl" },
                values: new object[,]
                {
                    { 1, "Leonardo DiCaprio", "Oscar-winning actor known for Titanic and Inception.", "1001001000", "https://images.unsplash.com/photo-1502767089025-6572583495b0?w=300" },
                    { 2, "Scarlett Johansson", "Black Widow and acclaimed dramatic actress.", "1001001001", "https://images.unsplash.com/photo-1535713875002-d1d0cf377fde?w=300" },
                    { 3, "Tom Hanks", "Beloved for Forrest Gump and Cast Away.", "1001001002", "https://images.unsplash.com/photo-1544723795-3fb6469f5b39?w=300" },
                    { 4, "Natalie Portman", "Star of Black Swan and Thor.", "1001001003", "https://images.unsplash.com/photo-1494790108377-be9c29b29330?w=300" },
                    { 5, "Robert Downey Jr.", "Iconic Iron Man from the Marvel universe.", "1001001004", "https://images.unsplash.com/photo-1603415526960-f7e0328fef67?w=300" },
                    { 6, "Jennifer Lawrence", "Known for Hunger Games and Silver Linings Playbook.", "1001001005", "https://images.unsplash.com/photo-1607746882042-944635dfe10e?w=300"},
                    { 7, "Brad Pitt", "Famous for Fight Club and Once Upon a Time in Hollywood.", "1001001006", "https://images.unsplash.com/photo-1492562080023-ab3db95bfbce?w=300" },
                    { 8, "Emma Stone", "Oscar-winner for La La Land.", "1001001007", "https://images.unsplash.com/photo-1511367461989-f85a21fda167?w=300" },
                    { 9, "Chris Hemsworth", "Best known as Thor in Marvel films.", "1001001008", "https://images.unsplash.com/photo-1584999734486-2f3e7f35fc53?w=300" },
                    { 10, "Gal Gadot", "Portrayed Wonder Woman in the DC Universe.", "1001001009", "https://images.unsplash.com/photo-1527980965255-d3b416303d12?w=300"},
                    { 11, "Christian Bale", "Renowned for Batman and dramatic transformations.", "1001001010", "https://images.unsplash.com/photo-1500648767791-00dcc994a43e?w=300"},
                    { 12, "Zendaya", "Rising star from Euphoria and Spider-Man.", "1001001011", "https://images.unsplash.com/photo-1502767089025-6572583495b0?w=300"},
                    { 13, "Matt Damon", "Bourne series and The Martian.", "1001001012", "https://images.unsplash.com/photo-1502767089025-6572583495b0?w=300" },
                    { 14, "Anne Hathaway", "Princess Diaries to Les Misérables.", "1001001013", "https://images.unsplash.com/photo-1494790108377-be9c29b29330?w=300"},
                    { 15, "Denzel Washington", "Two-time Oscar winner for dramatic roles.", "1001001014", "https://images.unsplash.com/photo-1535713875002-d1d0cf377fde?w=300"},
                    { 16, "Amy Adams", "Acclaimed for Arrival and American Hustle.", "1001001015", "https://images.unsplash.com/photo-1544723795-3fb6469f5b39?w=300" },
                    { 17, "Hugh Jackman", "Famous for Wolverine and The Greatest Showman.", "1001001016", "https://images.unsplash.com/photo-1584999734486-2f3e7f35fc53?w=300" },
                    { 18, "Meryl Streep", "Record-setting Oscar nominee.", "1001001017", "https://images.unsplash.com/photo-1607746882042-944635dfe10e?w=300" },
                    { 19, "Ryan Gosling", "Known for Drive, Barbie, and La La Land.", "1001001018", "https://images.unsplash.com/photo-1511367461989-f85a21fda167?w=300" },
                    { 20, "Rachel McAdams", "Star of The Notebook and Spotlight.", "1001001019", "https://images.unsplash.com/photo-1492562080023-ab3db95bfbce?w=300" },
                    { 21, "Idris Elba", "Star of Luther and The Wire.", "1001001020", "https://images.unsplash.com/photo-1494790108377-be9c29b29330?w=300" },
                    { 22, "Emily Blunt", "Edge of Tomorrow and A Quiet Place.", "1001001021", "https://images.unsplash.com/photo-1502767089025-6572583495b0?w=300" },
                    { 23, "Joseph Gordon-Levitt", "Inception and 500 Days of Summer.", "1001001022", "https://images.unsplash.com/photo-1544723795-3fb6469f5b39?w=300" },
                    { 24, "Kate Winslet", "Titanic and The Reader.", "1001001023", "https://images.unsplash.com/photo-1527980965255-d3b416303d12?w=300" },
                    { 25, "Anthony Hopkins", "Legendary performance in The Silence of the Lambs.", "1001001024", "https://images.unsplash.com/photo-1492562080023-ab3db95bfbce?w=300" }
                });

            migrationBuilder.InsertData(
                table: "ActorMovie",
                columns: new[] { "ActorId", "MovieId" },
                values: new object[,]
                {
                    { 1, 1 }, { 2, 1 }, { 3, 1 }, { 4, 1 }, { 5, 1 },
                    { 6, 2 }, { 7, 2 }, { 8, 2 }, { 9, 2 }, { 10, 2 },
                    { 11, 3 }, { 12, 3 }, { 13, 3 }, { 14, 3 }, { 15, 3 },
                    { 16, 4 }, { 17, 4 }, { 18, 4 }, { 19, 4 }, { 20, 4 },
                    { 21, 5 }, { 22, 5 }, { 23, 5 }, { 24, 5 }, { 25, 5 },
                    { 1, 6 }, { 3, 6 }, { 5, 6 }, { 7, 6 }, { 9, 6 },
                    { 2, 7 }, { 4, 7 }, { 6, 7 }, { 8, 7 }, { 10, 7 },
                    { 11, 8 }, { 13, 8 }, { 15, 8 }, { 17, 8 }, { 19, 8 },
                    { 12, 9 }, { 14, 9 }, { 16, 9 }, { 18, 9 }, { 20, 9 },
                    { 21, 10 }, { 22, 10 }, { 23, 10 }, { 24, 10 }, { 25, 10 }
                });


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ShowTimes",
                keyColumn: "Id",
                keyValues: Enumerable.Range(1, 25).Select(id => (object)id).ToArray());

            for (int actorId = 1; actorId <= 25; actorId++)
            {
                for (int movieId = 1; movieId <= 10; movieId++)
                {
                    // Only remove the exact combinations that were added
                    if ((actorId <= 5 && movieId == 1) ||
                        (actorId >= 6 && actorId <= 10 && movieId == 2) ||
                        (actorId >= 11 && actorId <= 15 && movieId == 3) ||
                        (actorId >= 16 && actorId <= 20 && movieId == 4) ||
                        (actorId >= 21 && actorId <= 25 && movieId == 5) ||
                        ((actorId == 1 || actorId == 3 || actorId == 5 || actorId == 7 || actorId == 9) && movieId == 6) ||
                        ((actorId == 2 || actorId == 4 || actorId == 6 || actorId == 8 || actorId == 10) && movieId == 7) ||
                        ((actorId == 11 || actorId == 13 || actorId == 15 || actorId == 17 || actorId == 19) && movieId == 8) ||
                        ((actorId == 12 || actorId == 14 || actorId == 16 || actorId == 18 || actorId == 20) && movieId == 9) ||
                        ((actorId >= 21 && actorId <= 25) && movieId == 10))
                    {
                        migrationBuilder.DeleteData(
                            table: "ActorMovie",
                            keyColumns: new[] { "ActorId", "MovieId" },
                            keyValues: new object[] { actorId, movieId });
                    }
                }
            }

        }
    }
}
