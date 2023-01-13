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
        _context.SaveChanges(); 
    }

    public Platform Get(int id)
    {
        return _context.Platforms.First(item => item.Id == id); 
    }

    public IEnumerable<Platform> GetAll()
    {
        return _context.Platforms.ToList(); 
    }

    public Platform Save(Platform entity)
    {
        if (entity is null) throw new ArgumentNullException(nameof(entity));

        _context.Platforms.Add(entity);

        return entity; 
    }

    public Platform Update(Platform entity)
    {
        if (entity is null) throw new ArgumentNullException(nameof(entity));

        _context.Platforms.Update(entity); 
        
        return entity; 
    }

    public void Delete(Platform entity)
    {
        if (entity is null) throw new ArgumentNullException(nameof(entity));

        _context.Platforms.Remove(entity);
    }
}