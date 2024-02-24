using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gesd.Data.Migrations
{
    public partial class Adding_KeystoreTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KeyStores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GeneratedKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EncryptedUrlId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyStores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KeyStores_EncryptedUrlFiles_EncryptedUrlId",
                        column: x => x.EncryptedUrlId,
                        principalTable: "EncryptedUrlFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KeyStores_EncryptedUrlId",
                table: "KeyStores",
                column: "EncryptedUrlId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KeyStores");
        }
    }
}
