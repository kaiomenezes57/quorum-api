using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quorum.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddVoteGoalToPoll : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VoteGoal",
                table: "Polls",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VoteGoal",
                table: "Polls");
        }
    }
}
