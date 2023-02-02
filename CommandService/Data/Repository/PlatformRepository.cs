using CommandService.Data.Contracts;
using CommandService.Model;
using Infrastructure.Data.Contracts.Repository;

namespace CommandService.Data.Repository;

public class PlatformRepository : BaseRepository<Platform, AppDbContext>, IPlatformRepository
{
    public PlatformRepository(AppDbContext context) : base(context) { } 
    
    public bool PlatformExists(int platformId)
    {
        return _context.Platforms.Any(item => item.Id == platformId); 
    }
}