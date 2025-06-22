using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace YotsubaBestGirl.Database
{
    public class YotsubaContextFactory : IDesignTimeDbContextFactory<YotsubaContext>
    {
        public YotsubaContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(AppContext.BaseDirectory)!)
                .AddJsonFile("appsettings.json")
                .AddJsonFile(
                    $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json",
                    true)
                .AddJsonFile("appsettings.Local.json", true)
                .Build();
            
            var connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<YotsubaContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new YotsubaContext(optionsBuilder.Options);
        }
    }
}
