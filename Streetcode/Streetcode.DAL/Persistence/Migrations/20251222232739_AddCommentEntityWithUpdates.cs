using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Streetcode.DAL.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCommentEntityWithUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comments_streetcodes_StreetcodeId",
                schema: "streetcode",
                table: "comments");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "streetcode",
                table: "comments");

            migrationBuilder.AddForeignKey(
                name: "FK_comments_streetcodes_StreetcodeId",
                schema: "streetcode",
                table: "comments",
                column: "StreetcodeId",
                principalSchema: "streetcode",
                principalTable: "streetcodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comments_streetcodes_StreetcodeId",
                schema: "streetcode",
                table: "comments");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "streetcode",
                table: "comments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_comments_streetcodes_StreetcodeId",
                schema: "streetcode",
                table: "comments",
                column: "StreetcodeId",
                principalSchema: "streetcode",
                principalTable: "streetcodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
