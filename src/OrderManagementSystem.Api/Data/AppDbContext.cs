using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Api.Models;

namespace OrderManagementSystem.Api.Data;

/// <summary>
/// Контекст доступу до SQL-бази через EF Core.
/// Містить таблиці та правила мапінгу сутностей.
/// </summary>
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Таблиця користувачів системи.
    /// </summary>
    public DbSet<UserAccount> Users => Set<UserAccount>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.ToTable("Users");
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(320);

            entity.Property(x => x.PasswordHash)
                .IsRequired();

            entity.Property(x => x.CreatedAtUtc)
                .IsRequired();

            // Унікальний email захищає від дублювання акаунтів на рівні БД.
            entity.HasIndex(x => x.Email)
                .IsUnique();
        });
    }
}
