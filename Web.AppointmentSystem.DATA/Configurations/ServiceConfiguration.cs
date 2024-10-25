using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web_AppointmentSystem.CORE.Entities;

namespace Web_AppointmentSystem.DATA.Configurations;

public class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(x => x.Description)
               .IsRequired(false)
               .HasMaxLength(550);

        builder.Property(x => x.Price)
               .IsRequired()
               .HasColumnType("decimal(18,2)");

        builder.Property(x => x.Duration)
               .IsRequired();
    }
}
