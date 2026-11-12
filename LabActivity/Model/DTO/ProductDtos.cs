namespace LabActivity.Model.DTO;

public record ProductRequestDto(string Name, decimal Price);
public record ProductDto(int Id, string Name, decimal Price);