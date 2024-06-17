using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlledrogO.Message.Core.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "MessageSchema");

            migrationBuilder.CreateTable(
                name: "ChatUsers",
                schema: "MessageSchema",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Chats",
                schema: "MessageSchema",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BuyerId = table.Column<Guid>(type: "uuid", nullable: false),
                    AdvertiserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Messages = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.Id);
                    table.UniqueConstraint("AK_Chats_BuyerId_AdvertiserId", x => new { x.BuyerId, x.AdvertiserId });
                    table.ForeignKey(
                        name: "FK_Chats_ChatUsers_AdvertiserId",
                        column: x => x.AdvertiserId,
                        principalSchema: "MessageSchema",
                        principalTable: "ChatUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Chats_ChatUsers_BuyerId",
                        column: x => x.BuyerId,
                        principalSchema: "MessageSchema",
                        principalTable: "ChatUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chats_AdvertiserId",
                schema: "MessageSchema",
                table: "Chats",
                column: "AdvertiserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chats",
                schema: "MessageSchema");

            migrationBuilder.DropTable(
                name: "ChatUsers",
                schema: "MessageSchema");
        }
    }
}
