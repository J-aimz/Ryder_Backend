using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ryder.Domain.Migrations
{
    /// <inheritdoc />
    public partial class messages_db_changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MessageThreads_LastMessageId",
                table: "MessageThreads");

            migrationBuilder.DropIndex(
                name: "IX_MessageThreads_PinnedMessageId",
                table: "MessageThreads");

            migrationBuilder.DropColumn(
                name: "LastMessageId",
                table: "MessageThreads");

            migrationBuilder.DropColumn(
                name: "PinnedMessageId",
                table: "MessageThreads");

            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "MessageThreadParticipants");

            migrationBuilder.DropColumn(
                name: "IsPinned",
                table: "MessageThreadParticipants");

            migrationBuilder.DropColumn(
                name: "LastReadTime",
                table: "MessageThreadParticipants");

            migrationBuilder.DropColumn(
                name: "PinnedDate",
                table: "MessageThreadParticipants");

            migrationBuilder.AddColumn<bool>(
                name: "MessageIsRead",
                table: "MessageThreads",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfUnreadMessages",
                table: "MessageThreads",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "RiderId",
                table: "MessageThreadParticipants",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Emojie",
                table: "Messages",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Receiver",
                table: "Messages",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MessageIsRead",
                table: "MessageThreads");

            migrationBuilder.DropColumn(
                name: "NumberOfUnreadMessages",
                table: "MessageThreads");

            migrationBuilder.DropColumn(
                name: "RiderId",
                table: "MessageThreadParticipants");

            migrationBuilder.DropColumn(
                name: "Emojie",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Receiver",
                table: "Messages");

            migrationBuilder.AddColumn<Guid>(
                name: "LastMessageId",
                table: "MessageThreads",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PinnedMessageId",
                table: "MessageThreads",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "MessageThreadParticipants",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPinned",
                table: "MessageThreadParticipants",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastReadTime",
                table: "MessageThreadParticipants",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PinnedDate",
                table: "MessageThreadParticipants",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_MessageThreads_LastMessageId",
                table: "MessageThreads",
                column: "LastMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageThreads_PinnedMessageId",
                table: "MessageThreads",
                column: "PinnedMessageId");
        }
    }
}
