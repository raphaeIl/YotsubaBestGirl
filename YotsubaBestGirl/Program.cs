using Microsoft.Extensions.Configuration;
using Serilog.Events;
using Serilog;
using YotsubaBestGirl.SDKServer;

namespace YotsubaBestGirl
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(AppContext.BaseDirectory)!)
                .AddJsonFile("appsettings.json")
                .AddJsonFile(
                    $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json",
                    true
                )
                .AddJsonFile("appsettings.Local.json", true)
                .Build();

            {
                var logFilePath = Path.Combine(
                    Path.GetDirectoryName(AppContext.BaseDirectory)!,
                    "logs",
                    "log.txt"
                );

                if (File.Exists(logFilePath))
                {
                    var prevLogFilePath = Path.Combine(
                        Path.GetDirectoryName(logFilePath)!,
                        "log-prev.txt"
                    );
                    if (File.Exists(prevLogFilePath))
                        File.Delete(prevLogFilePath);

                    File.Move(logFilePath, prevLogFilePath);
                }

                Log.Logger = new LoggerConfiguration()
                    .WriteTo.Console()
                    .WriteTo.File(
                        logFilePath,
                        restrictedToMinimumLevel: LogEventLevel.Verbose,
                        shared: true
                    )
                    .ReadFrom.Configuration(config)
                    .CreateBootstrapLogger();
            }

            GameServer.GameServer.Main(args);
        }
    }
}