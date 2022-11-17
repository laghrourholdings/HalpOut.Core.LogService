using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogService.Migrations
{
    public partial class RenameLogMessagesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ILogMessage_LogHandles_LogHandleId",
                table: "ILogMessage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ILogMessage",
                table: "ILogMessage");

            migrationBuilder.RenameTable(
                name: "ILogMessage",
                newName: "LogMessages");

            migrationBuilder.RenameIndex(
                name: "IX_ILogMessage_LogHandleId",
                table: "LogMessages",
                newName: "IX_LogMessages_LogHandleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LogMessages",
                table: "LogMessages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LogMessages_LogHandles_LogHandleId",
                table: "LogMessages",
                column: "LogHandleId",
                principalTable: "LogHandles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LogMessages_LogHandles_LogHandleId",
                table: "LogMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LogMessages",
                table: "LogMessages");

            migrationBuilder.RenameTable(
                name: "LogMessages",
                newName: "ILogMessage");

            migrationBuilder.RenameIndex(
                name: "IX_LogMessages_LogHandleId",
                table: "ILogMessage",
                newName: "IX_ILogMessage_LogHandleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ILogMessage",
                table: "ILogMessage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ILogMessage_LogHandles_LogHandleId",
                table: "ILogMessage",
                column: "LogHandleId",
                principalTable: "LogHandles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
