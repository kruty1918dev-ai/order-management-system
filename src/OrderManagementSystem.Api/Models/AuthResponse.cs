namespace OrderManagementSystem.Api.DTO;

/// <summary>
/// DTO відповіді після успішної реєстрації.
/// Використовується клієнтом для отримання базових даних доступу.
/// </summary>
public class AuthResponse
{
    /// <summary>
    /// Технічний токен доступу.
    /// </summary>
    public string Token { get; set; } = string.Empty;

    /// <summary>
    /// Email користувача, який був зареєстрований.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Хеш пароля. Повертається лише для навчального сценарію тесту.
    /// У production API зазвичай не повертається клієнту.
    /// </summary>
    public string HashPassword { get; set; } = string.Empty;
}