using LabActivity.Model.Domain;

namespace LabActivity.Repository;

public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string username);
    Task<bool> ExistsAsync(string username);
    Task<User> AddAsync(User user);
}