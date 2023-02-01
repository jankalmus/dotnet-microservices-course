using CommandService.Model;
using Microsoft.EntityFrameworkCore;

namespace CommandService.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<Platform> Platforms { get; set; }
    
    public DbSet<Command> Commands { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Platform>()
                    .HasMany(e => e.Commands)
                    .WithOne(e => e.Platform)
                    .HasForeignKey(e => e.PlatformId);

        modelBuilder.Entity<Command>()
                    .HasOne(e => e.Platform)
                    .WithMany(e => e.Commands)
                    .HasForeignKey(e => e.PlatformId); 
    }
}