using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Synonyms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: false),
                    NormalizedName = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Synonyms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Synonyms_Synonyms_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Synonyms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Synonym_NormalizedName",
                table: "Synonyms",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Synonyms_ParentId",
                table: "Synonyms",
                column: "ParentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Synonyms");
        }
    }
}
