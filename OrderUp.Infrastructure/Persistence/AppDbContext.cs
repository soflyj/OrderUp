using Microsoft.EntityFrameworkCore;
using OrderUp.Domain.Entities;

namespace OrderUp.Infrastructure.Persistence
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Tenant> Tenants { get; set; }

    public DbSet<Baker> Bakers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<InventoryItem> Inventories { get; set; }

    // Assuming RequestLog is your logging entity
    public DbSet<LogEntry> RequestLogs { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      // Configure Tenant
      modelBuilder.Entity<Tenant>(entity =>
      {
        entity.HasKey(t => t.Id);
        entity.Property(t => t.Name).IsRequired().HasMaxLength(100);
      });

      // Configure User
      modelBuilder.Entity<User>()
         .HasOne(u => u.Tenant)
         .WithMany(t => t.Users)
         .HasForeignKey(u => u.TenantId)
         .OnDelete(DeleteBehavior.Restrict); // or .NoAction in EF Core 5+

      // Configure RequestLog
      modelBuilder.Entity<LogEntry>(entity =>
      {
        entity.HasKey(r => r.Id);
        entity.Property(r => r.TableName).IsRequired().HasMaxLength(100);
        entity.Property(r => r.RecordId).IsRequired();
      });

      // 🔁 Global DeleteBehavior setting
      foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                   .SelectMany(e => e.GetForeignKeys()))
      {
        relationship.DeleteBehavior = DeleteBehavior.Restrict;
      }

      modelBuilder.Entity<Tenant>().HasData(
          new Tenant
          {
            Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
            Name = "DefaultTenant"
          }
      );

      modelBuilder.Entity<User>().HasData(
          new User
          {
            Id = Guid.Parse("7e841df0-aadd-4f7c-9d74-3d5bc43dd869"),
            TenantId = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
            Username = "Stevie",
            Email = "jar.ninja.nas@gmail.com",
            PasswordHash = "$2a$04$WkevAwqPEYqqXCOYJ4bxReOBihf7ZfwephUTyRfjwMr43bssuzxpm", // Use a proper hashing mechanism
            IsEmailConfirmed = true,
            EmailConfirmationToken = "TS3dagLkwuCxiIEdTl0cQxVZ3HdcDGtWZBBwe4gm94zyjq4ZimFzdSXZvRRzGqlO",
            Role = Domain.Enums.UserRole.Admin,
            CreatedAt = new DateTime(2025, 6, 1, 10, 30, 45),
            UpdatedAt = new DateTime(2025, 6, 1, 10, 30, 45)
          }
);
    }
  }
}
