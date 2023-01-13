using PlatformService.Data.Contracts;
using PlatformService.Models;

namespace PlatformService.Data.Repository;

public class PlatformRepository : IPlatformRepository
{
    private readonly AppDbContext _context;

    public PlatformRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public void SaveChanges()
    {
        throw new NotImplementedException();
    }

    public Platform Get(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Platform> GetAll()
    {
        throw new NotImplementedException();
    }

    public Platform Save(Platform entity)
    {
        throw new NotImplementedException();
    }

    public Platform Update()
    {
        throw new NotImplementedException();
    }

    public void Delete()
    {
        throw new NotImplementedException();
    }
}