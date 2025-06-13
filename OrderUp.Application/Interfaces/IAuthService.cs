using System.Threading.Tasks;
using OrderUp.Domain.Entities;

namespace OrderUp.Application.Interfaces
{
    public interface IAuthService
    {
        /// <summary>
        /// Logs in a user by validating credentials and returns a JWT token if successful.
        /// </summary>
        /// <param name="email">User's email address</param>
        /// <param name="password">User's password</param>
        /// <returns>JWT token string if login succeeds; otherwise, null.</returns>
        Task<string?> LoginAsync(string email, string password);

        /// <summary>
        /// Registers a new user and hashes their password.
        /// Sends an email confirmation token to verify the user's email.
        /// </summary>
        /// <param name="user">User entity to register</param>
        /// <param name="password">Plain text password</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        Task RegisterAsync(User user, string password);

        // You can add more methods here, e.g., for ForgotPassword, ResetPassword, ConfirmEmail, etc.
    }
}
