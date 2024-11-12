using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web_AppointmentSystem.CORE.Entities;

namespace Web_AppointmentSystem.DATA.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.Property(x => x.Rating)
                   .IsRequired();

            builder.Property(x => x.Comment)
                   .IsRequired()
                   .HasMaxLength(1000); 

            builder.HasOne(x => x.User)
                   .WithMany(a => a.Reviews)  
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Cascade); 

        }
    }
}
