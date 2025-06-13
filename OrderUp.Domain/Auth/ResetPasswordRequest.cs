namespace OrderUp.Application.DTOs.Auth;

public class ResetPasswordRequest
{
    public string Token { get; set; } = null!;            // Token sent by email
    public string Email { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
}
