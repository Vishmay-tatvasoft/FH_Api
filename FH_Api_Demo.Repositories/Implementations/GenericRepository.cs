using FH_Api_Demo.Repositories.Data;
using FH_Api_Demo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FH_Api_Demo.Repositories.Implementations;

public class GenericRepository<T>(TatvasoftFhContext context) : IGenericRepository<T>
    where T : class
{
    protected readonly TatvasoftFhContext _context = context;
    protected readonly DbSet<T> _dbSet = context.Set<T>();

    #region Add Record Async
    public async Task AddRecord(T entity)
    {
        await _dbSet.AddAsync(entity);
    }
    #endregion

    #region Save Changes Async
    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
    #endregion 
}
