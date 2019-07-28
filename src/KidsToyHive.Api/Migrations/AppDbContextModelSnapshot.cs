﻿// <auto-generated />
using System;
using KidsToyHive.Domain.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KidsToyHive.Api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0-preview5.19227.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("KidsToyHive.Domain.Models.Bin", b =>
                {
                    b.Property<Guid>("BinId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("Name");

                    b.Property<Guid>("TenantKey");

                    b.Property<int>("Version");

                    b.Property<Guid>("WarehouseId");

                    b.HasKey("BinId");

                    b.HasIndex("WarehouseId");

                    b.ToTable("Bins");
                });

            modelBuilder.Entity("KidsToyHive.Domain.Models.Booking", b =>
                {
                    b.Property<Guid>("BookingId")
                        .ValueGeneratedOnAdd();

                    b.Property<byte>("BookingTimeSlot");

                    b.Property<DateTime>("Created");

                    b.Property<Guid>("CustomerId");

                    b.Property<DateTime>("Date");

                    b.Property<DateTime>("EndTime");

                    b.Property<Guid?>("LocationId");

                    b.Property<string>("Name");

                    b.Property<DateTime>("StartTime");

                    b.Property<byte>("Status");

                    b.Property<Guid>("TenantKey");

                    b.Property<int>("Version");

                    b.HasKey("BookingId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("LocationId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("KidsToyHive.Domain.Models.BookingDetail", b =>
                {
                    b.Property<Guid>("BookingDetailId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("BookingId");

                    b.Property<int>("Cost");

                    b.Property<DateTime>("Created");

                    b.Property<Guid>("ProductId");

                    b.Property<int>("Quantity");

                    b.Property<Guid>("TenantKey");

                    b.Property<int>("Version");

                    b.HasKey("BookingDetailId");

                    b.HasIndex("BookingId");

                    b.HasIndex("ProductId");

                    b.ToTable("BookingDetails");
                });

            modelBuilder.Entity("KidsToyHive.Domain.Models.Brand", b =>
                {
                    b.Property<Guid>("BrandId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("ImageUrl");

                    b.Property<string>("Name");

                    b.Property<Guid>("TenantKey");

                    b.Property<int>("Version");

                    b.HasKey("BrandId");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("KidsToyHive.Domain.Models.Card", b =>
                {
                    b.Property<Guid>("CardId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<int>("Version");

                    b.HasKey("CardId");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("KidsToyHive.Domain.Models.CardLayout", b =>
                {
                    b.Property<Guid>("CardLayoutId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<int>("Version");

                    b.HasKey("CardLayoutId");

                    b.ToTable("CardLayouts");
                });

            modelBuilder.Entity("KidsToyHive.Domain.Models.Customer", b =>
                {
                    b.Property<Guid>("CustomerId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("PhoneNumber");

                    b.Property<Guid>("TenantKey");

                    b.Property<int>("Version");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("KidsToyHive.Domain.Models.CustomerLocation", b =>
                {
                    b.Property<Guid>("CustomerLocationId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<Guid>("CustomerId");

                    b.Property<Guid?>("LocationId");

                    b.Property<string>("Name");

                    b.Property<Guid>("TenantKey");

                    b.Property<int>("Version");

                    b.HasKey("CustomerLocationId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("LocationId");

                    b.ToTable("CustomerLocations");
                });

            modelBuilder.Entity("KidsToyHive.Domain.Models.Dashboard", b =>
                {
                    b.Property<Guid>("DashboardId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Options");

                    b.Property<Guid>("ProfileId");

                    b.Property<int>("Version");

                    b.HasKey("DashboardId");

                    b.HasIndex("ProfileId");

                    b.ToTable("Dashboards");
                });

            modelBuilder.Entity("KidsToyHive.Domain.Models.DashboardCard", b =>
                {
                    b.Property<Guid>("DashboardCardId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CardId");

                    b.Property<Guid>("DashboardId");

                    b.Property<string>("Name");

                    b.Property<string>("Options");

                    b.Property<int>("Version");

                    b.HasKey("DashboardCardId");

                    b.HasIndex("DashboardId");

                    b.ToTable("DashboardCards");
                });

            modelBuilder.Entity("KidsToyHive.Domain.Models.DigitalAsset", b =>
                {
                    b.Property<Guid>("DigitalAssetId")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("Bytes");

                    b.Property<string>("ContentType");

                    b.Property<string>("Name");

                    b.HasKey("DigitalAssetId");

                    b.ToTable("DigitalAssets");
                });

            modelBuilder.Entity("KidsToyHive.Domain.Models.Driver", b =>
                {
                    b.Property<Guid>("DriverId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<Guid?>("LocationId");

                    b.Property<string>("PhoneNumber");

                    b.Property<Guid>("TenantKey");

                    b.Property<int>("Version");

                    b.HasKey("DriverId");

                    b.HasIndex("LocationId");

                    b.ToTable("Drivers");
                });

            modelBuilder.Entity("KidsToyHive.Domain.Models.HtmlContent", b =>
                {
                    b.Property<Guid>("HtmlContentId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("Name");

                    b.Property<Guid>("TenantKey");

                    b.Property<string>("Value");

                    b.Property<int>("Version");

                    b.HasKey("HtmlContentId");

                    b.ToTable("HtmlContents");
                });

            modelBuilder.Entity("KidsToyHive.Domain.Models.InventoryItem", b =>
                {
                    b.Property<Guid>("InventoryItemId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("BinId");

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("Modified");

                    b.Property<Guid>("ProductId");

                    b.Property<int>("Quantity");

                    b.Property<Guid>("TenantKey");

                    b.Property<int>("Version");

                    b.Property<Guid?>("WarehouseId");

                    b.HasKey("InventoryItemId");

                    b.HasIndex("BinId");

                    b.HasIndex("ProductId");

                    b.HasIndex("WarehouseId");

                    b.ToTable("InventoryItems");
                });

            modelBuilder.Entity("KidsToyHive.Domain.Models.Location", b =>
                {
                    b.Property<Guid>("LocationId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("Description");

                    b.Property<Guid>("TenantKey");

                    b.Property<int>("Type");

                    b.Property<int>("Version");

                    b.HasKey("LocationId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("KidsToyHive.Domain.Models.Product", b =>
                {
                    b.Property<Guid>("ProductId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("BrandId");

                    b.Property<DateTime>("Created");

                    b.Property<string>("Description");

                    b.Property<bool>("IsRental");

                    b.Property<string>("Name");

                    b.Property<int>("Price");

                    b.Property<Guid?>("ProductCategoryId");

                    b.Property<int>("RentalPrice");

                    b.Property<Guid>("TenantKey");

                    b.Property<int>("Version");

                    b.HasKey("ProductId");

                    b.HasIndex("BrandId");

                    b.HasIndex("ProductCategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("KidsToyHive.Domain.Models.ProductCategory", b =>
                {
                    b.Property<Guid>("ProductCategoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("Version");

                    b.HasKey("ProductCategoryId");

                    b.ToTable("ProductCategories");
                });

            modelBuilder.Entity("KidsToyHive.Domain.Models.ProductImage", b =>
                {
                    b.Property<Guid>("ProductImageId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<Guid>("DigitalAssetId");

                    b.Property<Guid>("ProductId");

                    b.Property<Guid>("TenantKey");

                    b.Property<int>("Version");

                    b.HasKey("ProductImageId");

                    b.HasIndex("DigitalAssetId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductImage");
                });

            modelBuilder.Entity("KidsToyHive.Domain.Models.Profile", b =>
                {
                    b.Property<Guid>("ProfileId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AvatarUrl");

                    b.Property<bool>("IsDefault");

                    b.Property<string>("Name");

                    b.Property<byte>("Type");

                    b.Property<Guid>("UserId");

                    b.Property<int>("Version");

                    b.HasKey("ProfileId");

                    b.HasIndex("UserId");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("KidsToyHive.Domain.Models.Role", b =>
                {
                    b.Property<Guid>("RoleId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("Version");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("KidsToyHive.Domain.Models.SalesOrder", b =>
                {
                    b.Property<Guid>("SalesOrderId")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("Cost");

                    b.Property<DateTime>("Created");

                    b.Property<int>("Status");

                    b.Property<Guid>("TenantKey");

                    b.Property<int>("Version");

                    b.HasKey("SalesOrderId");

                    b.ToTable("SalesOrders");
                });

            modelBuilder.Entity("KidsToyHive.Domain.Models.SalesOrderDetail", b =>
                {
                    b.Property<Guid>("SalesOrderDetailId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<Guid?>("SalesOrderId");

                    b.Property<int>("Version");

                    b.HasKey("SalesOrderDetailId");

                    b.HasIndex("SalesOrderId");

                    b.ToTable("SalesOrderDetails");
                });

            modelBuilder.Entity("KidsToyHive.Domain.Models.Shipment", b =>
                {
                    b.Property<Guid>("ShipmentId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<Guid>("DriverId");

                    b.Property<Guid?>("LocationId");

                    b.Property<Guid>("SignatureId");

                    b.Property<int>("Status");

                    b.Property<Guid>("TenantKey");

                    b.Property<byte>("Type");

                    b.Property<int>("Version");

                    b.HasKey("ShipmentId");

                    b.HasIndex("DriverId");

                    b.HasIndex("LocationId");

                    b.HasIndex("SignatureId");

                    b.ToTable("Shipments");
                });

            modelBuilder.Entity("KidsToyHive.Domain.Models.ShipmentBooking", b =>
                {
                    b.Property<Guid>("ShipmentBookingId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("BookingId");

                    b.Property<string>("Name");

                    b.Property<Guid?>("ShipmentId");

                    b.Property<int>("Version");

                    b.HasKey("ShipmentBookingId");

                    b.HasIndex("BookingId");

                    b.HasIndex("ShipmentId");

                    b.ToTable("ShipmentBookings");
                });

            modelBuilder.Entity("KidsToyHive.Domain.Models.ShipmentSalesOrder", b =>
                {
                    b.Property<Guid>("ShipmentSalesOrderId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<Guid>("SalesOrderId");

                    b.Property<Guid>("ShipmentId");

                    b.Property<int>("Version");

                    b.HasKey("ShipmentSalesOrderId");

                    b.ToTable("ShipmentSalesOrders");
                });

            modelBuilder.Entity("KidsToyHive.Domain.Models.Signature", b =>
                {
                    b.Property<Guid>("SignatureId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("DigitialAssetId");

                    b.Property<string>("Name");

                    b.Property<int>("Version");

                    b.HasKey("SignatureId");

                    b.ToTable("Signatures");
                });

            modelBuilder.Entity("KidsToyHive.Domain.Models.Tax", b =>
                {
                    b.Property<Guid>("TaxId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("Effective");

                    b.Property<int>("Rate");

                    b.Property<Guid>("TenantKey");

                    b.Property<byte>("Type");

                    b.Property<int>("Version");

                    b.HasKey("TaxId");

                    b.ToTable("Taxes");
                });

            modelBuilder.Entity("KidsToyHive.Domain.Models.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Password");

                    b.Property<byte[]>("Salt");

                    b.Property<string>("Username");

                    b.Property<int>("Version");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("KidsToyHive.Domain.Models.Warehouse", b =>
                {
                    b.Property<Guid>("WarehouseId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<Guid?>("LocationId");

                    b.Property<string>("Name");

                    b.Property<Guid>("TenantKey");

                    b.Property<int>("Version");

                    b.HasKey("WarehouseId");

                    b.HasIndex("LocationId");

                    b.ToTable("Warehouses");
                });

            modelBuilder.Entity("KidsToyHive.Domain.Models.Bin", b =>
                {
                    b.HasOne("KidsToyHive.Domain.Models.Warehouse", "Warehouse")
                        .WithMany("Bins")
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("KidsToyHive.Domain.Models.Booking", b =>
                {
                    b.HasOne("KidsToyHive.Domain.Models.Customer", "Customer")
                        .WithMany("Bookings")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KidsToyHive.Domain.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId");
                });

            modelBuilder.Entity("KidsToyHive.Domain.Models.BookingDetail", b =>
                {
                    b.HasOne("KidsToyHive.Domain.Models.Booking", null)
                        .WithMany("BookingDetails")
                        .HasForeignKey("BookingId");

                    b.HasOne("KidsToyHive.Domain.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("KidsToyHive.Domain.Models.Customer", b =>
                {
                    b.OwnsOne("KidsToyHive.Domain.Models.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("CustomerId");

                            b1.Property<string>("City");

                            b1.Property<string>("PostalCode");

                            b1.Property<string>("Province");

                            b1.Property<string>("Street");

                            b1.HasKey("CustomerId");

                            b1.ToTable("Customers");

                            b1.WithOwner()
                                .HasForeignKey("CustomerId");
                        });
                });

            modelBuilder.Entity("KidsToyHive.Domain.Models.CustomerLocation", b =>
                {
                    b.HasOne("KidsToyHive.Domain.Models.Customer", "Customer")
                        .WithMany("CustomerLocations")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KidsToyHive.Domain.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId");
                });

            modelBuilder.Entity("KidsToyHive.Domain.Models.Dashboard", b =>
                {
                    b.HasOne("KidsToyHive.Domain.Models.Profile", "Profile")
                        .WithMany()
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("KidsToyHive.Domain.Models.DashboardCard", b =>
                {
                    b.HasOne("KidsToyHive.Domain.Models.Dashboard", null)
                        .WithMany("DashboardCards")
                        .HasForeignKey("DashboardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("KidsToyHive.Domain.Models.Driver", b =>
                {
                    b.HasOne("KidsToyHive.Domain.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId");
                });

            modelBuilder.Entity("KidsToyHive.Domain.Models.InventoryItem", b =>
                {
                    b.HasOne("KidsToyHive.Domain.Models.Bin", "Bin")
                        .WithMany("InventoryItems")
                        .HasForeignKey("BinId");

                    b.HasOne("KidsToyHive.Domain.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KidsToyHive.Domain.Models.Warehouse", "Warehouse")
                        .WithMany("InventoryItems")
                        .HasForeignKey("WarehouseId");
                });

            modelBuilder.Entity("KidsToyHive.Domain.Models.Location", b =>
                {
                    b.OwnsOne("KidsToyHive.Domain.Models.Address", "Adddress", b1 =>
                        {
                            b1.Property<Guid>("LocationId");

                            b1.Property<string>("City");

                            b1.Property<string>("PostalCode");

                            b1.Property<string>("Province");

                            b1.Property<string>("Street");

                            b1.HasKey("LocationId");

                            b1.ToTable("Locations");

                            b1.WithOwner()
                                .HasForeignKey("LocationId");
                        });
                });

            modelBuilder.Entity("KidsToyHive.Domain.Models.Product", b =>
                {
                    b.HasOne("KidsToyHive.Domain.Models.Brand", "Brand")
                        .WithMany("Products")
                        .HasForeignKey("BrandId");

                    b.HasOne("KidsToyHive.Domain.Models.ProductCategory", "ProductCategory")
                        .WithMany()
                        .HasForeignKey("ProductCategoryId");
                });

            modelBuilder.Entity("KidsToyHive.Domain.Models.ProductImage", b =>
                {
                    b.HasOne("KidsToyHive.Domain.Models.DigitalAsset", "DigitalAsset")
                        .WithMany()
                        .HasForeignKey("DigitalAssetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KidsToyHive.Domain.Models.Product", null)
                        .WithMany("ProductImages")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("KidsToyHive.Domain.Models.Profile", b =>
                {
                    b.HasOne("KidsToyHive.Domain.Models.User", "User")
                        .WithMany("Profiles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("KidsToyHive.Domain.Models.SalesOrderDetail", b =>
                {
                    b.HasOne("KidsToyHive.Domain.Models.SalesOrder", null)
                        .WithMany("SalesOrderDetails")
                        .HasForeignKey("SalesOrderId");
                });

            modelBuilder.Entity("KidsToyHive.Domain.Models.Shipment", b =>
                {
                    b.HasOne("KidsToyHive.Domain.Models.Driver", "Driver")
                        .WithMany("Shipments")
                        .HasForeignKey("DriverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KidsToyHive.Domain.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId");

                    b.HasOne("KidsToyHive.Domain.Models.Signature", "Signature")
                        .WithMany()
                        .HasForeignKey("SignatureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("KidsToyHive.Domain.Models.ShipmentBooking", b =>
                {
                    b.HasOne("KidsToyHive.Domain.Models.Booking", "Booking")
                        .WithMany()
                        .HasForeignKey("BookingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KidsToyHive.Domain.Models.Shipment", null)
                        .WithMany("ShipmentBookings")
                        .HasForeignKey("ShipmentId");
                });

            modelBuilder.Entity("KidsToyHive.Domain.Models.Warehouse", b =>
                {
                    b.HasOne("KidsToyHive.Domain.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId");
                });
#pragma warning restore 612, 618
        }
    }
}
