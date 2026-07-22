using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quorum.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPrediction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Prediction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false),
                    PollId = table.Column<Guid>(type: "char(36)", nullable: false),
                    OptionId = table.Column<Guid>(type: "char(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prediction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prediction_Option_OptionId",
                        column: x => x.OptionId,
                        principalTable: "Option",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prediction_Polls_PollId",
                        column: x => x.PollId,
                        principalTable: "Polls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prediction_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Prediction_OptionId",
                table: "Prediction",
                column: "OptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Prediction_PollId",
                table: "Prediction",
                column: "PollId");

            migrationBuilder.CreateIndex(
                name: "IX_Prediction_UserId",
                table: "Prediction",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prediction");
        }
    }
}
