using System.Reflection.Emit;
using EventmiWorkshop.Data;
using EventmiWorkshopMVC.Services.Data;
using EventmiWorkshopMVC.Services.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventmiWorkShopMVC.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string connectionString = builder.Configuration.GetConnectionString("Default");

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<EventmiDbContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddScoped<IEventService, EventService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            using IServiceScope score = app.Services.CreateScope();
            EventmiDbContext db = score.ServiceProvider.GetRequiredService<EventmiDbContext>();
            await db.Database.MigrateAsync();
            

            await app.RunAsync();
        }
    }
}