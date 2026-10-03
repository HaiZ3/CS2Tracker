using FormsApp_CS2_Arbitrage_And_Investment_Tracker.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

public class CS2TrackerContextFactory : IDesignTimeDbContextFactory<CS2TrackerContext>
{
    public CS2TrackerContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();

        var connectionString = config.GetConnectionString("DefaultConnection");

        var optionsBuilder = new DbContextOptionsBuilder<CS2TrackerContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new CS2TrackerContext(optionsBuilder.Options);
    }
}