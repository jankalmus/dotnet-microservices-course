namespace Infrastructure.Data.Contracts.Repository;

public interface IRepositoryBase<TEntity> where TEntity : EntityBase
{
    void SaveChanges(); 
    
    TEntity? Get(int id);
    
    IEnumerable<TEntity> GetAll(); 

    TEntity Save(TEntity entity);

    TEntity Update(TEntity entity);

    void Delete(TEntity entity);
}