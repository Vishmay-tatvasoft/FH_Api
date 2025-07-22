namespace FH_Api_Demo.Repositories.Interfaces;

public interface IGenericRepository<T>
{
    Task<T?> GetRecordById(string id);
    Task AddRecord(T entity);
    void UpdateRecord(T entity);
    Task<bool> SaveChangesAsync();
}
