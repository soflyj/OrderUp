using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderUp.Application.Interfaces;
using OrderUp.Infrastructure.Persistence;
using OrderUp.Infrastructure.Repositories;
using OrderUp.Infrastructure.Security;

namespace OrderUp.Infrastructure;

public static class DependencyInjection
{
  public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
  {
    // Add SQL Server DbContext
    services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

    // Register repositories
    services.AddScoped<IUserRepository, UserRepository>();
    //services.AddScoped<ITenantRepository, TenantRepository>();
    //services.AddScoped<ILogEntryRepository, LogEntryRepository>();

    // Register security services
    services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
    //services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

    // Register email sender
    //services.AddScoped<IEmailService, SmtpEmailService>();

    return services;
  }
}
