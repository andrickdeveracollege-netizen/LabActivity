using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LabActivity.Model.Domain;
using LabActivity.Model.DTO;
using LabActivity.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace LabActivity.Service;

public class AuthService : IAuthService
{
    private static readonly PasswordHasher<User> _hasher = new();
    private readonly IUserRepository _users;
    private readonly IConfiguration _config;

    public AuthService(IUserRepository users, IConfiguration config)
    {
        _users = users;
        _config = config;
    }

    public async Task<RegisterResponseDto?> RegisterAsync(AuthRequestDto req)
    {
        if (await _users.ExistsAsync(req.Username)) return null;

        var user = new User
        {
            Username = req.Username,
            Role = (req.Username == "admin") ? "Admin" : "User"  
        };
        user.PasswordHash = _hasher.HashPassword(user, req.Password);

        await _users.AddAsync(user);
        return new RegisterResponseDto(user.Id, user.Username);
    }

    public async Task<AuthResponseDto?> LoginAsync(AuthRequestDto req)
    {
        var user = await _users.GetByUsernameAsync(req.Username);
        if (user is null) return null;

        var check = _hasher.VerifyHashedPassword(user, user.PasswordHash, req.Password);
        if (check == PasswordVerificationResult.Failed) return null;

        return new AuthResponseDto(CreateToken(user));
    }

    private string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(int.Parse(_config["Jwt:ExpiryMinutes"]!)),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}