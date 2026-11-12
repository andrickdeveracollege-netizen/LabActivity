using LabActivity.Model.DTO;

namespace LabActivity.Service;

public interface IAuthService
{
    Task<RegisterResponseDto?> RegisterAsync(AuthRequestDto req);
    Task<AuthResponseDto?> LoginAsync(AuthRequestDto req);        
}