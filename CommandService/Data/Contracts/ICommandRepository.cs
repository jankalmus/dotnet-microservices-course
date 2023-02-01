using CommandService.Model;
using Infrastructure.Data.Contracts.Repository;

namespace CommandService.Data.Contracts;

public interface ICommandRepository : IRepositoryBase<Command>
{
    bool PlatformExists(int platformId);

    IEnumerable<Command> PlatformCommands(int platformId);
}