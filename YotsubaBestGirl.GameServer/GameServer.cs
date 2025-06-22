using System.Reflection;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Serilog;
using YotsubaBestGirl.GameServer.Controllers.Api.ProtocolHandlers;
using YotsubaBestGirl.GameServer.Services;
using Microsoft.EntityFrameworkCore;
using YotsubaBestGirl.Database;

namespace YotsubaBestGirl.GameServer
{
    public class GameServer
    {
        public static void Main(string[] args)
        {
            Log.Information("Starting GameServer...");

            try
            {
                var builder = WebApplication.CreateBuilder(args);

                builder.Services.Configure<KestrelServerOptions>(op =>
                    op.AllowSynchronousIO = true
                );
                builder.Host.UseSerilog();

                builder.Services.AddControllers();
                builder.Services.AddProtocolHandlerFactory();
                builder.Services.AddControllers().AddApplicationPart(Assembly.GetAssembly(typeof(GameServer)));
                builder.Services.AddResourceService();
                builder.Services.AddSessionService();

                // postgresql db
                builder.Services.AddDbContext<YotsubaContext>(options =>
                {
                    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
                });

                // sqlite db, add support later
                //builder.Services.AddDbContext<YotsubaContext>(options =>
                //{
                //    options.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection"));
                //});


                // Add all Handler Groups
                var handlerGroups = Assembly.GetAssembly(typeof(ProtocolHandlerFactory))
                    .GetTypes()
                    .Where(t => t.IsSubclassOf(typeof(ProtocolHandlerBase)));

                foreach (var handlerGroup in handlerGroups)
                {
                    builder.Services.AddProtocolHandlerGroupByType(handlerGroup);
                }

                var app = builder.Build();

                app.UseAuthorization();
                app.UseSerilogRequestLogging();

                app.MapControllers();

                using (var scope = app.Services.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<YotsubaContext>();
                    db.Database.EnsureCreated(); // create dbs
                }

                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "An unhandled exception occurred during runtime");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
