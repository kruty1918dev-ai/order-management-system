using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OrderManagementSystem.Api.Data;

namespace OrderManagementSystem.Tests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
	private readonly SqliteConnection _connection = new("DataSource=:memory:");

	protected override void ConfigureWebHost(IWebHostBuilder builder)
	{
		_connection.Open();

		builder.ConfigureServices(services =>
		{
			services.RemoveAll<DbContextOptions<AppDbContext>>();

			services.AddDbContext<AppDbContext>(options =>
				options.UseSqlite(_connection));

			using var scope = services.BuildServiceProvider().CreateScope();
			var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
			dbContext.Database.EnsureCreated();
		});
	}

	protected override void Dispose(bool disposing)
	{
		base.Dispose(disposing);
		_connection.Dispose();
	}
}
