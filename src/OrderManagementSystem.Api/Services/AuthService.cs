using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Api.Data;
using OrderManagementSystem.Api.DTO;
using OrderManagementSystem.Api.Interfaces;
using OrderManagementSystem.Api.Models;

namespace OrderManagementSystem.Api.Services;

/// <summary>
/// Реалізація бізнес-логіки реєстрації користувача.
/// На поточному етапі не працює з БД, але імітує реальну поведінку:
/// - хешує пароль
/// - генерує токен
/// - повертає DTO відповіді
/// </summary>
public class AuthService : IAuthService
{
    private readonly AppDbContext _dbContext;
    private readonly PasswordHasher<object> _passwordHasher = new();

    public AuthService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <inheritdoc />
    public async Task<AuthResponse> RegisterAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        var normalizedEmail = request.Email.Trim().ToLowerInvariant();

        var userExists = await _dbContext.Users
            .AnyAsync(x => x.Email == normalizedEmail, cancellationToken);

        if (userExists)
        {
            throw new InvalidOperationException("Користувач з таким email вже існує.");
        }

        // Хешування виконується стандартним PasswordHasher з ASP.NET Core,
        // щоб не реалізовувати криптографію вручну.
        var hashedPassword = _passwordHasher.HashPassword(new object(), request.Password);

        // Для прикладу та поточних тестів створюємо технічний токен доступу.
        var token = GenerateOpaqueToken();

        var user = new UserAccount
        {
            Email = normalizedEmail,
            PasswordHash = hashedPassword,
            CreatedAtUtc = DateTime.UtcNow
        };

        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var response = new AuthResponse
        {
            Token = token,
            Email = normalizedEmail,
            HashPassword = hashedPassword
        };

        return response;
    }

    private static string GenerateOpaqueToken()
    {
        var randomBytes = RandomNumberGenerator.GetBytes(32);
        return Convert.ToBase64String(randomBytes);
    }
}
