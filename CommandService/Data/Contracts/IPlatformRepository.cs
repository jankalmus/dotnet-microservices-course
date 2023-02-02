using CommandService.Model;
using Infrastructure.Data.Contracts.Repository;

namespace CommandService.Data.Contracts;

public interface IPlatformRepository : IRepositoryBase<Platform>
{
    bool PlatformExists(int platformId);
}