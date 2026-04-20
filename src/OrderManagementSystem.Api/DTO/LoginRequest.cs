namespace OrderManagementSystem.Api.DTO;

/// <summary>
/// DTO запиту на реєстрацію користувача.
/// Містить лише дані, які клієнт має передати для створення акаунта.
/// </summary>
public class LoginRequest
{
    /// <summary>
    /// Email користувача. Використовується як унікальний логін.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Сирий пароль від клієнта. У відповіді не повертається у відкритому вигляді.
    /// </summary>
    public string Password { get; set; } = string.Empty;
}
