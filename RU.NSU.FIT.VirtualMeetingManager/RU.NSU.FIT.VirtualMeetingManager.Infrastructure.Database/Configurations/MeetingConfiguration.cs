using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RU.NSU.FIT.VirtualManager.Domain.Constants;
using RU.NSU.FIT.VirtualManager.Domain.Entities;

namespace RU.NSU.FIT.VirtualMeetingManager.Configurations;

public class MeetingConfiguration : IEntityTypeConfiguration<Meeting>
{
    public void Configure(EntityTypeBuilder<Meeting> builder)
    {
        builder.ToTable("Meetings");
        builder.HasKey(m => m.Id);
        builder
            .HasMany(m => m.Users)
            .WithMany(u => u.Meetings)
            .UsingEntity(cfg => { cfg.ToTable("UserMeetings"); });
        builder
            .HasOne(m => m.Manager)
            .WithMany();
        builder
            .Property(m => m.Name)
            .HasMaxLength(EntityConstants.Meeting.Name.Max);
        builder
            .Property(m => m.Description)
            .HasMaxLength(EntityConstants.Meeting.Description.Max);
        builder
            .Property(m => m.ShortDescription)
            .HasMaxLength(EntityConstants.Meeting.ShortDescription.Max);
    }
}