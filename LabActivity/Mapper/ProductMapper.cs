using LabActivity.Model.Domain;
using LabActivity.Model.DTO;

namespace LabActivity.Mapper;

public static class ProductMapper
{
    public static ProductDto ToDto(this Product p) => new(p.Id, p.Name, p.Price);

    public static Product ToDomain(this ProductRequestDto d) =>
        new() { Name = d.Name, Price = d.Price };
}