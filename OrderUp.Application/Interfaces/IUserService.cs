using OrderUp.Domain.Entities;

namespace OrderUp.Application.Interfaces;

public interface IUserService
{
    Task<User?> GetUserByIdAsync(Guid id);
    Task<User?> GetUserByEmailAsync(string email);
    Task<IEnumerable<User>> GetUsersByTenantAsync(Guid tenantId);
    Task RegisterUserAsync(User user, string password);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(Guid id);
}
