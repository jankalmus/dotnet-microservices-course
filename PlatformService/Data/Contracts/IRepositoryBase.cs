using PlatformService.Models;

namespace PlatformService.Data.Contracts;

public interface IRepositoryBase<out TEntity> where TEntity : EntityBase
{
    void SaveChanges(); 
    
    TEntity Get(); 
    
    TEntity Save();

    TEntity Update();

    void Delete();
}