using OrderManagementSystem.Api.DTO;

namespace OrderManagementSystem.Api.Interfaces;

/// <summary>
/// Контракт бізнес-логіки авторизації та реєстрації.
/// Виноситься в інтерфейс, щоб контролер залежав від абстракції,
/// а реалізацію можна було підміняти в тестах.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Створює акаунт користувача та повертає дані для клієнта.
    /// </summary>
    /// <param name="request">Вхідні дані реєстрації.</param>
    /// <param name="cancellationToken">Токен скасування запиту.</param>
    /// <returns>Результат реєстрації з токеном та технічними полями.</returns>
    Task<AuthResponse> RegisterAsync(LoginRequest request, CancellationToken cancellationToken = default);
}
