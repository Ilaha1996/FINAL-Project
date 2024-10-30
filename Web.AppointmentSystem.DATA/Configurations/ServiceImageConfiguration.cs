using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Web_AppointmentSystem.CORE.Entities;

namespace Web.AppointmentSystem.DATA.Configurations;

public class ServiceImageConfiguration : IEntityTypeConfiguration<ServiceImage>
{
    public void Configure(EntityTypeBuilder<ServiceImage> builder)
    {
        builder.Property(x => x.ImageUrl).IsRequired().HasMaxLength(100);
    }
}

