using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Streetcode.DAL.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddSoftDeleteToComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comments_comments_ParentCommentId",
                schema: "streetcode",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "FK_comments_streetcodes_StreetcodeId",
                schema: "streetcode",
                table: "comments");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                schema: "streetcode",
                table: "comments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "streetcode",
                table: "comments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_comments_comments_ParentCommentId",
                schema: "streetcode",
                table: "comments",
                column: "ParentCommentId",
                principalSchema: "streetcode",
                principalTable: "comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comments_comments_ParentCommentId",
                schema: "streetcode",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "FK_comments_streetcodes_StreetcodeId",
                schema: "streetcode",
                table: "comments");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                schema: "streetcode",
                table: "comments");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "streetcode",
                table: "comments");

            migrationBuilder.AddForeignKey(
                name: "FK_comments_comments_ParentCommentId",
                schema: "streetcode",
                table: "comments",
                column: "ParentCommentId",
                principalSchema: "streetcode",
                principalTable: "comments",
                principalColumn: "Id");

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
    }
}
