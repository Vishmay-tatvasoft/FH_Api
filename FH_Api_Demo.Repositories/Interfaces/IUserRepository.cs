using FH_Api_Demo.Repositories.Models;

namespace FH_Api_Demo.Repositories.Interfaces;

public interface IUserRepository
{
    Task<FhUser?> GetUserByUsername(string userName);
}
