using LabActivity.Model.Domain;
using Microsoft.EntityFrameworkCore;

namespace LabActivity.Model.DATA;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<User>().HasIndex(u => u.Username).IsUnique();
        b.Entity<Product>().Property(p => p.Price).HasPrecision(18, 2);
    }
}