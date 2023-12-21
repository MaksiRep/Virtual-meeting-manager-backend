using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RU.NSU.FIT.VirtualManager.Domain.Constants;
using RU.NSU.FIT.VirtualManager.Domain.Entities;

namespace RU.NSU.FIT.VirtualMeetingManager.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.FirstName)
            .HasMaxLength(EntityConstants.User.FirstName.Max);
        
        builder.Property(u => u.LastName)
            .HasMaxLength(EntityConstants.User.LastName.Max);
        
        builder.Property(u => u.Email)
            .HasMaxLength(EntityConstants.User.Email.Max);

        builder.Property(u => u.PhoneNumber)
            .HasMaxLength(EntityConstants.User.PhoneNumber.Max)
            .IsRequired(false);

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