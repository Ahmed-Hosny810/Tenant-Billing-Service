using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pos.tenant.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentProviderFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FailureReason",
                table: "SubscriptionPayments",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdempotencyKey",
                table: "SubscriptionPayments",
                type: "varchar(200)",
                unicode: false,
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Provider",
                table: "SubscriptionPayments",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProviderClientSecret",
                table: "SubscriptionPayments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProviderPaymentReference",
                table: "SubscriptionPayments",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProviderStatus",
                table: "SubscriptionPayments",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProviderTransactionId",
                table: "SubscriptionPayments",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPayments_IdempotencyKey",
                table: "SubscriptionPayments",
                column: "IdempotencyKey",
                unique: true,
                filter: "[IdempotencyKey] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SubscriptionPayments_IdempotencyKey",
                table: "SubscriptionPayments");

            migrationBuilder.DropColumn(
                name: "FailureReason",
                table: "SubscriptionPayments");

            migrationBuilder.DropColumn(
                name: "IdempotencyKey",
                table: "SubscriptionPayments");

            migrationBuilder.DropColumn(
                name: "Provider",
                table: "SubscriptionPayments");

            migrationBuilder.DropColumn(
                name: "ProviderClientSecret",
                table: "SubscriptionPayments");

            migrationBuilder.DropColumn(
                name: "ProviderPaymentReference",
                table: "SubscriptionPayments");

            migrationBuilder.DropColumn(
                name: "ProviderStatus",
                table: "SubscriptionPayments");

            migrationBuilder.DropColumn(
                name: "ProviderTransactionId",
                table: "SubscriptionPayments");
        }
    }
}
