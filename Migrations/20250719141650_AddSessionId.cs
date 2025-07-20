using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PopCinema.Migrations
{
    /// <inheritdoc />
    public partial class AddSessionId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymentID",
                table: "Bookings",
                newName: "PaymentId");

            migrationBuilder.AddColumn<string>(
                name: "SessionId",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "PaymentId",
                table: "Bookings",
                newName: "PaymentID");
        }
    }
}
