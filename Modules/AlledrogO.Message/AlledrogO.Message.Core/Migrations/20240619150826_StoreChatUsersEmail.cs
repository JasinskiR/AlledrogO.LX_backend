using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlledrogO.Message.Core.Migrations
{
    /// <inheritdoc />
    public partial class StoreChatUsersEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "MessageSchema",
                table: "ChatUsers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                schema: "MessageSchema",
                table: "ChatUsers");
        }
    }
}
