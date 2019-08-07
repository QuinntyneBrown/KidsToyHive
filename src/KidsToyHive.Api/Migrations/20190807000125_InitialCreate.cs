using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KidsToyHive.Api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    BrandId = table.Column<Guid>(nullable: false),
                    TenantKey = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.BrandId);
                });

            migrationBuilder.CreateTable(
                name: "CardLayouts",
                columns: table => new
                {
                    CardLayoutId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Version = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardLayouts", x => x.CardLayoutId);
                });

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    CardId = table.Column<Guid>(nullable: false),
                    TenantKey = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.CardId);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<Guid>(nullable: false),
                    TenantKey = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    Address_Street = table.Column<string>(nullable: true),
                    Address_City = table.Column<string>(nullable: true),
                    Address_Province = table.Column<string>(nullable: true),
                    Address_PostalCode = table.Column<string>(nullable: true),
                    Address_Latitude = table.Column<decimal>(nullable: true),
                    Address_Longitude = table.Column<decimal>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "DigitalAssets",
                columns: table => new
                {
                    DigitalAssetId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Bytes = table.Column<byte[]>(nullable: true),
                    ContentType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DigitalAssets", x => x.DigitalAssetId);
                });

            migrationBuilder.CreateTable(
                name: "EmailTemplates",
                columns: table => new
                {
                    EmailTemplateId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplates", x => x.EmailTemplateId);
                });

            migrationBuilder.CreateTable(
                name: "HtmlContents",
                columns: table => new
                {
                    HtmlContentId = table.Column<Guid>(nullable: false),
                    TenantKey = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HtmlContents", x => x.HtmlContentId);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    LocationId = table.Column<Guid>(nullable: false),
                    TenantKey = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    Adddress_Street = table.Column<string>(nullable: true),
                    Adddress_City = table.Column<string>(nullable: true),
                    Adddress_Province = table.Column<string>(nullable: true),
                    Adddress_PostalCode = table.Column<string>(nullable: true),
                    Adddress_Latitude = table.Column<decimal>(nullable: true),
                    Adddress_Longitude = table.Column<decimal>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.LocationId);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    ProductCategoryId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Version = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.ProductCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Version = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "SalesOrders",
                columns: table => new
                {
                    SalesOrderId = table.Column<Guid>(nullable: false),
                    TenantKey = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    Cost = table.Column<float>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrders", x => x.SalesOrderId);
                });

            migrationBuilder.CreateTable(
                name: "Signatures",
                columns: table => new
                {
                    SignatureId = table.Column<Guid>(nullable: false),
                    DigitialAssetId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Version = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Signatures", x => x.SignatureId);
                });

            migrationBuilder.CreateTable(
                name: "Taxes",
                columns: table => new
                {
                    TaxId = table.Column<Guid>(nullable: false),
                    TenantKey = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    Rate = table.Column<decimal>(nullable: false),
                    Type = table.Column<byte>(nullable: false),
                    Effective = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taxes", x => x.TaxId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    TenantKey = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Salt = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Videos",
                columns: table => new
                {
                    VideoId = table.Column<Guid>(nullable: false),
                    TenantKey = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    Category = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    SubTitle = table.Column<string>(nullable: true),
                    Slug = table.Column<string>(nullable: true),
                    YouTubeVideoId = table.Column<string>(nullable: true),
                    Abstract = table.Column<string>(nullable: true),
                    DurationInSeconds = table.Column<int>(nullable: false),
                    Rating = table.Column<decimal>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Published = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.VideoId);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingId = table.Column<Guid>(nullable: false),
                    TenantKey = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    LocationId = table.Column<Guid>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    BookingTimeSlot = table.Column<byte>(nullable: false),
                    Status = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_Bookings_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerLocations",
                columns: table => new
                {
                    CustomerLocationId = table.Column<Guid>(nullable: false),
                    TenantKey = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    LocationId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerLocations", x => x.CustomerLocationId);
                    table.ForeignKey(
                        name: "FK_CustomerLocations_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerLocations_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    DriverId = table.Column<Guid>(nullable: false),
                    TenantKey = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    LocationId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.DriverId);
                    table.ForeignKey(
                        name: "FK_Drivers_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    WarehouseId = table.Column<Guid>(nullable: false),
                    TenantKey = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    LocationId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.WarehouseId);
                    table.ForeignKey(
                        name: "FK_Warehouses_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(nullable: false),
                    TenantKey = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    BrandId = table.Column<Guid>(nullable: true),
                    ProductCategoryId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Price = table.Column<int>(nullable: false),
                    ChargePeriodPrice = table.Column<int>(nullable: false),
                    Type = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "BrandId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_ProductCategories_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "ProductCategories",
                        principalColumn: "ProductCategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalesOrderDetails",
                columns: table => new
                {
                    SalesOrderDetailId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Version = table.Column<int>(nullable: false),
                    SalesOrderId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrderDetails", x => x.SalesOrderDetailId);
                    table.ForeignKey(
                        name: "FK_SalesOrderDetails_SalesOrders_SalesOrderId",
                        column: x => x.SalesOrderId,
                        principalTable: "SalesOrders",
                        principalColumn: "SalesOrderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    ProfileId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    IsDefault = table.Column<bool>(nullable: false),
                    AvatarUrl = table.Column<string>(nullable: true),
                    Type = table.Column<byte>(nullable: false),
                    Version = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.ProfileId);
                    table.ForeignKey(
                        name: "FK_Profiles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shipments",
                columns: table => new
                {
                    ShipmentId = table.Column<Guid>(nullable: false),
                    TenantKey = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    TrackingNumber = table.Column<string>(nullable: true),
                    TotalWeight = table.Column<decimal>(nullable: true),
                    DriverId = table.Column<Guid>(nullable: false),
                    LocationId = table.Column<Guid>(nullable: true),
                    SignatureId = table.Column<Guid>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    Type = table.Column<byte>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipments", x => x.ShipmentId);
                    table.ForeignKey(
                        name: "FK_Shipments_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "DriverId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shipments_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Shipments_Signatures_SignatureId",
                        column: x => x.SignatureId,
                        principalTable: "Signatures",
                        principalColumn: "SignatureId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bins",
                columns: table => new
                {
                    BinId = table.Column<Guid>(nullable: false),
                    TenantKey = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    WarehouseId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bins", x => x.BinId);
                    table.ForeignKey(
                        name: "FK_Bins_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "WarehouseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookingDetails",
                columns: table => new
                {
                    BookingDetailId = table.Column<Guid>(nullable: false),
                    TenantKey = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    LocationId = table.Column<Guid>(nullable: true),
                    Cost = table.Column<int>(nullable: false),
                    BookingId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingDetails", x => x.BookingDetailId);
                    table.ForeignKey(
                        name: "FK_BookingDetails_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "BookingId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingDetails_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImage",
                columns: table => new
                {
                    ProductImageId = table.Column<Guid>(nullable: false),
                    TenantKey = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    DigitalAssetId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImage", x => x.ProductImageId);
                    table.ForeignKey(
                        name: "FK_ProductImage_DigitalAssets_DigitalAssetId",
                        column: x => x.DigitalAssetId,
                        principalTable: "DigitalAssets",
                        principalColumn: "DigitalAssetId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductImage_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dashboards",
                columns: table => new
                {
                    DashboardId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ProfileId = table.Column<Guid>(nullable: false),
                    Options = table.Column<string>(nullable: true),
                    Version = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dashboards", x => x.DashboardId);
                    table.ForeignKey(
                        name: "FK_Dashboards_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "ProfileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShipmentBookings",
                columns: table => new
                {
                    ShipmentBookingId = table.Column<Guid>(nullable: false),
                    BookingId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Version = table.Column<int>(nullable: false),
                    ShipmentId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipmentBookings", x => x.ShipmentBookingId);
                    table.ForeignKey(
                        name: "FK_ShipmentBookings_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "BookingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShipmentBookings_Shipments_ShipmentId",
                        column: x => x.ShipmentId,
                        principalTable: "Shipments",
                        principalColumn: "ShipmentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShipmentItems",
                columns: table => new
                {
                    ShipmentItemId = table.Column<Guid>(nullable: false),
                    TenantKey = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    ShipmentId = table.Column<Guid>(nullable: false),
                    SalesOrderDetailId = table.Column<Guid>(nullable: true),
                    BookingDetailId = table.Column<Guid>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    Comments = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipmentItems", x => x.ShipmentItemId);
                    table.ForeignKey(
                        name: "FK_ShipmentItems_Shipments_ShipmentId",
                        column: x => x.ShipmentId,
                        principalTable: "Shipments",
                        principalColumn: "ShipmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShipmentSalesOrders",
                columns: table => new
                {
                    ShipmentSalesOrderId = table.Column<Guid>(nullable: false),
                    ShipmentId = table.Column<Guid>(nullable: false),
                    SalesOrderId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Version = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipmentSalesOrders", x => x.ShipmentSalesOrderId);
                    table.ForeignKey(
                        name: "FK_ShipmentSalesOrders_Shipments_ShipmentId",
                        column: x => x.ShipmentId,
                        principalTable: "Shipments",
                        principalColumn: "ShipmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryItems",
                columns: table => new
                {
                    InventoryItemId = table.Column<Guid>(nullable: false),
                    TenantKey = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    WarehouseId = table.Column<Guid>(nullable: true),
                    BinId = table.Column<Guid>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryItems", x => x.InventoryItemId);
                    table.ForeignKey(
                        name: "FK_InventoryItems_Bins_BinId",
                        column: x => x.BinId,
                        principalTable: "Bins",
                        principalColumn: "BinId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryItems_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "WarehouseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DashboardCards",
                columns: table => new
                {
                    DashboardCardId = table.Column<Guid>(nullable: false),
                    DashboardId = table.Column<Guid>(nullable: false),
                    Options = table.Column<string>(nullable: true),
                    CardId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Version = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DashboardCards", x => x.DashboardCardId);
                    table.ForeignKey(
                        name: "FK_DashboardCards_Dashboards_DashboardId",
                        column: x => x.DashboardId,
                        principalTable: "Dashboards",
                        principalColumn: "DashboardId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bins_WarehouseId",
                table: "Bins",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetails_BookingId",
                table: "BookingDetails",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetails_LocationId",
                table: "BookingDetails",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetails_ProductId",
                table: "BookingDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_CustomerId",
                table: "Bookings",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_LocationId",
                table: "Bookings",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLocations_CustomerId",
                table: "CustomerLocations",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerLocations_LocationId",
                table: "CustomerLocations",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_DashboardCards_DashboardId",
                table: "DashboardCards",
                column: "DashboardId");

            migrationBuilder.CreateIndex(
                name: "IX_Dashboards_ProfileId",
                table: "Dashboards",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_LocationId",
                table: "Drivers",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItems_BinId",
                table: "InventoryItems",
                column: "BinId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItems_ProductId",
                table: "InventoryItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItems_WarehouseId",
                table: "InventoryItems",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImage_DigitalAssetId",
                table: "ProductImage",
                column: "DigitalAssetId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImage_ProductId",
                table: "ProductImage",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductCategoryId",
                table: "Products",
                column: "ProductCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_UserId",
                table: "Profiles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderDetails_SalesOrderId",
                table: "SalesOrderDetails",
                column: "SalesOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentBookings_BookingId",
                table: "ShipmentBookings",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentBookings_ShipmentId",
                table: "ShipmentBookings",
                column: "ShipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentItems_ShipmentId",
                table: "ShipmentItems",
                column: "ShipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_DriverId",
                table: "Shipments",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_LocationId",
                table: "Shipments",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_SignatureId",
                table: "Shipments",
                column: "SignatureId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentSalesOrders_ShipmentId",
                table: "ShipmentSalesOrders",
                column: "ShipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_LocationId",
                table: "Warehouses",
                column: "LocationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingDetails");

            migrationBuilder.DropTable(
                name: "CardLayouts");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "CustomerLocations");

            migrationBuilder.DropTable(
                name: "DashboardCards");

            migrationBuilder.DropTable(
                name: "EmailTemplates");

            migrationBuilder.DropTable(
                name: "HtmlContents");

            migrationBuilder.DropTable(
                name: "InventoryItems");

            migrationBuilder.DropTable(
                name: "ProductImage");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "SalesOrderDetails");

            migrationBuilder.DropTable(
                name: "ShipmentBookings");

            migrationBuilder.DropTable(
                name: "ShipmentItems");

            migrationBuilder.DropTable(
                name: "ShipmentSalesOrders");

            migrationBuilder.DropTable(
                name: "Taxes");

            migrationBuilder.DropTable(
                name: "Videos");

            migrationBuilder.DropTable(
                name: "Dashboards");

            migrationBuilder.DropTable(
                name: "Bins");

            migrationBuilder.DropTable(
                name: "DigitalAssets");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "SalesOrders");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Shipments");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "ProductCategories");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "Signatures");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
