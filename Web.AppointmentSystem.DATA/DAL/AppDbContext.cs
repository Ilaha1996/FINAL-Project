using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Web_AppointmentSystem.CORE.Entities;
using Web_AppointmentSystem.DATA.Configurations;

namespace Web.AppointmentSystem.DATA.DAL;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Service> Services { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<TimeSlot> TimeSlots { get; set; }
    public DbSet<AppUser> Users { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<FavoriteService> FavoriteServices { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ServiceConfiguration).Assembly);
        base.OnModelCreating(modelBuilder);
    }

}
