using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogService.Migrations
{
    public partial class ReducedLogMessageData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "LogMessages");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "LogMessages");

            migrationBuilder.DropColumn(
                name: "IsSuspended",
                table: "LogMessages");

            migrationBuilder.DropColumn(
                name: "SuspendedDate",
                table: "LogMessages");

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "LogHandles",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SuspendedBy",
                table: "LogHandles",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "LogHandles");

            migrationBuilder.DropColumn(
                name: "SuspendedBy",
                table: "LogHandles");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedDate",
                table: "LogMessages",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "LogMessages",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSuspended",
                table: "LogMessages",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "SuspendedDate",
                table: "LogMessages",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }
    }
}
