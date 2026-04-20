using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Api.DTO;
using OrderManagementSystem.Api.Interfaces;

namespace OrderManagementSystem.Api.Controllers;

/// <summary>
/// HTTP-контролер для сценаріїв авторизації.
/// На поточному кроці реалізовано лише реєстрацію,
/// що потрібна для інтеграційного тесту TestUserCreateAccount.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Реєструє користувача та повертає DTO з токеном.
    /// </summary>
    /// <param name="request">Вхідні дані реєстрації.</param>
    /// <param name="cancellationToken">Токен скасування HTTP-запиту.</param>
    /// <returns>Успішна відповідь з AuthResponse.</returns>
    [HttpPost("register")]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AuthResponse>> Register(
        [FromBody] LoginRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _authService.RegisterAsync(request, cancellationToken);
        return Ok(result);
    }
}
