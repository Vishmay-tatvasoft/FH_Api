using FH_Api_Demo.Repositories.Data;
using FH_Api_Demo.Repositories.Interfaces;
using FH_Api_Demo.Repositories.Models;
using Microsoft.EntityFrameworkCore;

namespace FH_Api_Demo.Repositories.Implementations;

public class UserRepository(TatvasoftFhContext context) : IUserRepository
{
    #region Configuration Settings
    private readonly TatvasoftFhContext _context = context;
    #endregion

    #region Get User By Email Async
    public async Task<FhUser?> GetUserByUsername(string userName)
    {
        return await _context.FhUsers.FirstOrDefaultAsync(u => u.UserName == userName);
    }
    #endregion

}
