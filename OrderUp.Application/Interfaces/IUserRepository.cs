using OrderUp.Domain.Entities;

namespace OrderUp.Application.Interfaces;

public interface IUserRepository
{
  Task<User?> GetByIdAsync(Guid id);
  Task<User?> GetByEmailAsync(string email);
  Task<IEnumerable<User>> GetUsersByTenantAsync(Guid tenantId);
  Task AddAsync(User user);
  void Update(User user);
  void Delete(User user);
  Task SaveChangesAsync();
  Task AddUserAsync(User user);
  Task<User?> GetByConfirmationTokenAsync(string token);
}
