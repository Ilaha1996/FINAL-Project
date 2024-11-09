using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web_AppointmentSystem.CORE.Entities;

namespace Web_AppointmentSystem.DATA.Configurations
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.Property(a => a.Notes)
                   .HasMaxLength(1000);

            builder.HasOne(a => a.User)
                   .WithMany(u => u.Appointments)
                   .HasForeignKey(a => a.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.Service)
                   .WithMany(s => s.Appointments)
                   .HasForeignKey(a => a.ServiceId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(a => a.Date)
                   .IsRequired();

            builder.Property(a => a.StartTime)
                   .IsRequired();

            builder.HasIndex(a => new { a.ServiceId, a.Date, a.StartTime })
                   .IsUnique();
        }
    }
}
