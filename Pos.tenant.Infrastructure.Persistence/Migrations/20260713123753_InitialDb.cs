using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pos.tenant.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SubscriptionPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "varchar(80)", unicode: false, maxLength: 80, nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DescriptionAr = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DescriptionEn = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MonthlyPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrencyCode = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: false, defaultValue: "EGP"),
                    BranchLimit = table.Column<int>(type: "int", nullable: true),
                    ProductLimit = table.Column<int>(type: "int", nullable: true),
                    CashierLimit = table.Column<int>(type: "int", nullable: true),
                    AllowVariants = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    NameEn = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Slug = table.Column<string>(type: "varchar(120)", unicode: false, maxLength: 120, nullable: false),
                    Subdomain = table.Column<string>(type: "varchar(120)", unicode: false, maxLength: 120, nullable: false),
                    BusinessTypeCode = table.Column<string>(type: "varchar(80)", unicode: false, maxLength: 80, nullable: false),
                    Status = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false, defaultValue: "Pending"),
                    CurrencyCode = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: false, defaultValue: "EGP"),
                    InventoryMode = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false, defaultValue: "TrackStock"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TenantSettings",
                columns: table => new
                {
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DefaultVatRate = table.Column<decimal>(type: "decimal(5,2)", nullable: false, defaultValue: 14m),
                    PricesIncludeTax = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ReceiptFooterAr = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ReceiptFooterEn = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AllowReturns = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    DiscountLimitPercent = table.Column<decimal>(type: "decimal(5,2)", nullable: false, defaultValue: 20m),
                    DefaultLanguage = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false, defaultValue: "en")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantSettings", x => x.TenantId);
                    table.ForeignKey(
                        name: "FK_TenantSettings_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TenantStatusHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OldStatus = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    NewStatus = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ChangedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantStatusHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenantStatusHistory_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TenantSubscriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false, defaultValue: "Active"),
                    CurrentPeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CurrentPeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GracePeriodEndsAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantSubscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TenantSubscriptions_SubscriptionPlans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "SubscriptionPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TenantSubscriptions_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TenantUsageCounters",
                columns: table => new
                {
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    ProductCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    CashierCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantUsageCounters", x => x.TenantId);
                    table.ForeignKey(
                        name: "FK_TenantUsageCounters_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionInvoices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantSubscriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "varchar(80)", unicode: false, maxLength: 80, nullable: false),
                    PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false, defaultValue: "Unpaid"),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaidAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionInvoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionInvoices_TenantSubscriptions_TenantSubscriptionId",
                        column: x => x.TenantSubscriptionId,
                        principalTable: "TenantSubscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubscriptionInvoices_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionPayments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Method = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ReferenceNumber = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Status = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false, defaultValue: "Pending"),
                    PaidAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionPayments_SubscriptionInvoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "SubscriptionInvoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubscriptionPayments_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionInvoices_TenantId_DueDate",
                table: "SubscriptionInvoices",
                columns: new[] { "TenantId", "DueDate" });

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionInvoices_TenantId_InvoiceNumber",
                table: "SubscriptionInvoices",
                columns: new[] { "TenantId", "InvoiceNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionInvoices_TenantId_Status",
                table: "SubscriptionInvoices",
                columns: new[] { "TenantId", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionInvoices_TenantSubscriptionId",
                table: "SubscriptionInvoices",
                column: "TenantSubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPayments_InvoiceId",
                table: "SubscriptionPayments",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPayments_TenantId_InvoiceId",
                table: "SubscriptionPayments",
                columns: new[] { "TenantId", "InvoiceId" });

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPayments_TenantId_Status",
                table: "SubscriptionPayments",
                columns: new[] { "TenantId", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPlans_Code",
                table: "SubscriptionPlans",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_Slug",
                table: "Tenants",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_Subdomain",
                table: "Tenants",
                column: "Subdomain",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TenantStatusHistory_TenantId_ChangedAt",
                table: "TenantStatusHistory",
                columns: new[] { "TenantId", "ChangedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_TenantSubscriptions_PlanId",
                table: "TenantSubscriptions",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantSubscriptions_TenantId",
                table: "TenantSubscriptions",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_TenantSubscriptions_TenantId_Status",
                table: "TenantSubscriptions",
                columns: new[] { "TenantId", "Status" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubscriptionPayments");

            migrationBuilder.DropTable(
                name: "TenantSettings");

            migrationBuilder.DropTable(
                name: "TenantStatusHistory");

            migrationBuilder.DropTable(
                name: "TenantUsageCounters");

            migrationBuilder.DropTable(
                name: "SubscriptionInvoices");

            migrationBuilder.DropTable(
                name: "TenantSubscriptions");

            migrationBuilder.DropTable(
                name: "SubscriptionPlans");

            migrationBuilder.DropTable(
                name: "Tenants");
        }
    }
}
