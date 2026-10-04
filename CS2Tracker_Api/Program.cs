
using CS2Tracker_Api.Services;
using CS2Tracker_Core.Interfaces.IServices;
using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Context;
using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Interfaces.IServices;
using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CS2Tracker_Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Database
            var connectionString =
                builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<CS2TrackerContext>(options =>
                options.UseSqlServer(connectionString));

            // Application services
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IEntryService, EntryService>();
            builder.Services.AddScoped<ISheetService, SheetService>();

            // External APIs
            builder.Services.AddHttpClient<ICSFloatService, CSFloatService>();
            builder.Services.AddHttpClient<ICurrencyService, CurrencyService>();

            // API
            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            // JWT Key setup
            var jwtKey = builder.Configuration["Jwt:Key"];
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtKey!))
                    };
                });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}