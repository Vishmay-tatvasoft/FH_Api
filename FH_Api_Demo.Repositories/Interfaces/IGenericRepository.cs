namespace FH_Api_Demo.Repositories.Interfaces;

public interface IGenericRepository<T>
{
    Task AddRecord(T entity);

    Task<bool> SaveChangesAsync();
}
