using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class ProductCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "Products",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BaseEntityId",
                table: "ProductImages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ProductImages",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "ProductImages",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ProductImages",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "ProductImages",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                table: "ProductImages",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "ProductImages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ProductCategories",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "ProductCategories",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BaseEntityId",
                table: "Dashboards",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Dashboards",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Dashboards",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Dashboards",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Dashboards",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                table: "Dashboards",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "Dashboards",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BaseEntityId",
                table: "DashboardCards",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CardId",
                table: "DashboardCards",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "DashboardCards",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "DashboardCards",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DashboardId",
                table: "DashboardCards",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "DashboardCards",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "DashboardCards",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                table: "DashboardCards",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "DashboardCards",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BaseEntityId",
                table: "Contents",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Contents",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Contents",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Contents",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Contents",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                table: "Contents",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "Contents",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BaseEntityId",
                table: "ContactRequests",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ContactRequests",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "ContactRequests",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ContactRequests",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "ContactRequests",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                table: "ContactRequests",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "ContactRequests",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BaseEntityId",
                table: "Cards",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Cards",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Cards",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Cards",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Cards",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Cards",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                table: "Cards",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "Cards",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    BaseEntityId = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    LastModifiedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    AccountId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_Accounts_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "TenantId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    BaseEntityId = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    LastModifiedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    BrandId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.BrandId);
                    table.ForeignKey(
                        name: "FK_Brands_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "TenantId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    BaseEntityId = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    LastModifiedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    TenantId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ServiceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.ServiceId);
                    table.ForeignKey(
                        name: "FK_Service_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "TenantId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_TenantId",
                table: "ProductImages",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Dashboards_TenantId",
                table: "Dashboards",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_DashboardCards_CardId",
                table: "DashboardCards",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_DashboardCards_DashboardId",
                table: "DashboardCards",
                column: "DashboardId");

            migrationBuilder.CreateIndex(
                name: "IX_DashboardCards_TenantId",
                table: "DashboardCards",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Contents_TenantId",
                table: "Contents",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactRequests_TenantId",
                table: "ContactRequests",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_TenantId",
                table: "Cards",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_TenantId",
                table: "Accounts",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_TenantId",
                table: "Brands",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Service_TenantId",
                table: "Service",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Tenants_TenantId",
                table: "Cards",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "TenantId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactRequests_Tenants_TenantId",
                table: "ContactRequests",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "TenantId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_Tenants_TenantId",
                table: "Contents",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "TenantId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DashboardCards_Cards_CardId",
                table: "DashboardCards",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "CardId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DashboardCards_Dashboards_DashboardId",
                table: "DashboardCards",
                column: "DashboardId",
                principalTable: "Dashboards",
                principalColumn: "DashboardId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DashboardCards_Tenants_TenantId",
                table: "DashboardCards",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "TenantId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Dashboards_Tenants_TenantId",
                table: "Dashboards",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "TenantId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Tenants_TenantId",
                table: "ProductImages",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "TenantId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "BrandId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Tenants_TenantId",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactRequests_Tenants_TenantId",
                table: "ContactRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Contents_Tenants_TenantId",
                table: "Contents");

            migrationBuilder.DropForeignKey(
                name: "FK_DashboardCards_Cards_CardId",
                table: "DashboardCards");

            migrationBuilder.DropForeignKey(
                name: "FK_DashboardCards_Dashboards_DashboardId",
                table: "DashboardCards");

            migrationBuilder.DropForeignKey(
                name: "FK_DashboardCards_Tenants_TenantId",
                table: "DashboardCards");

            migrationBuilder.DropForeignKey(
                name: "FK_Dashboards_Tenants_TenantId",
                table: "Dashboards");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Tenants_TenantId",
                table: "ProductImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brands_BrandId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropIndex(
                name: "IX_Products_BrandId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_ProductImages_TenantId",
                table: "ProductImages");

            migrationBuilder.DropIndex(
                name: "IX_Dashboards_TenantId",
                table: "Dashboards");

            migrationBuilder.DropIndex(
                name: "IX_DashboardCards_CardId",
                table: "DashboardCards");

            migrationBuilder.DropIndex(
                name: "IX_DashboardCards_DashboardId",
                table: "DashboardCards");

            migrationBuilder.DropIndex(
                name: "IX_DashboardCards_TenantId",
                table: "DashboardCards");

            migrationBuilder.DropIndex(
                name: "IX_Contents_TenantId",
                table: "Contents");

            migrationBuilder.DropIndex(
                name: "IX_ContactRequests_TenantId",
                table: "ContactRequests");

            migrationBuilder.DropIndex(
                name: "IX_Cards_TenantId",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "BaseEntityId",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ProductCategories");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "ProductCategories");

            migrationBuilder.DropColumn(
                name: "BaseEntityId",
                table: "Dashboards");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Dashboards");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Dashboards");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Dashboards");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Dashboards");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                table: "Dashboards");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Dashboards");

            migrationBuilder.DropColumn(
                name: "BaseEntityId",
                table: "DashboardCards");

            migrationBuilder.DropColumn(
                name: "CardId",
                table: "DashboardCards");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "DashboardCards");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "DashboardCards");

            migrationBuilder.DropColumn(
                name: "DashboardId",
                table: "DashboardCards");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "DashboardCards");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "DashboardCards");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                table: "DashboardCards");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "DashboardCards");

            migrationBuilder.DropColumn(
                name: "BaseEntityId",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "BaseEntityId",
                table: "ContactRequests");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ContactRequests");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "ContactRequests");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ContactRequests");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "ContactRequests");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                table: "ContactRequests");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "ContactRequests");

            migrationBuilder.DropColumn(
                name: "BaseEntityId",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Cards");
        }
    }
}
