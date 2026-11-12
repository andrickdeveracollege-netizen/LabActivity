using LabActivity.Model.DATA;
using LabActivity.Model.Domain;
using Microsoft.EntityFrameworkCore;

namespace LabActivity.Repository;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;
    public UserRepository(AppDbContext db) => _db = db;

    public Task<User?> GetByUsernameAsync(string username) =>
        _db.Users.FirstOrDefaultAsync(u => u.Username == username);

    public Task<bool> ExistsAsync(string username) =>
        _db.Users.AnyAsync(u => u.Username == username);

    public async Task<User> AddAsync(User user)
    {
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return user;
    }
}