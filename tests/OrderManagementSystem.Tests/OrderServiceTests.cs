using System.Net.Http.Json;
using OrderManagementSystem.Api.DTO;

namespace OrderManagementSystem.Tests;

public class OrderServiceTests : IAsyncLifetime
{
    private CustomWebApplicationFactory _factory = null!;
    private HttpClient _client = null!;

    public async Task InitializeAsync()
    {
        _factory = new CustomWebApplicationFactory();
        _client = _factory.CreateClient();
    }

    public async Task DisposeAsync()
    {
        _client?.Dispose();
        _factory?.Dispose();
    }

    [Fact]
    public async Task TestUserCreateAccount()
    {
        //Arrange
        var loginData = new { Email = "test@example.com", Password = "password123" };

        //Act
        var response = await _client.PostAsJsonAsync("/api/auth/register", loginData);

        //Assert
        var result = await response.Content.ReadFromJsonAsync<AuthResponse>();

        Assert.NotNull(result);
        Assert.NotEmpty(result.Token);
        Assert.Equal(loginData.Email, result.Email);
        Assert.NotEmpty(result.HashPassword);
    }

    [Fact]
    public void TestUserLogin()
    {

    }

    [Fact]
    public void TestUserLogout()
    {

    }

    [Fact]
    public void TestUserAccountDelete()
    {

    }

    [Fact]
    public void GetUserInfo()
    {

    }

    [Fact]
    public void UpdateUserInfo()
    {

    }
}
