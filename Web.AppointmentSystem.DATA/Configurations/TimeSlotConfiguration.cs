using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web_AppointmentSystem.CORE.Entities;

namespace Web_AppointmentSystem.DATA.Configurations
{
    public class TimeSlotConfiguration : IEntityTypeConfiguration<TimeSlot>
    {
        public void Configure(EntityTypeBuilder<TimeSlot> builder)
        {
            builder.Property(x => x.Date)
                   .IsRequired();

            builder.Property(x => x.StartTime)
                   .IsRequired();

            builder.Property(x => x.IsAvailable)
                   .IsRequired();
       
            builder.HasIndex(x => new { x.Date, x.StartTime })
                   .IsUnique();
        }
    }
}
