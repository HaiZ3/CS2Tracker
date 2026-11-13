using CS2Tracker_Core.Interfaces.IServices;
using CS2Tracker_Core.Services;
using CS2Tracker_Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static CS2Tracker_Common.Validations.ModelValidations;

namespace CS2Tracker_MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<CS2TrackerContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services
                .AddIdentity<IdentityUser, IdentityRole>(options =>
                {
                    options.Password.RequiredLength = UserPasswordMinLen;
                    options.Password.RequireDigit = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireLowercase = false;
                    options.User.RequireUniqueEmail = true;
                    options.Lockout.MaxFailedAccessAttempts = 5;
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
                })
                .AddEntityFrameworkStores<CS2TrackerContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IEntryService, EntryService>();
            builder.Services.AddScoped<ISheetService, SheetService>();
            builder.Services.AddScoped<IUserService,UserService>();

            // External APIs
            builder.Services.AddMemoryCache();

            builder.Services.AddHttpClient<IMarketplacePriceService, MarketplacePriceService>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Shared/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Accounts}/{action=Home}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
