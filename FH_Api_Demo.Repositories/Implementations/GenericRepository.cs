using FH_Api_Demo.Repositories.Data;
using FH_Api_Demo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FH_Api_Demo.Repositories.Implementations;

public class GenericRepository<T>(TatvasoftFhContext context) : IGenericRepository<T>
    where T : class
{
    protected readonly TatvasoftFhContext _context = context;
    protected readonly DbSet<T> _dbSet = context.Set<T>();

    #region Get Record By ID
    public async Task<T?> GetRecordById(string id)
    {
        return await _dbSet.FindAsync(id);
    }
    #endregion

    #region Add Record
    public async Task AddRecord(T entity)
    {
        await _dbSet.AddAsync(entity);
    }
    #endregion

    #region Update Record
    public void UpdateRecord(T entity)
    {
        _dbSet.Update(entity);
    }
    #endregion   

    #region Save Changes To Db
    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
    #endregion 
}
