using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RU.NSU.FIT.VirtualManager.Domain.Auth;
using RU.NSU.FIT.VirtualManager.Domain.Entities;
using RU.NSU.FIT.VirtualManager.Domain.ValueTypes;

namespace RU.NSU.FIT.VirtualMeetingManager;

public static class DataSeeder
{
    public static void SeedInitialData(this ModelBuilder builder)
    {
        var mainAdminRoleId = new Guid("4b110a01-7935-483a-b127-1646acc211c7");
        var adminRoleId = new Guid("ff3405f7-9a3b-46d3-83d6-e2e3a147f8be");
        var userRoleId = new Guid("b65ed1ed-7cbe-41c6-a24e-2e457fd51f58");
        var mainAdminId = new Guid("630b2bdc-abd2-44eb-9b16-30025d98202e");
        var adminId = new Guid("0eddcd7a-b582-4637-ba9c-b1ed69cc361a");
        var userId = new Guid("70f36e69-f7b9-42fc-9e77-a59f3fd9bfac");

        var passwordHasher = new PasswordHasher<User>();
        
        builder.Entity<Role>().HasData(new Role {Id = mainAdminRoleId, Name = RoleConstants.MainAdmin, NormalizedName = "MAINADMIN"});
        builder.Entity<Role>().HasData(new Role {Id = adminRoleId, Name = RoleConstants.Admin, NormalizedName = "ADMIN"});
        builder.Entity<Role>().HasData(new Role {Id = userRoleId, Name = RoleConstants.User, NormalizedName = "USER"});

        var mainAdmin = new User(
            "mainAdminName", "lastName", "main@test.test",
            new DateOnly(2000, 1, 1), new DateTime(2020, 10, 10, 0, 0, 0, DateTimeKind.Utc), GenderType.Female)
        {
            UserName = "main@test.test",
            NormalizedUserName = "MAIN@TEST.TEST",
            Id = mainAdminId
        };
        mainAdmin.PasswordHash = passwordHasher.HashPassword(mainAdmin, "MainPassword");
        mainAdmin.SecurityStamp = Guid.NewGuid().ToString();
        
        var admin = new User(
            "adminName", "lastName", "admin@test.test",
            new DateOnly(2000, 1, 1), new DateTime(2020, 10, 10, 0, 0, 0, DateTimeKind.Utc), GenderType.Female)
        {
            UserName = "admin@test.test",
            NormalizedUserName = "ADMIN@TEST.TEST",
            Id = adminId
        };
        admin.PasswordHash = passwordHasher.HashPassword(admin, "AdminPassword");
        admin.SecurityStamp = Guid.NewGuid().ToString();

        var user = new User(
            "userName", "lastName", "user@test.test",
            new DateOnly(2000, 1, 1), new DateTime(2020, 10, 10, 0, 0, 0, DateTimeKind.Utc), GenderType.Female)
        {
            UserName = "user@test.test",
            NormalizedUserName = "USER@TEST.TEST",
            Id = userId
        };
        user.PasswordHash = passwordHasher.HashPassword(user, "UserPassword");
        user.SecurityStamp = Guid.NewGuid().ToString();
        
        builder.Entity<User>().HasData(mainAdmin);
        builder.Entity<User>().HasData(admin);
        builder.Entity<User>().HasData(user);

        builder.Entity<UserRole>().HasData(new UserRole(mainAdminId, mainAdminRoleId));
        
        builder.Entity<UserRole>().HasData(new UserRole(adminId, adminRoleId));
        
        builder.Entity<UserRole>().HasData(new UserRole(userId, userRoleId));
    }
}