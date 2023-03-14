using System;
using KidsToyHive.Domain.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KidsToyHive.Api.Migrations;

 [DbContext(typeof(AppDbContext))]
 [Migration("20190901120538_InitialCreate")]
 partial class InitialCreate
 {
     protected override void BuildTargetModel(ModelBuilder modelBuilder)
     {
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
                 b.Property<Guid?>("TenantId");
                 b.Property<int>("Version");
                 b.Property<Guid>("WarehouseId");
                 b.HasKey("BinId");
                 b.HasIndex("TenantId");
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
                 b.Property<Guid?>("TenantId");
                 b.Property<int>("Version");
                 b.HasKey("BookingId");
                 b.HasIndex("CustomerId");
                 b.HasIndex("LocationId");
                 b.HasIndex("TenantId");
                 b.ToTable("Bookings");
             });
         modelBuilder.Entity("KidsToyHive.Domain.Models.BookingDetail", b =>
             {
                 b.Property<Guid>("BookingDetailId")
                     .ValueGeneratedOnAdd();
                 b.Property<Guid?>("BookingId");
                 b.Property<int>("Cost");
                 b.Property<DateTime>("Created");
                 b.Property<Guid?>("LocationId");
                 b.Property<Guid>("ProductId");
                 b.Property<int>("Quantity");
                 b.Property<Guid?>("TenantId");
                 b.Property<int>("Version");
                 b.HasKey("BookingDetailId");
                 b.HasIndex("BookingId");
                 b.HasIndex("LocationId");
                 b.HasIndex("ProductId");
                 b.HasIndex("TenantId");
                 b.ToTable("BookingDetails");
             });
         modelBuilder.Entity("KidsToyHive.Domain.Models.Brand", b =>
             {
                 b.Property<Guid>("BrandId")
                     .ValueGeneratedOnAdd();
                 b.Property<DateTime>("Created");
                 b.Property<string>("ImageUrl");
                 b.Property<string>("Name");
                 b.Property<Guid?>("TenantId");
                 b.Property<int>("Version");
                 b.HasKey("BrandId");
                 b.HasIndex("TenantId");
                 b.ToTable("Brands");
             });
         modelBuilder.Entity("KidsToyHive.Domain.Models.Card", b =>
             {
                 b.Property<Guid>("CardId")
                     .ValueGeneratedOnAdd();
                 b.Property<DateTime>("Created");
                 b.Property<string>("Description");
                 b.Property<string>("Name");
                 b.Property<Guid?>("TenantId");
                 b.Property<int>("Version");
                 b.HasKey("CardId");
                 b.HasIndex("TenantId");
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
         modelBuilder.Entity("KidsToyHive.Domain.Models.Contact", b =>
             {
                 b.Property<Guid>("ContactId")
                     .ValueGeneratedOnAdd();
                 b.Property<DateTime>("Created");
                 b.Property<string>("Email");
                 b.Property<string>("FullName");
                 b.Property<string>("PhoneNumber");
                 b.Property<Guid?>("TenantId");
                 b.Property<int>("Version");
                 b.HasKey("ContactId");
                 b.HasIndex("TenantId");
                 b.ToTable("Contacts");
             });
         modelBuilder.Entity("KidsToyHive.Domain.Models.ContactMessage", b =>
             {
                 b.Property<Guid>("ContactMessageId")
                     .ValueGeneratedOnAdd();
                 b.Property<Guid?>("ContactId");
                 b.Property<string>("Value");
                 b.Property<int>("Version");
                 b.HasKey("ContactMessageId");
                 b.HasIndex("ContactId");
                 b.ToTable("ContactMessages");
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
                 b.Property<Guid?>("TenantId");
                 b.Property<int>("Version");
                 b.HasKey("CustomerId");
                 b.HasIndex("TenantId");
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
                 b.Property<Guid?>("TenantId");
                 b.Property<int>("Version");
                 b.HasKey("CustomerLocationId");
                 b.HasIndex("CustomerId");
                 b.HasIndex("LocationId");
                 b.HasIndex("TenantId");
                 b.ToTable("CustomerLocations");
             });
         modelBuilder.Entity("KidsToyHive.Domain.Models.CustomerTermsAndConditions", b =>
             {
                 b.Property<Guid>("CustomerTermsAndConditionsId")
                     .ValueGeneratedOnAdd();
                 b.Property<DateTime>("Accepted");
                 b.Property<Guid>("CustomerId");
                 b.HasKey("CustomerTermsAndConditionsId");
                 b.HasIndex("CustomerId");
                 b.ToTable("CustomerTermsAndConditions");
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
                 b.Property<Guid?>("TenantId");
                 b.Property<int>("Version");
                 b.HasKey("DriverId");
                 b.HasIndex("LocationId");
                 b.HasIndex("TenantId");
                 b.ToTable("Drivers");
             });
         modelBuilder.Entity("KidsToyHive.Domain.Models.EmailTemplate", b =>
             {
                 b.Property<Guid>("EmailTemplateId")
                     .ValueGeneratedOnAdd();
                 b.Property<string>("Name");
                 b.Property<string>("Value");
                 b.HasKey("EmailTemplateId");
                 b.ToTable("EmailTemplates");
             });
         modelBuilder.Entity("KidsToyHive.Domain.Models.HtmlContent", b =>
             {
                 b.Property<Guid>("HtmlContentId")
                     .ValueGeneratedOnAdd();
                 b.Property<DateTime>("Created");
                 b.Property<string>("Name");
                 b.Property<Guid?>("TenantId");
                 b.Property<string>("Value");
                 b.Property<int>("Version");
                 b.HasKey("HtmlContentId");
                 b.HasIndex("TenantId");
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
                 b.Property<Guid?>("TenantId");
                 b.Property<int>("Version");
                 b.Property<Guid?>("WarehouseId");
                 b.HasKey("InventoryItemId");
                 b.HasIndex("BinId");
                 b.HasIndex("ProductId");
                 b.HasIndex("TenantId");
                 b.HasIndex("WarehouseId");
                 b.ToTable("InventoryItems");
             });
         modelBuilder.Entity("KidsToyHive.Domain.Models.Location", b =>
             {
                 b.Property<Guid>("LocationId")
                     .ValueGeneratedOnAdd();
                 b.Property<DateTime>("Created");
                 b.Property<string>("Description");
                 b.Property<Guid?>("TenantId");
                 b.Property<int>("Type");
                 b.Property<int>("Version");
                 b.HasKey("LocationId");
                 b.HasIndex("TenantId");
                 b.ToTable("Locations");
             });
         modelBuilder.Entity("KidsToyHive.Domain.Models.Option", b =>
             {
                 b.Property<Guid>("OptionId")
                     .ValueGeneratedOnAdd();
                 b.Property<DateTime>("Created");
                 b.Property<string>("Name");
                 b.Property<int>("Order");
                 b.Property<Guid?>("QuestionId");
                 b.Property<Guid?>("TenantId");
                 b.Property<int>("Version");
                 b.HasKey("OptionId");
                 b.HasIndex("QuestionId");
                 b.HasIndex("TenantId");
                 b.ToTable("Option");
             });
         modelBuilder.Entity("KidsToyHive.Domain.Models.Product", b =>
             {
                 b.Property<Guid>("ProductId")
                     .ValueGeneratedOnAdd();
                 b.Property<Guid?>("BrandId");
                 b.Property<int>("ChargePeriodPrice");
                 b.Property<DateTime>("Created");
                 b.Property<string>("Description");
                 b.Property<string>("Name");
                 b.Property<int>("Price");
                 b.Property<Guid?>("ProductCategoryId");
                 b.Property<Guid?>("TenantId");
                 b.Property<byte>("Type");
                 b.Property<int>("Version");
                 b.HasKey("ProductId");
                 b.HasIndex("BrandId");
                 b.HasIndex("ProductCategoryId");
                 b.HasIndex("TenantId");
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
                 b.Property<Guid?>("TenantId");
                 b.Property<int>("Version");
                 b.HasKey("ProductImageId");
                 b.HasIndex("DigitalAssetId");
                 b.HasIndex("ProductId");
                 b.HasIndex("TenantId");
                 b.ToTable("ProductImage");
             });
         modelBuilder.Entity("KidsToyHive.Domain.Models.ProfessionalServiceProvider", b =>
             {
                 b.Property<Guid>("ProfessionalServiceProviderId")
                     .ValueGeneratedOnAdd();
                 b.Property<DateTime>("Created");
                 b.Property<string>("FullName");
                 b.Property<string>("ImageUrl");
                 b.Property<Guid?>("TenantId");
                 b.Property<string>("Title");
                 b.Property<int>("Type");
                 b.Property<int>("Version");
                 b.HasKey("ProfessionalServiceProviderId");
                 b.HasIndex("TenantId");
                 b.ToTable("ProfessionalServiceProviders");
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
         modelBuilder.Entity("KidsToyHive.Domain.Models.Question", b =>
             {
                 b.Property<Guid>("QuestionId")
                     .ValueGeneratedOnAdd();
                 b.Property<string>("Body");
                 b.Property<DateTime>("Created");
                 b.Property<string>("Description");
                 b.Property<string>("Name");
                 b.Property<int>("Order");
                 b.Property<int>("QuestionType");
                 b.Property<Guid?>("SurveyId");
                 b.Property<Guid?>("TenantId");
                 b.Property<int>("Version");
                 b.HasKey("QuestionId");
                 b.HasIndex("SurveyId");
                 b.HasIndex("TenantId");
                 b.ToTable("Question");
             });
         modelBuilder.Entity("KidsToyHive.Domain.Models.Response", b =>
             {
                 b.Property<Guid>("ResponseId")
                     .ValueGeneratedOnAdd();
                 b.Property<DateTime>("Created");
                 b.Property<Guid?>("OptionId");
                 b.Property<Guid?>("QuestionId");
                 b.Property<Guid?>("SurveyResultId");
                 b.Property<Guid?>("TenantId");
                 b.Property<string>("Value");
                 b.Property<int>("Version");
                 b.HasKey("ResponseId");
                 b.HasIndex("OptionId");
                 b.HasIndex("QuestionId");
                 b.HasIndex("SurveyResultId");
                 b.HasIndex("TenantId");
                 b.ToTable("Response");
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
                 b.Property<Guid?>("TenantId");
                 b.Property<int>("Version");
                 b.HasKey("SalesOrderId");
                 b.HasIndex("TenantId");
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
                 b.Property<string>("Comment");
                 b.Property<DateTime>("Created");
                 b.Property<Guid?>("DriverId");
                 b.Property<Guid?>("LocationId");
                 b.Property<Guid?>("SignatureId");
                 b.Property<int>("Status");
                 b.Property<Guid?>("TenantId");
                 b.Property<double>("TotalWeight");
                 b.Property<string>("TrackingNumber");
                 b.Property<byte>("Type");
                 b.Property<int>("Version");
                 b.HasKey("ShipmentId");
                 b.HasIndex("DriverId");
                 b.HasIndex("LocationId");
                 b.HasIndex("SignatureId");
                 b.HasIndex("TenantId");
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
         modelBuilder.Entity("KidsToyHive.Domain.Models.ShipmentItem", b =>
             {
                 b.Property<Guid>("ShipmentItemId")
                     .ValueGeneratedOnAdd();
                 b.Property<Guid?>("BookingDetailId");
                 b.Property<string>("Comments");
                 b.Property<DateTime>("Created");
                 b.Property<int>("Quantity");
                 b.Property<Guid?>("SalesOrderDetailId");
                 b.Property<Guid>("ShipmentId");
                 b.Property<Guid?>("TenantId");
                 b.Property<int>("Version");
                 b.HasKey("ShipmentItemId");
                 b.HasIndex("ShipmentId");
                 b.HasIndex("TenantId");
                 b.ToTable("ShipmentItems");
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
                 b.HasIndex("ShipmentId");
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
         modelBuilder.Entity("KidsToyHive.Domain.Models.Survey", b =>
             {
                 b.Property<Guid>("SurveyId")
                     .ValueGeneratedOnAdd();
                 b.Property<DateTime>("Created");
                 b.Property<string>("Name");
                 b.Property<Guid?>("TenantId");
                 b.Property<int>("Version");
                 b.HasKey("SurveyId");
                 b.HasIndex("TenantId");
                 b.ToTable("Surveys");
             });
         modelBuilder.Entity("KidsToyHive.Domain.Models.SurveyResult", b =>
             {
                 b.Property<Guid>("SurveyResultId")
                     .ValueGeneratedOnAdd();
                 b.Property<DateTime>("Created");
                 b.Property<string>("Name");
                 b.Property<Guid?>("SurveyId");
                 b.Property<Guid?>("TenantId");
                 b.Property<int>("Version");
                 b.HasKey("SurveyResultId");
                 b.HasIndex("SurveyId");
                 b.HasIndex("TenantId");
                 b.ToTable("SurveyResult");
             });
         modelBuilder.Entity("KidsToyHive.Domain.Models.Tax", b =>
             {
                 b.Property<Guid>("TaxId")
                     .ValueGeneratedOnAdd();
                 b.Property<DateTime>("Created");
                 b.Property<DateTime>("Effective");
                 b.Property<double>("Rate");
                 b.Property<Guid?>("TenantId");
                 b.Property<byte>("Type");
                 b.Property<int>("Version");
                 b.HasKey("TaxId");
                 b.HasIndex("TenantId");
                 b.ToTable("Taxes");
             });
         modelBuilder.Entity("KidsToyHive.Domain.Models.Tenant", b =>
             {
                 b.Property<Guid>("TenantId")
                     .ValueGeneratedOnAdd();
                 b.Property<string>("Name");
                 b.HasKey("TenantId");
                 b.ToTable("Tenants");
             });
         modelBuilder.Entity("KidsToyHive.Domain.Models.User", b =>
             {
                 b.Property<Guid>("UserId")
                     .ValueGeneratedOnAdd();
                 b.Property<DateTime>("Created");
                 b.Property<string>("Password");
                 b.Property<bool>("PasswordChangeRequired");
                 b.Property<byte[]>("Salt");
                 b.Property<Guid?>("TenantId");
                 b.Property<string>("Username");
                 b.Property<int>("Version");
                 b.HasKey("UserId");
                 b.HasIndex("TenantId");
                 b.ToTable("Users");
             });
         modelBuilder.Entity("KidsToyHive.Domain.Models.Video", b =>
             {
                 b.Property<Guid>("VideoId")
                     .ValueGeneratedOnAdd();
                 b.Property<string>("Abstract");
                 b.Property<string>("Category");
                 b.Property<DateTime>("Created");
                 b.Property<string>("Description");
                 b.Property<int>("DurationInSeconds");
                 b.Property<DateTime?>("Published");
                 b.Property<double>("Rating");
                 b.Property<string>("Slug");
                 b.Property<string>("SubTitle");
                 b.Property<Guid?>("TenantId");
                 b.Property<string>("Title");
                 b.Property<int>("Version");
                 b.Property<string>("YouTubeVideoId");
                 b.HasKey("VideoId");
                 b.HasIndex("TenantId");
                 b.ToTable("Videos");
             });
         modelBuilder.Entity("KidsToyHive.Domain.Models.Warehouse", b =>
             {
                 b.Property<Guid>("WarehouseId")
                     .ValueGeneratedOnAdd();
                 b.Property<DateTime>("Created");
                 b.Property<Guid?>("LocationId");
                 b.Property<string>("Name");
                 b.Property<Guid?>("TenantId");
                 b.Property<int>("Version");
                 b.HasKey("WarehouseId");
                 b.HasIndex("LocationId");
                 b.HasIndex("TenantId");
                 b.ToTable("Warehouses");
             });
         modelBuilder.Entity("KidsToyHive.Domain.Models.Bin", b =>
             {
                 b.HasOne("KidsToyHive.Domain.Models.Tenant", "Tenant")
                     .WithMany()
                     .HasForeignKey("TenantId");
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
                     .WithMany("Bookings")
                     .HasForeignKey("LocationId");
                 b.HasOne("KidsToyHive.Domain.Models.Tenant", "Tenant")
                     .WithMany()
                     .HasForeignKey("TenantId");
             });
         modelBuilder.Entity("KidsToyHive.Domain.Models.BookingDetail", b =>
             {
                 b.HasOne("KidsToyHive.Domain.Models.Booking", null)
                     .WithMany("BookingDetails")
                     .HasForeignKey("BookingId");
                 b.HasOne("KidsToyHive.Domain.Models.Location", "Location")
                     .WithMany("BookingDetails")
                     .HasForeignKey("LocationId");
                 b.HasOne("KidsToyHive.Domain.Models.Product", "Product")
                     .WithMany()
                     .HasForeignKey("ProductId")
                     .OnDelete(DeleteBehavior.Cascade)
                     .IsRequired();
                 b.HasOne("KidsToyHive.Domain.Models.Tenant", "Tenant")
                     .WithMany()
                     .HasForeignKey("TenantId");
             });
         modelBuilder.Entity("KidsToyHive.Domain.Models.Brand", b =>
             {
                 b.HasOne("KidsToyHive.Domain.Models.Tenant", "Tenant")
                     .WithMany()
                     .HasForeignKey("TenantId");
             });
         modelBuilder.Entity("KidsToyHive.Domain.Models.Card", b =>
             {
                 b.HasOne("KidsToyHive.Domain.Models.Tenant", "Tenant")
                     .WithMany()
                     .HasForeignKey("TenantId");
             });
         modelBuilder.Entity("KidsToyHive.Domain.Models.Contact", b =>
             {
                 b.HasOne("KidsToyHive.Domain.Models.Tenant", "Tenant")
                     .WithMany()
                     .HasForeignKey("TenantId");
             });
         modelBuilder.Entity("KidsToyHive.Domain.Models.ContactMessage", b =>
             {
                 b.HasOne("KidsToyHive.Domain.Models.Contact", null)
                     .WithMany("ContactMessages")
                     .HasForeignKey("ContactId");
             });
         modelBuilder.Entity("KidsToyHive.Domain.Models.Customer", b =>
             {
                 b.HasOne("KidsToyHive.Domain.Models.Tenant", "Tenant")
                     .WithMany()
                     .HasForeignKey("TenantId");
                 b.OwnsOne("KidsToyHive.Domain.Models.Address", "Address", b1 =>
                     {
                         b1.Property<Guid>("CustomerId");
                         b1.Property<string>("City");
                         b1.Property<double>("Latitude");
                         b1.Property<double>("Longitude");
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
                 b.HasOne("KidsToyHive.Domain.Models.Tenant", "Tenant")
                     .WithMany()
                     .HasForeignKey("TenantId");
             });
         modelBuilder.Entity("KidsToyHive.Domain.Models.CustomerTermsAndConditions", b =>
             {
                 b.HasOne("KidsToyHive.Domain.Models.Customer", "Customer")
                     .WithMany("CustomerTermsAndConditions")
                     .HasForeignKey("CustomerId")
                     .OnDelete(DeleteBehavior.Cascade)
                     .IsRequired();
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
                 b.HasOne("KidsToyHive.Domain.Models.Tenant", "Tenant")
                     .WithMany()
                     .HasForeignKey("TenantId");
             });
         modelBuilder.Entity("KidsToyHive.Domain.Models.HtmlContent", b =>
             {
                 b.HasOne("KidsToyHive.Domain.Models.Tenant", "Tenant")
                     .WithMany()
                     .HasForeignKey("TenantId");
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
                 b.HasOne("KidsToyHive.Domain.Models.Tenant", "Tenant")
                     .WithMany()
                     .HasForeignKey("TenantId");
                 b.HasOne("KidsToyHive.Domain.Models.Warehouse", "Warehouse")
                     .WithMany("InventoryItems")
                     .HasForeignKey("WarehouseId");
             });
         modelBuilder.Entity("KidsToyHive.Domain.Models.Location", b =>
             {
                 b.HasOne("KidsToyHive.Domain.Models.Tenant", "Tenant")
                     .WithMany()
                     .HasForeignKey("TenantId");
                 b.OwnsOne("KidsToyHive.Domain.Models.Address", "Adddress", b1 =>
                     {
                         b1.Property<Guid>("LocationId");
                         b1.Property<string>("City");
                         b1.Property<double>("Latitude");
                         b1.Property<double>("Longitude");
                         b1.Property<string>("PostalCode");
                         b1.Property<string>("Province");
                         b1.Property<string>("Street");
                         b1.HasKey("LocationId");
                         b1.ToTable("Locations");
                         b1.WithOwner()
                             .HasForeignKey("LocationId");
                     });
             });
         modelBuilder.Entity("KidsToyHive.Domain.Models.Option", b =>
             {
                 b.HasOne("KidsToyHive.Domain.Models.Question", "Question")
                     .WithMany("Options")
                     .HasForeignKey("QuestionId");
                 b.HasOne("KidsToyHive.Domain.Models.Tenant", "Tenant")
                     .WithMany()
                     .HasForeignKey("TenantId");
             });
         modelBuilder.Entity("KidsToyHive.Domain.Models.Product", b =>
             {
                 b.HasOne("KidsToyHive.Domain.Models.Brand", "Brand")
                     .WithMany("Products")
                     .HasForeignKey("BrandId");
                 b.HasOne("KidsToyHive.Domain.Models.ProductCategory", "ProductCategory")
                     .WithMany()
                     .HasForeignKey("ProductCategoryId");
                 b.HasOne("KidsToyHive.Domain.Models.Tenant", "Tenant")
                     .WithMany()
                     .HasForeignKey("TenantId");
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
                 b.HasOne("KidsToyHive.Domain.Models.Tenant", "Tenant")
                     .WithMany()
                     .HasForeignKey("TenantId");
             });
         modelBuilder.Entity("KidsToyHive.Domain.Models.ProfessionalServiceProvider", b =>
             {
                 b.HasOne("KidsToyHive.Domain.Models.Tenant", "Tenant")
                     .WithMany()
                     .HasForeignKey("TenantId");
             });
         modelBuilder.Entity("KidsToyHive.Domain.Models.Profile", b =>
             {
                 b.HasOne("KidsToyHive.Domain.Models.User", "User")
                     .WithMany("Profiles")
                     .HasForeignKey("UserId")
                     .OnDelete(DeleteBehavior.Cascade)
                     .IsRequired();
             });
         modelBuilder.Entity("KidsToyHive.Domain.Models.Question", b =>
             {
                 b.HasOne("KidsToyHive.Domain.Models.Survey", "Survey")
                     .WithMany("Questions")
                     .HasForeignKey("SurveyId");
                 b.HasOne("KidsToyHive.Domain.Models.Tenant", "Tenant")
                     .WithMany()
                     .HasForeignKey("TenantId");
             });
         modelBuilder.Entity("KidsToyHive.Domain.Models.Response", b =>
             {
                 b.HasOne("KidsToyHive.Domain.Models.Option", "Option")
                     .WithMany("Responses")
                     .HasForeignKey("OptionId");
                 b.HasOne("KidsToyHive.Domain.Models.Question", "Question")
                     .WithMany()
                     .HasForeignKey("QuestionId");
                 b.HasOne("KidsToyHive.Domain.Models.SurveyResult", null)
                     .WithMany("Responses")
                     .HasForeignKey("SurveyResultId");
                 b.HasOne("KidsToyHive.Domain.Models.Tenant", "Tenant")
                     .WithMany()
                     .HasForeignKey("TenantId");
             });
         modelBuilder.Entity("KidsToyHive.Domain.Models.SalesOrder", b =>
             {
                 b.HasOne("KidsToyHive.Domain.Models.Tenant", "Tenant")
                     .WithMany()
                     .HasForeignKey("TenantId");
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
                     .HasForeignKey("DriverId");
                 b.HasOne("KidsToyHive.Domain.Models.Location", "Location")
                     .WithMany()
                     .HasForeignKey("LocationId");
                 b.HasOne("KidsToyHive.Domain.Models.Signature", "Signature")
                     .WithMany()
                     .HasForeignKey("SignatureId");
                 b.HasOne("KidsToyHive.Domain.Models.Tenant", "Tenant")
                     .WithMany()
                     .HasForeignKey("TenantId");
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
         modelBuilder.Entity("KidsToyHive.Domain.Models.ShipmentItem", b =>
             {
                 b.HasOne("KidsToyHive.Domain.Models.Shipment", "Shipment")
                     .WithMany("ShipmentItems")
                     .HasForeignKey("ShipmentId")
                     .OnDelete(DeleteBehavior.Cascade)
                     .IsRequired();
                 b.HasOne("KidsToyHive.Domain.Models.Tenant", "Tenant")
                     .WithMany()
                     .HasForeignKey("TenantId");
             });
         modelBuilder.Entity("KidsToyHive.Domain.Models.ShipmentSalesOrder", b =>
             {
                 b.HasOne("KidsToyHive.Domain.Models.Shipment", null)
                     .WithMany("ShipmentSalesOrders")
                     .HasForeignKey("ShipmentId")
                     .OnDelete(DeleteBehavior.Cascade)
                     .IsRequired();
             });
         modelBuilder.Entity("KidsToyHive.Domain.Models.Survey", b =>
             {
                 b.HasOne("KidsToyHive.Domain.Models.Tenant", "Tenant")
                     .WithMany()
                     .HasForeignKey("TenantId");
             });
         modelBuilder.Entity("KidsToyHive.Domain.Models.SurveyResult", b =>
             {
                 b.HasOne("KidsToyHive.Domain.Models.Survey", "Survey")
                     .WithMany("SurveyResults")
                     .HasForeignKey("SurveyId");
                 b.HasOne("KidsToyHive.Domain.Models.Tenant", "Tenant")
                     .WithMany()
                     .HasForeignKey("TenantId");
             });
         modelBuilder.Entity("KidsToyHive.Domain.Models.Tax", b =>
             {
                 b.HasOne("KidsToyHive.Domain.Models.Tenant", "Tenant")
                     .WithMany()
                     .HasForeignKey("TenantId");
             });
         modelBuilder.Entity("KidsToyHive.Domain.Models.User", b =>
             {
                 b.HasOne("KidsToyHive.Domain.Models.Tenant", "Tenant")
                     .WithMany()
                     .HasForeignKey("TenantId");
             });
         modelBuilder.Entity("KidsToyHive.Domain.Models.Video", b =>
             {
                 b.HasOne("KidsToyHive.Domain.Models.Tenant", "Tenant")
                     .WithMany()
                     .HasForeignKey("TenantId");
             });
         modelBuilder.Entity("KidsToyHive.Domain.Models.Warehouse", b =>
             {
                 b.HasOne("KidsToyHive.Domain.Models.Location", "Location")
                     .WithMany()
                     .HasForeignKey("LocationId");
                 b.HasOne("KidsToyHive.Domain.Models.Tenant", "Tenant")
                     .WithMany()
                     .HasForeignKey("TenantId");
             });
     }
 }
