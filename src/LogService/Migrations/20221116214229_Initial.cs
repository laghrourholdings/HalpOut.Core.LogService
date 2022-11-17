using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogService.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogHandles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ObjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsSuspended = table.Column<bool>(type: "boolean", nullable: false),
                    SuspendedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LogHandleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Descriptor = table.Column<string>(type: "text", nullable: true),
                    ObjectType = table.Column<string>(type: "text", nullable: false),
                    LocationDetails = table.Column<string>(type: "text", nullable: true),
                    AuthorizationDetails = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogHandles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ILogMessage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsSuspended = table.Column<bool>(type: "boolean", nullable: false),
                    SuspendedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LogHandleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Descriptor = table.Column<string>(type: "text", nullable: true),
                    Message = table.Column<string>(type: "text", nullable: false),
                    Severity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ILogMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ILogMessage_LogHandles_LogHandleId",
                        column: x => x.LogHandleId,
                        principalTable: "LogHandles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ILogMessage_LogHandleId",
                table: "ILogMessage",
                column: "LogHandleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ILogMessage");

            migrationBuilder.DropTable(
                name: "LogHandles");
        }
    }
}
