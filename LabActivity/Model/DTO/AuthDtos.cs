namespace LabActivity.Model.DTO;

public record AuthRequestDto(string Username, string Password);
public record RegisterResponseDto(int Id, string Username);
public record AuthResponseDto(string Token);