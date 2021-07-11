using LupExercise_ArchieGonzales.Config;
using LupExercise_ArchieGonzales.DAL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LupExercise_ArchieGonzales
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            try
            {
                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;


                    var context = services.GetRequiredService<ApplicationDbContext>();
                    context.Database.Migrate();

                    // requires using Microsoft.Extensions.Configuration;
                    var config = host.Services.GetRequiredService<IConfiguration>();
                    SeedData.InitializeAsync(services).Wait();
                }

                host.Run();
            }
            catch (Exception ex)
            {
                //logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                //NLog.LogManager.Shutdown();
            }

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
