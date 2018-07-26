﻿// <auto-generated />
using System;
using KidsToyHive.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KidsToyHive.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20180726115215_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("KidsToyHive.Core.Models.StoredEvent", b =>
                {
                    b.Property<Guid>("StoredEventId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Aggregate");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Data");

                    b.Property<string>("DotNetType");

                    b.Property<Guid>("StreamId");

                    b.Property<string>("Type");

                    b.Property<int>("Version");

                    b.HasKey("StoredEventId");

                    b.ToTable("StoredEvents");
                });
#pragma warning restore 612, 618
        }
    }
}
