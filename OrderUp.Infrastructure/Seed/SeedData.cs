// File: OrderUp.Infrastructure/Seed/SeedData.cs
using Microsoft.EntityFrameworkCore;
using OrderUp.Domain.Entities;
using OrderUp.Infrastructure.Persistence;

namespace OrderUp.Infrastructure.Seed;

public static class SeedData
{
  public static async Task SeedAsync(AppDbContext db)
  {
    if (!await db.VendorTypes.AnyAsync())
    {
      var baker = new VendorType { Id = Guid.NewGuid(), Name = "Baker" };
      var groomer = new VendorType { Id = Guid.NewGuid(), Name = "PetGroomer" };

      db.VendorTypes.AddRange(baker, groomer);

      db.GeneralInventoryItems.AddRange(
          new GeneralInventoryItem { Id = Guid.NewGuid(), VendorType = baker, Name = "Flour" },
          new GeneralInventoryItem { Id = Guid.NewGuid(), VendorType = baker, Name = "Sugar" },
          new GeneralInventoryItem { Id = Guid.NewGuid(), VendorType = groomer, Name = "Shampoo" }
      );

      await db.SaveChangesAsync();
    }

    if (!await db.Vendors.AnyAsync())
    {
      var bakerType = await db.VendorTypes.FirstOrDefaultAsync(x => x.Name == "Baker");
      var vendor = new Vendor
      {
        Id = Guid.NewGuid(),
        Name = "Super Bakes",
        VendorTypeId = bakerType!.Id
      };

      db.Vendors.Add(vendor);

      db.Users.Add(new User
      {
        Id = Guid.NewGuid(),
        Vendor = vendor,
        Username = "admin",
        Email = "admin@bakes.com",
        PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123"),
        Role = UserRole.Admin,
        IsEmailConfirmed = true,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
      });

      db.InventoryItems.AddRange(
          new InventoryItem { Id = Guid.NewGuid(), Vendor = vendor, Name = "Flour", Quantity = 100, Cost = 10 },
          new InventoryItem { Id = Guid.NewGuid(), Vendor = vendor, Name = "Sugar", Quantity = 50, Cost = 8 }
      );

      await db.SaveChangesAsync();
    }
  }
}
