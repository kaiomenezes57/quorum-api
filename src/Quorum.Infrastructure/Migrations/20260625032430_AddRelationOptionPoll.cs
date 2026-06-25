using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quorum.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationOptionPoll : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Options_Polls_PollId",
                table: "Options");

            migrationBuilder.AlterColumn<Guid>(
                name: "PollId",
                table: "Options",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Options_Polls_PollId",
                table: "Options",
                column: "PollId",
                principalTable: "Polls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Options_Polls_PollId",
                table: "Options");

            migrationBuilder.AlterColumn<Guid>(
                name: "PollId",
                table: "Options",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Options_Polls_PollId",
                table: "Options",
                column: "PollId",
                principalTable: "Polls",
                principalColumn: "Id");
        }
    }
}
