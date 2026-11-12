using LabActivity.Model.DTO;

namespace LabActivity.Service;

public interface IProductService
{
    Task<List<ProductDto>> GetAllAsync();
    Task<ProductDto?> GetByIdAsync(int id);
    Task<ProductDto> CreateAsync(ProductRequestDto dto);
    Task<ProductDto?> UpdateAsync(int id, ProductRequestDto dto);
    Task<bool> DeleteAsync(int id);
}