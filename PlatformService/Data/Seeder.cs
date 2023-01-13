using PlatformService.Models;

namespace PlatformService.Data;

public static class Seeder
{
    public static void SeedDatabase(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        
        SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
    }

    private static void SeedData(AppDbContext? context)
    {
        if (context is null) throw new ArgumentNullException(nameof(context));

        if (context.Platforms.Any()) return;

        var platforms = new List<Platform>()
        {
            new Platform()
            {
                Name = "Platform A",
                Cost = "153.29",
                Publisher = "Oracle Inc."
            },
            new Platform()
            {
                Name = "Platform B",
                Cost = "143.1",
                Publisher = "IBM"
            },
            new Platform()
            {
                Name = "Platform C",
                Cost = "13.35",
                Publisher = "Microsoft"
            },
            new Platform()
            {
                Name = "Platform D",
                Cost = "15.29",
                Publisher = "Apple"
            }
        };
        
        context.Platforms.AddRange(platforms);
        
        context.SaveChanges(); 
    }
}