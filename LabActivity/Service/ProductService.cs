using LabActivity.Mapper;
using LabActivity.Model.DTO;
using LabActivity.Repository;

namespace LabActivity.Service;

public class ProductService : IProductService
{
    private readonly IProductRepository _repo;
    public ProductService(IProductRepository repo) => _repo = repo;

    public async Task<List<ProductDto>> GetAllAsync() =>
        (await _repo.GetAllAsync()).Select(p => p.ToDto()).ToList();

    public async Task<ProductDto?> GetByIdAsync(int id) =>
        (await _repo.GetByIdAsync(id))?.ToDto();

    public async Task<ProductDto> CreateAsync(ProductRequestDto dto) =>
        (await _repo.AddAsync(dto.ToDomain())).ToDto();

    public async Task<ProductDto?> UpdateAsync(int id, ProductRequestDto dto) =>
        (await _repo.UpdateAsync(id, dto.ToDomain()))?.ToDto();

    public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);
}