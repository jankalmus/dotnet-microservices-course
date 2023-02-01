using CommandService.Data.Contracts;
using CommandService.Model;
using Infrastructure.Data.Contracts.Repository;

namespace CommandService.Data.Repository;

public class CommandRepository : BaseRepository<Command, AppDbContext>, ICommandRepository
{
    public CommandRepository(AppDbContext context) : base(context) { }
    
    public bool PlatformExists(int platformId)
    {
        return _context.Platforms.Any(item => item.Id == platformId); 
    }

    public IEnumerable<Command> PlatformCommands(int platformId)
    {
        return _context.Commands.Where(c => c.Platform.Id == platformId); 
    }
}