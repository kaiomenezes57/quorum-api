using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quorum.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPredictionResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Result",
                table: "Prediction",
                type: "longtext",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Result",
                table: "Prediction");
        }
    }
}
