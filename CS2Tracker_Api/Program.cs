
using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Interfaces.IServices;
using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Services;

namespace CS2Tracker_Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            // Your application services
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IEntryService, EntryService>();
            builder.Services.AddScoped<ISheetService, SheetService>();

            builder.Services.AddHttpClient<ICSFloatService, CSFloatService>();
            builder.Services.AddHttpClient<ICurrencyService, CurrencyService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
