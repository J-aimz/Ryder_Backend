using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ryder.Domain.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageThreadParticipants_MessageThreads_MessageThreadId",
                table: "MessageThreadParticipants");

            migrationBuilder.DropIndex(
                name: "IX_MessageThreadParticipants_MessageThreadId",
                table: "MessageThreadParticipants");

            migrationBuilder.DropIndex(
                name: "IX_Messages_MessageThreadId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Receiver",
                table: "Messages");

            migrationBuilder.AddColumn<Guid>(
                name: "MessageThreadParticipantsId",
                table: "MessageThreads",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrdersId",
                table: "MessageThreads",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "RiderId",
                table: "MessageThreadParticipants",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<string>(
                name: "SenderId",
                table: "Messages",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<string>(
                name: "ReceiverId",
                table: "Messages",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_MessageThreads_MessageThreadParticipantsId",
                table: "MessageThreads",
                column: "MessageThreadParticipantsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MessageThreads_OrdersId",
                table: "MessageThreads",
                column: "OrdersId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_MessageThreadId",
                table: "Messages",
                column: "MessageThreadId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_MessageThreads_MessageThreadId",
                table: "Messages",
                column: "MessageThreadId",
                principalTable: "MessageThreads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MessageThreads_MessageThreadParticipants_MessageThreadParti~",
                table: "MessageThreads",
                column: "MessageThreadParticipantsId",
                principalTable: "MessageThreadParticipants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageThreads_Orders_OrdersId",
                table: "MessageThreads",
                column: "OrdersId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_MessageThreads_MessageThreadId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageThreads_MessageThreadParticipants_MessageThreadParti~",
                table: "MessageThreads");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageThreads_Orders_OrdersId",
                table: "MessageThreads");

            migrationBuilder.DropIndex(
                name: "IX_MessageThreads_MessageThreadParticipantsId",
                table: "MessageThreads");

            migrationBuilder.DropIndex(
                name: "IX_MessageThreads_OrdersId",
                table: "MessageThreads");

            migrationBuilder.DropIndex(
                name: "IX_Messages_MessageThreadId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "MessageThreadParticipantsId",
                table: "MessageThreads");

            migrationBuilder.DropColumn(
                name: "OrdersId",
                table: "MessageThreads");

            migrationBuilder.DropColumn(
                name: "ReceiverId",
                table: "Messages");

            migrationBuilder.AlterColumn<Guid>(
                name: "RiderId",
                table: "MessageThreadParticipants",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "SenderId",
                table: "Messages",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<Guid>(
                name: "Receiver",
                table: "Messages",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_MessageThreadParticipants_MessageThreadId",
                table: "MessageThreadParticipants",
                column: "MessageThreadId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_MessageThreadId",
                table: "Messages",
                column: "MessageThreadId");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageThreadParticipants_MessageThreads_MessageThreadId",
                table: "MessageThreadParticipants",
                column: "MessageThreadId",
                principalTable: "MessageThreads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
