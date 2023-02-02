using CommandService.Data.Contracts;
using CommandService.DataServices.gRPC;
using CommandService.Model;

namespace CommandService.Data;

public static class DatabaseInitializer
{
    public static void PopulatePlatforms(this IApplicationBuilder applicationBuilder)
    {
        using (var scope = applicationBuilder.ApplicationServices.CreateScope())
        {
            var client = scope.ServiceProvider.GetService<IPlatformDataClient>();

            if (client is null) throw new NullReferenceException("Failed to initialize gRPC client."); 
            
            var platforms = client.ReturnAllPlatforms();

            var repository = scope.ServiceProvider.GetService<IPlatformRepository>();

            if (repository is null) throw new NullReferenceException("Failed to initialize platforms repository.");
            
            SeedPlatforms(repository, platforms);
        }
    }

    private static void SeedPlatforms(IPlatformRepository repository, IEnumerable<Platform> platforms)
    {
        Console.WriteLine("INFO: Seeding platforms based on gRPC response.");

        var addedCount = 0; 

        foreach (var platform in platforms)
        {
            if (repository.ExternalPlatformExists(platform.ExternalId)) continue;

            repository.Save(platform);
            repository.SaveChanges();
            addedCount++; 
        }
        
        Console.WriteLine($"INFO: Platform seeding complete. {addedCount} platforms were added.");
    }
}