using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quorum.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationUserPool : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Option_Poll_PollId",
                table: "Option");

            migrationBuilder.DropForeignKey(
                name: "FK_Poll_User_OwnerId",
                table: "Poll");

            migrationBuilder.DropForeignKey(
                name: "FK_Vote_Option_OptionId",
                table: "Vote");

            migrationBuilder.DropForeignKey(
                name: "FK_Vote_Poll_PollId",
                table: "Vote");

            migrationBuilder.DropForeignKey(
                name: "FK_Vote_User_VoterId",
                table: "Vote");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Poll",
                table: "Poll");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Option",
                table: "Option");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Poll",
                newName: "Polls");

            migrationBuilder.RenameTable(
                name: "Option",
                newName: "Options");

            migrationBuilder.RenameIndex(
                name: "IX_Poll_OwnerId",
                table: "Polls",
                newName: "IX_Polls_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Option_PollId",
                table: "Options",
                newName: "IX_Options_PollId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Polls",
                table: "Polls",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Options",
                table: "Options",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Options_Polls_PollId",
                table: "Options",
                column: "PollId",
                principalTable: "Polls",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Polls_Users_OwnerId",
                table: "Polls",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_Options_OptionId",
                table: "Vote",
                column: "OptionId",
                principalTable: "Options",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_Polls_PollId",
                table: "Vote",
                column: "PollId",
                principalTable: "Polls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_Users_VoterId",
                table: "Vote",
                column: "VoterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Options_Polls_PollId",
                table: "Options");

            migrationBuilder.DropForeignKey(
                name: "FK_Polls_Users_OwnerId",
                table: "Polls");

            migrationBuilder.DropForeignKey(
                name: "FK_Vote_Options_OptionId",
                table: "Vote");

            migrationBuilder.DropForeignKey(
                name: "FK_Vote_Polls_PollId",
                table: "Vote");

            migrationBuilder.DropForeignKey(
                name: "FK_Vote_Users_VoterId",
                table: "Vote");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Polls",
                table: "Polls");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Options",
                table: "Options");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Polls",
                newName: "Poll");

            migrationBuilder.RenameTable(
                name: "Options",
                newName: "Option");

            migrationBuilder.RenameIndex(
                name: "IX_Polls_OwnerId",
                table: "Poll",
                newName: "IX_Poll_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Options_PollId",
                table: "Option",
                newName: "IX_Option_PollId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Poll",
                table: "Poll",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Option",
                table: "Option",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Option_Poll_PollId",
                table: "Option",
                column: "PollId",
                principalTable: "Poll",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Poll_User_OwnerId",
                table: "Poll",
                column: "OwnerId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_Option_OptionId",
                table: "Vote",
                column: "OptionId",
                principalTable: "Option",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_Poll_PollId",
                table: "Vote",
                column: "PollId",
                principalTable: "Poll",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_User_VoterId",
                table: "Vote",
                column: "VoterId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
