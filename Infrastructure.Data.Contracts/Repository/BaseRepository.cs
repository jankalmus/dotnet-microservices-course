using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Contracts.Repository;

public class BaseRepository<TEntity, TDbContext> : IRepositoryBase<TEntity>
    where TEntity : EntityBase
    where TDbContext : DbContext
{
    protected readonly TDbContext _context;

    public BaseRepository(TDbContext context)
    {
        _context = context;
    }
    
    public void SaveChanges()
    {
        _context.SaveChanges();
    }

    public TEntity? Get(int id)
    {
       return _context.Set<TEntity>().FirstOrDefault(e => e.Id == id); 
    }

    public IEnumerable<TEntity> GetAll()
    {
        return _context.Set<TEntity>().ToList(); 
    }

    public TEntity Save(TEntity entity)
    {
        if (entity is null) throw new ArgumentNullException(nameof(entity));
        
        var result = _context.Set<TEntity>().Add(entity);

        return result.Entity;
    }

    public TEntity Update(TEntity entity)
    {
        if (entity is null) throw new ArgumentNullException(nameof(entity));
        
        var result = _context.Set<TEntity>().Update(entity);

        return result.Entity;
    }

    public void Delete(TEntity entity)
    {
        if (entity is null) throw new ArgumentNullException(nameof(entity));
        
        _context.Set<TEntity>().Remove(entity);
    }
}