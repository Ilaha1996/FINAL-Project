using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Web_AppointmentSystem.CORE.Entities;

namespace Web_AppointmentSystem.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.RegisterService();
            builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
     
            // Configure Session
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Session Middleware
            app.UseSession();

            // Authentication and Authorization Middleware
            app.UseAuthentication();
            app.UseAuthorization();

            // Map Area Routes
            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Auth}/{action=Login}/{id?}"
            );

            // Map Default Route
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
            );

            app.Run();
        }
    }
}
