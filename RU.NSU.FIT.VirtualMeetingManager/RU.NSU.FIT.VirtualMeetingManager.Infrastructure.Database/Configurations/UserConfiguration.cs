using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RU.NSU.FIT.VirtualManager.Domain.Entities;

namespace RU.NSU.FIT.VirtualMeetingManager.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.HasMany(u => u.RefreshTokens)
            .WithOne(r => r.Owner);

        builder.HasMany(u => u.Roles)
            .WithMany()
            .UsingEntity<UserRole>(cfg =>
            {
                cfg.ToTable("UserRoles");

                cfg.HasKey(x => new {x.RoleId, x.UserId});
            });
    }
}