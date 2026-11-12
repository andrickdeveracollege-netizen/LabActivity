using LabActivity.Model.DATA;
using LabActivity.Model.Domain;
using Microsoft.EntityFrameworkCore;

namespace LabActivity.Repository;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _db;
    public ProductRepository(AppDbContext db) => _db = db;

    public Task<List<Product>> GetAllAsync() => _db.Products.ToListAsync();

    public async Task<Product?> GetByIdAsync(int id) => await _db.Products.FindAsync(id);

    public async Task<Product> AddAsync(Product product)
    {
        _db.Products.Add(product);
        await _db.SaveChangesAsync();
        return product;
    }

    public async Task<Product?> UpdateAsync(int id, Product input)
    {
        var p = await _db.Products.FindAsync(id);
        if (p is null) return null;
        p.Name = input.Name;
        p.Price = input.Price;
        await _db.SaveChangesAsync();
        return p;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var p = await _db.Products.FindAsync(id);
        if (p is null) return false;
        _db.Products.Remove(p);
        await _db.SaveChangesAsync();
        return true;
    }
}