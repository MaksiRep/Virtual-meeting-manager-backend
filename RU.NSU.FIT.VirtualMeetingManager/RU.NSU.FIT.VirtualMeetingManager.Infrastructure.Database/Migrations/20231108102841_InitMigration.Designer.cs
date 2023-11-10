﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RU.NSU.FIT.VirtualMeetingManager;

#nullable disable

namespace RU.NSU.FIT.VirtualMeetingManager.Migrations
{
    [DbContext(typeof(VMMDbContext))]
    [Migration("20231108102841_InitMigration")]
    partial class InitMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

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
                        .HasColumnType("text");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

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
                        .HasColumnType("text");

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
                            Id = new Guid("0eddcd7a-b582-4637-ba9c-b1ed69cc361a"),
                            AccessFailedCount = 0,
                            BirthDate = new DateOnly(2000, 1, 1),
                            ConcurrencyStamp = "3f1a473e-8774-4c8c-978c-bfe0daa9cdb6",
                            Email = "admin@test.test",
                            EmailConfirmed = false,
                            FirstName = "adminName",
                            LastName = "lastName",
                            LockoutEnabled = false,
                            NormalizedUserName = "ADMIN@TEST.TEST",
                            PasswordHash = "AQAAAAIAAYagAAAAEAi9bJTvk2nDlje+Xvx60sU9UO/4czm6BFUwC7gRxWSUpLbbsyPz0prXUSdYPufq6g==",
                            PhoneNumberConfirmed = false,
                            RegisteredOn = new DateTime(2020, 10, 10, 0, 0, 0, 0, DateTimeKind.Utc),
                            SecurityStamp = "706f1ef6-361e-41d2-aeec-d9f7f1e3394b",
                            TwoFactorEnabled = false,
                            UserName = "admin@test.test"
                        },
                        new
                        {
                            Id = new Guid("70f36e69-f7b9-42fc-9e77-a59f3fd9bfac"),
                            AccessFailedCount = 0,
                            BirthDate = new DateOnly(2000, 1, 1),
                            ConcurrencyStamp = "6529a386-dd02-4ad6-af5f-dcbe1ba38479",
                            Email = "user@test.test",
                            EmailConfirmed = false,
                            FirstName = "userName",
                            LastName = "lastName",
                            LockoutEnabled = false,
                            NormalizedUserName = "USER@TEST.TEST",
                            PasswordHash = "AQAAAAIAAYagAAAAEFuYGp8RsBrsXsY3ArJ6riPWmM/hZF/AzGg8HBhcNubQitDAeo2mlgj/Fbf4+K6RWg==",
                            PhoneNumberConfirmed = false,
                            RegisteredOn = new DateTime(2020, 10, 10, 0, 0, 0, 0, DateTimeKind.Utc),
                            SecurityStamp = "17a0c33f-83d5-40ee-a450-d4311ea56ed9",
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
                            RoleId = new Guid("ff3405f7-9a3b-46d3-83d6-e2e3a147f8be"),
                            UserId = new Guid("0eddcd7a-b582-4637-ba9c-b1ed69cc361a")
                        },
                        new
                        {
                            RoleId = new Guid("b65ed1ed-7cbe-41c6-a24e-2e457fd51f58"),
                            UserId = new Guid("70f36e69-f7b9-42fc-9e77-a59f3fd9bfac")
                        });
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
