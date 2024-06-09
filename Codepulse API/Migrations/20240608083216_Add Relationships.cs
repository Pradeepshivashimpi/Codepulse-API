using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Codepulse_API.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_Categories_ClategoryId",
                table: "BlogPosts");

            migrationBuilder.DropIndex(
                name: "IX_BlogPosts_ClategoryId",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "ClategoryId",
                table: "BlogPosts");

            migrationBuilder.CreateTable(
                name: "BlogPostClategory",
                columns: table => new
                {
                    BlogPostsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoriesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPostClategory", x => new { x.BlogPostsId, x.CategoriesId });
                    table.ForeignKey(
                        name: "FK_BlogPostClategory_BlogPosts_BlogPostsId",
                        column: x => x.BlogPostsId,
                        principalTable: "BlogPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogPostClategory_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostClategory_CategoriesId",
                table: "BlogPostClategory",
                column: "CategoriesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogPostClategory");

            migrationBuilder.AddColumn<Guid>(
                name: "ClategoryId",
                table: "BlogPosts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_ClategoryId",
                table: "BlogPosts",
                column: "ClategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_Categories_ClategoryId",
                table: "BlogPosts",
                column: "ClategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
