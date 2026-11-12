using LabActivity.Model.Domain;

namespace LabActivity.Repository;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task<Product> AddAsync(Product product);
    Task<Product?> UpdateAsync(int id, Product input);
    Task<bool> DeleteAsync(int id);
}