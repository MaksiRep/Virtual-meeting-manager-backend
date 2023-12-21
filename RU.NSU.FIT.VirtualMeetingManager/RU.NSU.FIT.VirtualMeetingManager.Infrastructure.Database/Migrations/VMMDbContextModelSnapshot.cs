﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RU.NSU.FIT.VirtualMeetingManager;

#nullable disable

namespace RU.NSU.FIT.VirtualMeetingManager.Migrations
{
    [DbContext(typeof(VMMDbContext))]
    partial class VMMDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MeetingUser", b =>
                {
                    b.Property<int>("MeetingsId")
                        .HasColumnType("integer");

                    b.Property<Guid>("UsersId")
                        .HasColumnType("uuid");

                    b.HasKey("MeetingsId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("UserMeetings", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("RoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("UserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.ToTable("UserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("RU.NSU.FIT.VirtualManager.Domain.Entities.Meeting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("character varying(1024)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<byte?>("Gender")
                        .HasColumnType("smallint");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("text");

                    b.Property<Guid>("ManagerId")
                        .HasColumnType("uuid");

                    b.Property<int?>("MaxUsers")
                        .HasColumnType("integer");

                    b.Property<int?>("MinAge")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("ShortDescription")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("ManagerId");

                    b.ToTable("Meetings", (string)null);
                });

            modelBuilder.Entity("RU.NSU.FIT.VirtualManager.Domain.Entities.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("IssuedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ValidUntil")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("RU.NSU.FIT.VirtualManager.Domain.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("4b110a01-7935-483a-b127-1646acc211c7"),
                            Name = "MainAdmin",
                            NormalizedName = "MAINADMIN"
                        },
                        new
                        {
                            Id = new Guid("ff3405f7-9a3b-46d3-83d6-e2e3a147f8be"),
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = new Guid("b65ed1ed-7cbe-41c6-a24e-2e457fd51f58"),
                            Name = "User",
                            NormalizedName = "USER"
                        });
                });

            modelBuilder.Entity("RU.NSU.FIT.VirtualManager.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<DateOnly>("BirthDate")
                        .HasColumnType("date");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<byte>("Gender")
                        .HasColumnType("smallint");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("text");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("RegisteredOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("630b2bdc-abd2-44eb-9b16-30025d98202e"),
                            AccessFailedCount = 0,
                            BirthDate = new DateOnly(2000, 1, 1),
                            ConcurrencyStamp = "5730f234-901e-4fe8-b1c4-02676a138535",
                            Email = "main@test.test",
                            EmailConfirmed = false,
                            FirstName = "mainAdminName",
                            Gender = (byte)2,
                            LastName = "lastName",
                            LockoutEnabled = false,
                            NormalizedUserName = "MAIN@TEST.TEST",
                            PasswordHash = "AQAAAAIAAYagAAAAEP9o2+DxNCEYAQ8v4blIE5HC8Mv6wkjwxtTrvLFhu2dVycSdXLHv1dIH7k/hkPVsDw==",
                            PhoneNumberConfirmed = false,
                            RegisteredOn = new DateTime(2020, 10, 10, 0, 0, 0, 0, DateTimeKind.Utc),
                            SecurityStamp = "9823af76-57b6-437e-b09d-bdcce45cd2b2",
                            TwoFactorEnabled = false,
                            UserName = "main@test.test"
                        },
                        new
                        {
                            Id = new Guid("0eddcd7a-b582-4637-ba9c-b1ed69cc361a"),
                            AccessFailedCount = 0,
                            BirthDate = new DateOnly(2000, 1, 1),
                            ConcurrencyStamp = "c93100a0-ae44-45cb-b3c2-42c78cf863bc",
                            Email = "admin@test.test",
                            EmailConfirmed = false,
                            FirstName = "adminName",
                            Gender = (byte)2,
                            LastName = "lastName",
                            LockoutEnabled = false,
                            NormalizedUserName = "ADMIN@TEST.TEST",
                            PasswordHash = "AQAAAAIAAYagAAAAEHxXBdwELduSm92SX0KF8LxmXPG0YhZmaf9KUm4SleDuKKNphqzDAtf7dICBYSomsQ==",
                            PhoneNumberConfirmed = false,
                            RegisteredOn = new DateTime(2020, 10, 10, 0, 0, 0, 0, DateTimeKind.Utc),
                            SecurityStamp = "e4bc5d91-6795-4d81-b2e5-89fb961fc8b8",
                            TwoFactorEnabled = false,
                            UserName = "admin@test.test"
                        },
                        new
                        {
                            Id = new Guid("70f36e69-f7b9-42fc-9e77-a59f3fd9bfac"),
                            AccessFailedCount = 0,
                            BirthDate = new DateOnly(2000, 1, 1),
                            ConcurrencyStamp = "6f9946ff-c3ac-4489-ae80-9ef72d22d682",
                            Email = "user@test.test",
                            EmailConfirmed = false,
                            FirstName = "userName",
                            Gender = (byte)2,
                            LastName = "lastName",
                            LockoutEnabled = false,
                            NormalizedUserName = "USER@TEST.TEST",
                            PasswordHash = "AQAAAAIAAYagAAAAEMvUzja/8Pgj9k6Q1oBdU4YUn8NOnLs6JxBdtc6v5KbMkYb4CzPk0uxNHdyMi7PHeQ==",
                            PhoneNumberConfirmed = false,
                            RegisteredOn = new DateTime(2020, 10, 10, 0, 0, 0, 0, DateTimeKind.Utc),
                            SecurityStamp = "32b97c23-6fd7-4cce-8c08-f21cfa047942",
                            TwoFactorEnabled = false,
                            UserName = "user@test.test"
                        });
                });

            modelBuilder.Entity("RU.NSU.FIT.VirtualManager.Domain.Entities.UserRole", b =>
                {
                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("RoleId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            RoleId = new Guid("4b110a01-7935-483a-b127-1646acc211c7"),
                            UserId = new Guid("630b2bdc-abd2-44eb-9b16-30025d98202e")
                        },
                        new
                        {
                            RoleId = new Guid("ff3405f7-9a3b-46d3-83d6-e2e3a147f8be"),
                            UserId = new Guid("0eddcd7a-b582-4637-ba9c-b1ed69cc361a")
                        },
                        new
                        {
                            RoleId = new Guid("b65ed1ed-7cbe-41c6-a24e-2e457fd51f58"),
                            UserId = new Guid("70f36e69-f7b9-42fc-9e77-a59f3fd9bfac")
                        });
                });

            modelBuilder.Entity("MeetingUser", b =>
                {
                    b.HasOne("RU.NSU.FIT.VirtualManager.Domain.Entities.Meeting", null)
                        .WithMany()
                        .HasForeignKey("MeetingsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RU.NSU.FIT.VirtualManager.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RU.NSU.FIT.VirtualManager.Domain.Entities.Meeting", b =>
                {
                    b.HasOne("RU.NSU.FIT.VirtualManager.Domain.Entities.User", "Manager")
                        .WithMany()
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("RU.NSU.FIT.VirtualManager.Domain.Entities.RefreshToken", b =>
                {
                    b.HasOne("RU.NSU.FIT.VirtualManager.Domain.Entities.User", "Owner")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("RU.NSU.FIT.VirtualManager.Domain.Entities.UserRole", b =>
                {
                    b.HasOne("RU.NSU.FIT.VirtualManager.Domain.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RU.NSU.FIT.VirtualManager.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RU.NSU.FIT.VirtualManager.Domain.Entities.User", b =>
                {
                    b.Navigation("RefreshTokens");
                });
#pragma warning restore 612, 618
        }
    }
}
