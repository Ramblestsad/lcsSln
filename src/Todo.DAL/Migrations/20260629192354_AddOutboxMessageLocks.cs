using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Todo.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddOutboxMessageLocks: Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockedUntilUtc",
                table: "order_outbox_messages",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockedUntilUtc",
                table: "inventory_outbox_messages",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_order_outbox_messages_PublishedOnUtc_LockedUntilUtc_Id",
                table: "order_outbox_messages",
                columns: new[] { "PublishedOnUtc", "LockedUntilUtc", "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_inventory_outbox_messages_PublishedOnUtc_LockedUntilUtc_Id",
                table: "inventory_outbox_messages",
                columns: new[] { "PublishedOnUtc", "LockedUntilUtc", "Id" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_order_outbox_messages_PublishedOnUtc_LockedUntilUtc_Id",
                table: "order_outbox_messages");

            migrationBuilder.DropIndex(
                name: "IX_inventory_outbox_messages_PublishedOnUtc_LockedUntilUtc_Id",
                table: "inventory_outbox_messages");

            migrationBuilder.DropColumn(
                name: "LockedUntilUtc",
                table: "order_outbox_messages");

            migrationBuilder.DropColumn(
                name: "LockedUntilUtc",
                table: "inventory_outbox_messages");
        }
    }
}
