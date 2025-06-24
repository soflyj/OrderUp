// File: OrderUp.Infrastructure/Persistence/AppDbContext.cs
using Microsoft.EntityFrameworkCore;
using OrderUp.Domain.Entities;

namespace OrderUp.Infrastructure.Persistence
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<VendorType> VendorTypes => Set<VendorType>();
    public DbSet<Vendor> Vendors => Set<Vendor>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductImage> ProductImages => Set<ProductImage>();
    public DbSet<ProductInventoryItem> ProductInventoryItems => Set<ProductInventoryItem>();
    public DbSet<InventoryItem> InventoryItems => Set<InventoryItem>();
    public DbSet<GeneralInventoryItem> GeneralInventoryItems => Set<GeneralInventoryItem>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<Invoice> Invoices => Set<Invoice>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      // Disable cascade delete globally to prevent cycle issues
      foreach (var relationship in modelBuilder.Model
                   .GetEntityTypes()
                   .SelectMany(e => e.GetForeignKeys()))
      {
        relationship.DeleteBehavior = DeleteBehavior.Restrict;
      }

      // Fix for decimal properties
      modelBuilder.Entity<Product>()
          .Property(p => p.Price)
          .HasPrecision(18, 2); // Up to 999,999,999,999.99

      modelBuilder.Entity<InventoryItem>()
          .Property(i => i.Cost)
          .HasPrecision(18, 2);

      // ProductInventoryItem (join table)
      modelBuilder.Entity<ProductInventoryItem>()
          .HasOne(p => p.Product)
          .WithMany(p => p.RequiredInventory)
          .HasForeignKey(p => p.ProductId);

      // OrderItem
      modelBuilder.Entity<OrderItem>()
          .HasOne(oi => oi.Order)
          .WithMany(o => o.OrderItems)
          .HasForeignKey(oi => oi.OrderId);

      modelBuilder.Entity<OrderItem>()
          .HasOne(oi => oi.Product)
          .WithMany()
          .HasForeignKey(oi => oi.ProductId);

      // ProductImage
      modelBuilder.Entity<ProductImage>()
          .HasOne(pi => pi.Product)
          .WithMany(p => p.Images)
          .HasForeignKey(pi => pi.ProductId);

      // Invoice
      modelBuilder.Entity<Invoice>()
          .HasOne(i => i.Order)
          .WithOne(o => o.Invoice)
          .HasForeignKey<Invoice>(i => i.OrderId);

      // Seeding VendorTypes
      var bakerId = Guid.Parse("fbb3e66d-76f6-4c7a-81e9-8796618c5f68");
      var groomerId = Guid.Parse("77424ac7-16a2-4074-8e74-a0ab54ff8b64");

      modelBuilder.Entity<VendorType>().HasData(
          new VendorType { Id = bakerId, Name = "Baker" },
          new VendorType { Id = groomerId, Name = "PetGroomer" }
      );

      // Seeding GeneralInventoryItems
      modelBuilder.Entity<GeneralInventoryItem>().HasData(
          new GeneralInventoryItem { Id = Guid.Parse("71f2629e-25a2-4f94-a6a3-8a5241d400e6"), VendorTypeId = bakerId, Name = "Flour" },
          new GeneralInventoryItem { Id = Guid.Parse("8b189732-d5a2-4058-a602-4b60cf005f4a"), VendorTypeId = bakerId, Name = "Sugar" },
          new GeneralInventoryItem { Id = Guid.Parse("fdb62b10-435c-4af9-b60e-0ea935498d6d"), VendorTypeId = groomerId, Name = "Shampoo" },
          new GeneralInventoryItem { Id = Guid.Parse("1b820d50-e679-4d00-9df9-46481a7b5e49"), VendorTypeId = groomerId, Name = "Brush" }
      );

      //// Seeding inventory items
      //modelBuilder.Entity<InventoryItem>().HasData(
      //    new InventoryItem { Id = Guid.Parse("71f2629e-25a2-4f94-a6a3-8a5241d400e6"), VendorTypeId = bakerId, Name = "Flour" },
      //    new InventoryItem { Id = Guid.Parse("8b189732-d5a2-4058-a602-4b60cf005f4a"), VendorTypeId = bakerId, Name = "Sugar" }
      //);
    }
  }
}
