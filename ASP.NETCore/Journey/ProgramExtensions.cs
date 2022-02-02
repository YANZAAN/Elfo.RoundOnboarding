using System;
using Journey.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Journey
{
    public static class ProgramExtensions
    {
        public static IHost SeedDatabase(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            
            var provider = scope.ServiceProvider;
            var context = provider.GetRequiredService<JourneyDbContext>();
            var seeder = new DataSeed(context);

            try
            {
                seeder.SeedDatabase();
            }
            catch (Exception)
            {
                // log
                throw;
            }

            return host;
        }
    }
}