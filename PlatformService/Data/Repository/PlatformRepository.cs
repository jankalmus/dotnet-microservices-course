using Infrastructure.Data.Contracts.Repository;
using PlatformService.Data.Contracts;
using PlatformService.Models;

namespace PlatformService.Data.Repository;

public class PlatformRepository : BaseRepository<Platform, AppDbContext>, IPlatformRepository
{
    public PlatformRepository(AppDbContext context) : base(context) { }
}