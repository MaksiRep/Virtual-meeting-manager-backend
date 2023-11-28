using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RU.NSU.FIT.VirtualManager.Domain.Constants;

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
            .HasMaxLength(EntityConstants.MAX_MEETING_NAME_SIZE);
        builder
            .Property(m => m.Description)
            .HasMaxLength(EntityConstants.MAX_MEETING_DESCRIPTION_SIZE);
        builder
            .Property(m => m.ShortDescription)
            .HasMaxLength(EntityConstants.MAX_SHORT_DESCRIPTION_SIZE);
    }
}