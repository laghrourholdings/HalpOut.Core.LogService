﻿// <auto-generated />
using System;
using LogService.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LogService.Migrations
{
    [DbContext(typeof(ServiceDbContext))]
    [Migration("20221119044821_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CommonLibrary.Logging.LogHandle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AuthorizationDetails")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("DeletedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Descriptor")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsSuspended")
                        .HasColumnType("boolean");

                    b.Property<string>("LocationDetails")
                        .HasColumnType("text");

                    b.Property<Guid>("ObjectId")
                        .HasColumnType("uuid");

                    b.Property<string>("ObjectType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("SuspendedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("LogHandles");
                });

            modelBuilder.Entity("CommonLibrary.Logging.LogMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("DeletedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Descriptor")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsSuspended")
                        .HasColumnType("boolean");

                    b.Property<Guid>("LogHandleId")
                        .HasColumnType("uuid");

                    b.Property<int>("Severity")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset>("SuspendedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("LogHandleId");

                    b.ToTable("LogMessages");
                });

            modelBuilder.Entity("CommonLibrary.Logging.LogMessage", b =>
                {
                    b.HasOne("CommonLibrary.Logging.LogHandle", "LogHandle")
                        .WithMany("Messages")
                        .HasForeignKey("LogHandleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LogHandle");
                });

            modelBuilder.Entity("CommonLibrary.Logging.LogHandle", b =>
                {
                    b.Navigation("Messages");
                });
#pragma warning restore 612, 618
        }
    }
}